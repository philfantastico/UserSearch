using Newtonsoft.Json;
using System.Collections.Generic;

namespace UserSearch.Models.Github
{
    /// <summary>
    /// A class used to deserialize a github user json into
    /// </summary>
    public class GithubUser : IGithubUser
    {
        /// <summary>
        /// The Id of the github user
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The Login (username) of the github user (i.e. "robconery")
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// The Name of the github user (i.e. "rob conery")
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The Location of the github user
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// The URL of the github user's Avatar image
        /// </summary>
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// The URL that will return the users repos in a json format.
        /// </summary>
        [JsonProperty("repos_url")]
        public string ReposUrl { get; set; }

        /// <summary>
        /// The top 5 repos created by the user based on popularity
        /// (i.e. how many github users have starred the repo)
        /// </summary>
        public List<GithubRepo> TopFiveMostPopularRepos { get; set; }
    }
}
