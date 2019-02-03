using Newtonsoft.Json;

namespace UserSearch.Models.Github
{
    /// <summary>
    /// A class used to deserialize a github repo json into
    /// </summary>
    public class GithubRepo : IGithubRepo
    {
        /// <summary>
        /// The Id of the github repo
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The Name of the github repo
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// How many times users have starred the repo
        /// </summary>
        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }
    }
}
