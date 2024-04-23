namespace DomainModels;

public class MeasureValue : EntityBase
{
  public float Value { get; set; }

  public string Unit { get; set; } = default!;
}