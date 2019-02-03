using System.Collections.Generic;
using System.Linq;
using UserSearch.Models;
using UserSearch.Models.Github;
using UserSearch.WebClients;

namespace UserSearch.Repositories
{
    /// <summary>
    /// A class specific to github used to query the github api for basic 
    /// information on users
    /// </summary>
    public class GithubRepository : IRepository
    {
        private IWebClient webClient;

        /// <summary>
        /// Public constructor allows injection of a web client to enable
        /// unit testing without making calls to an api.
        /// </summary>
        /// <param name="client">A web client that implements the IWebClient interface</param>
        public GithubRepository(IWebClient client)
        {
            webClient = client;
        }

        /// <summary>
        /// Search for users in github
        /// </summary>
        /// <param name="username">The login name of a github user that you want to search for</param>
        /// <returns>
        /// A generic source control user model including details about the github user
        /// and also details about the users 5 most popular repos
        /// </returns>
        public IUser SearchUsers(string username)
        {
            string userUrl = $"https://api.github.com/users/{username}";

            IGithubUser githubUser = webClient.Query<GithubUser>(userUrl);

            if(githubUser != null && !string.IsNullOrWhiteSpace(githubUser.ReposUrl))
            {
                List<GithubRepo> repos = webClient.Query<List<GithubRepo>>(githubUser.ReposUrl);
                if (repos != null)
                {
                    githubUser.TopFiveMostPopularRepos = repos.OrderByDescending(r => r.StargazersCount).Take(5).ToList();
                }
            }
            return MapGithubUserToUser(githubUser);
        }

        /// <summary>
        /// Maps a github user model to a generic user model.
        /// </summary>
        /// <param name="githubUser">A github user model, including the 5 most popular github repos</param>
        /// <returns>A generic user model, including the user's 5 most popular repos</returns>
        private IUser MapGithubUserToUser(IGithubUser githubUser)
        {
            if(githubUser == null)
            {
                return null;
            }

            IUser user = new User
            {
                Login = githubUser.Login,
                Name = githubUser.Name,
                Location = githubUser.Location,
                AvatarUrl = githubUser.AvatarUrl,
                TopFiveMostPopularRepos = new List<IRepo>()
            };

            if (githubUser.TopFiveMostPopularRepos != null)
            {
                foreach (IGithubRepo githubRepo in githubUser.TopFiveMostPopularRepos)
                {
                    IRepo repo = new Repo
                    {
                        Name = githubRepo.Name,
                        PopularityCount = githubRepo.StargazersCount
                    };
                    user.TopFiveMostPopularRepos.Add(repo);
                }
            }
            return user;
        }
    }
}
