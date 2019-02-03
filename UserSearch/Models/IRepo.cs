namespace UserSearch.Models
{
    public interface IRepo
    {
        string Name { get; set; }
        int PopularityCount { get; set; }
    }
}
