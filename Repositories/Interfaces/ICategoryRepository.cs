namespace project_mvc;

public interface ICategoryRepository
{
    IEnumerable<Category> Categories { get; }
}
