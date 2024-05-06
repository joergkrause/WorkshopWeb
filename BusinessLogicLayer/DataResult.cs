using DomainModels;

namespace BusinessLogicLayer
{
  public record Result(bool Success, Exception? Exception);
  public record DataResult<T>(T Data, bool Success, Exception? Exception) 
    : Result(Success, Exception)
    where T : class
    ;
}