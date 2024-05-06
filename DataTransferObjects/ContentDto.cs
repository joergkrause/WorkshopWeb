namespace DataTransferObjects
{
  public class ContentDto
  {
    public int Version { get; set; }

    public DateTime Created { get; set; }

    public string Text { get; set; } = default!;
  }
}