using Moq;
using System.Collections.Generic;
using UserSearch.Models;
using UserSearch.Models.Github;
using UserSearch.Repositories;
using UserSearch.WebClients;
using Xunit;

namespace UserSearchTests.Repositories
{
    public class GithubRepositoryTests
    {
        [Fact]
        public void Query_ValidUserAndRepo_MapsReturnedUserFieldsCorrectly()
        {
            //  Arrange
            string userUrl = "https://api.github.com/users/robconery";
            GithubUser githubUser = ValidGithubUser();
            GithubRepo githubRepo = CreateRepo(1, 5);
            List<GithubRepo> githubRepos = new List<GithubRepo> { githubRepo } ;
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(r => r.Query<GithubUser>(userUrl)).Returns(githubUser);
            webClient.Setup(r => r.Query<List<GithubRepo>>(githubUser.ReposUrl)).Returns(githubRepos);
            IRepository repository = new GithubRepository(webClient.Object);

            //  Act
            IUser userReturned = repository.SearchUsers(githubUser.Login);

            //  Assert
            Assert.Equal(githubUser.Login, userReturned.Login);
            Assert.Equal(githubUser.Name, userReturned.Name);
            Assert.Equal(githubUser.AvatarUrl, userReturned.AvatarUrl);
            Assert.Equal(githubUser.Location, userReturned.Location);
            Assert.Equal(githubUser.TopFiveMostPopularRepos[0].Name, userReturned.TopFiveMostPopularRepos[0].Name);
            Assert.Equal(githubUser.TopFiveMostPopularRepos[0].StargazersCount, userReturned.TopFiveMostPopularRepos[0].PopularityCount);
        }

        [Fact]
        public void Query_ValidUserTenRepos_ReturnsTopFiveReposOrderedCorrectly()
        {
            //  Arrange
            string userUrl = "https://api.github.com/users/robconery";
            GithubUser githubUser = ValidGithubUser();
            GithubRepo repo1 = CreateRepo(1, 5);
            GithubRepo repo2 = CreateRepo(2, 500);
            GithubRepo repo3 = CreateRepo(3, 51);
            GithubRepo repo4 = CreateRepo(4, 43);
            GithubRepo repo5 = CreateRepo(5, 56);
            GithubRepo repo6 = CreateRepo(6, 999);
            GithubRepo repo7 = CreateRepo(7, 0);
            GithubRepo repo8 = CreateRepo(8, 14);
            GithubRepo repo9 = CreateRepo(9, 23);
            GithubRepo repo10 = CreateRepo(10, 34);
            List<GithubRepo> githubRepos = new List<GithubRepo>
            {
                repo1, repo2, repo3, repo4, repo5, repo6, repo7, repo8, repo9, repo10
            };
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(r => r.Query<GithubUser>(userUrl)).Returns(githubUser);
            webClient.Setup(r => r.Query<List<GithubRepo>>(githubUser.ReposUrl)).Returns(githubRepos);
            IRepository repository = new GithubRepository(webClient.Object);

            //  Act
            IUser userReturned = repository.SearchUsers(githubUser.Login);

            //  Assert
            Assert.NotNull(userReturned.TopFiveMostPopularRepos);
            Assert.Equal(5, userReturned.TopFiveMostPopularRepos.Count);
            Assert.Equal(repo6.Name, userReturned.TopFiveMostPopularRepos[0].Name);
            Assert.Equal(repo2.Name, userReturned.TopFiveMostPopularRepos[1].Name);
            Assert.Equal(repo5.Name, userReturned.TopFiveMostPopularRepos[2].Name);
            Assert.Equal(repo3.Name, userReturned.TopFiveMostPopularRepos[3].Name);
            Assert.Equal(repo4.Name, userReturned.TopFiveMostPopularRepos[4].Name);
        }

        [Fact]
        public void Query_ValidUserNoRepos_ReturnsEmptyReposCollection()
        {
            //  Arrange
            string userUrl = "https://api.github.com/users/robconery";
            GithubUser githubUser = ValidGithubUser();
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(r => r.Query<GithubUser>(userUrl)).Returns(githubUser);
            webClient.Setup(r => r.Query<List<GithubRepo>>(githubUser.ReposUrl)).Returns(new List<GithubRepo>());
            IRepository repository = new GithubRepository(webClient.Object);

            //  Act
            IUser userReturned = repository.SearchUsers(githubUser.Login);

            //  Assert
            Assert.NotNull(userReturned.TopFiveMostPopularRepos);
            Assert.Empty(userReturned.TopFiveMostPopularRepos);
        }

        private static GithubRepo CreateRepo(int repoId, int stargazerscount)
        {
            return new GithubRepo()
            {
                Id = repoId,
                Name = $"repo{repoId}",
                StargazersCount = stargazerscount
            };
        }

        private static GithubUser ValidGithubUser()
        {
            return new GithubUser()
            {
                Id = 78586,
                Login = "robconery",
                Name = "rob conery",
                Location = "Honolulu, HI",
                AvatarUrl = "https://avatars0.githubusercontent.com/u/78586?v=4",
                ReposUrl = "https://api.github.com/users/robconery/repos"
            };
        }
    }
}
