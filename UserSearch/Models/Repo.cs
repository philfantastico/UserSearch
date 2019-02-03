using Newtonsoft.Json;
using System.ComponentModel;

namespace UserSearch.Models
{
    /// <summary>
    /// A class used to store information about a source control repo in
    /// </summary>
    public class Repo : IRepo
    {
        /// <summary>
        /// The Name of the source control repo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The popularity of the repository (i.e. the number of users who have favourited the repository)
        /// </summary>
        [DisplayName("Popularity Count")]
        public int PopularityCount { get; set; }
    }
}
