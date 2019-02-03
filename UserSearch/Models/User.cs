using Newtonsoft.Json;
using System.Collections.Generic;

namespace UserSearch.Models
{
    /// <summary>
    /// A class used to store information about a source control user in
    /// </summary>
    public class User : IUser
    {
        /// <summary>
        /// The Login name of the source control user
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The Name of the source control user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Location of the source control user
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The URL of the source control user's Avatar image
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// The user's top five most popular source control repos based on 
        /// how many other users have favourited the repo.
        /// </summary>
        public List<IRepo> TopFiveMostPopularRepos { get; set; }
    }
}
