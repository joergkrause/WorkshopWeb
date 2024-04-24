namespace DataTransferObjects
{
  public class MeasureValueDto
  {
    public float Value { get; set; }

    public string Unit { get; set; } = default!;
  }
}