namespace PhoneValidator.Core.Model;

public class PhoneNumber
{
    public Guid Id { get; set; } = Guid.NewGuid();
   
    public string? DefaultCountryCode { get; set; }
    public string? Number { get; set; }
    public string? PhoneType { get; set; }
    public string? Certainty { get; set; }
    
    public bool IsProcessed { get; set; }
}