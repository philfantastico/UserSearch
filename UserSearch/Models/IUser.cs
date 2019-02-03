using System.Collections.Generic;

namespace UserSearch.Models
{
    public interface IUser
    {
        string Login { get; set; }
        string Name { get; set; }
        string Location { get; set; }
        string AvatarUrl { get; set; }
        List<IRepo> TopFiveMostPopularRepos { get; set; }
    }
}
