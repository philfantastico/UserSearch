using System.Collections.Generic;

namespace UserSearch.Models.Github
{
    public interface IGithubUser
    {
        string Login { get; set; }
        string Name { get; set; }
        string Location { get; set; }
        string AvatarUrl { get; set; }
        string ReposUrl { get; set; }
        List<GithubRepo> TopFiveMostPopularRepos { get; set; }
    }
}
