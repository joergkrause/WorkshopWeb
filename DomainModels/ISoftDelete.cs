namespace DomainModels;

public interface ISoftDelete
{
}

public interface ISoftDeleteProperties
{
    public bool IsDeleted { get; set; }
}