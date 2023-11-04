using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PhoneValidator.Application.ApiServices.Interfaces;
using PhoneValidator.Application.Configurations;
using PhoneValidator.Application.DTOs;
using RestSharp;

namespace PhoneValidator.Application.ApiServices.Implementations;

public class PhoneVerificationService : IPhoneVerificationService
{
    private readonly RestClient _client;

    public PhoneVerificationService(IOptions<ApiConfig> apiConfig)
    {
        if (apiConfig.Value.ApiBaseUrl is null)
        {
            throw new Exception("API BaseUrl cannot be null");
        }

        if (apiConfig.Value.AuthToken is null)
        {
            throw new Exception("API Auth Token cannot be null");
        }

        var options = new RestClientOptions(apiConfig.Value.ApiBaseUrl)
        {
            MaxTimeout = -1
        };

        _client = new RestClient(options);

        _client.AddDefaultHeader("auth-token", apiConfig.Value.AuthToken);
        _client.AddDefaultHeader("Accept", "application/json");
        _client.AddDefaultHeader("Content-Type", "application/json");
    }

    public async Task Verify(PhoneNumberDto phoneNumber)
    {
        var request = new RestRequest("sync/queryresult/PhoneValidate/1.0/", Method.Post);
        request.AddBody(phoneNumber, ContentType.Json);

        try
        {
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var responsePhone = JsonConvert.DeserializeObject<PhoneNumberDto>(response.Content);

                phoneNumber.Certainty = responsePhone?.Certainty;
                phoneNumber.PhoneType = responsePhone?.PhoneType;
                phoneNumber.Number = responsePhone?.Number;
            }
            else
            {
                phoneNumber.Certainty = response.Content;
            }
        }
        catch (Exception e)
        {
            phoneNumber.Certainty = $"{e.Message}";
        }

        phoneNumber.IsProcessed = true;
    }
}