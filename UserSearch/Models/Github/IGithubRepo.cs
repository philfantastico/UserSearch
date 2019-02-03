using Newtonsoft.Json;

namespace UserSearch.Models.Github
{
    public interface IGithubRepo
    {
        int Id { get; set; }
        string Name { get; set; }
        int StargazersCount { get; set; }
    }
}
