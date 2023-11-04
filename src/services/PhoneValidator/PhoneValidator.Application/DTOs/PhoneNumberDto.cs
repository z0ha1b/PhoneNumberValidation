namespace PhoneValidator.Application.DTOs;

public class PhoneNumberDto
{
    public Guid Id { get; set; }
    public string? DefaultCountryCode { get; set; }
    public string? Number { get; set; }
    public string? PhoneType { get; set; }
    public string? Certainty { get; set; }
    public bool IsProcessed { get; set; }
}