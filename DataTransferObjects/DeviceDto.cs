using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataTransferObjects
{
  public class CustomNumberAttribute : ValidationAttribute
  {
    public string Prefix { get; set; }

    public override bool IsValid(object? value)
    {
      if (value is not null && value is string s)
      {
        return s.StartsWith(Prefix);
      }
      return false;
    }
  }


  public class DeviceDto
  {
    public int Id { get; set; }

    [JsonPropertyName("name")]
    [StringLength(40), Required, CustomNumber(Prefix = "A")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("desc")]
    [StringLength(200)]
    public string? Description { get; set; }

    [JsonPropertyName("hasval")]
    public bool HasValues { get; set; }
  }
}
