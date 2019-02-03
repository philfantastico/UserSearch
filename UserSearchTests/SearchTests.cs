using Moq;
using UserSearch;
using UserSearch.Models;
using UserSearch.Repositories;
using Xunit;

namespace UserSearchTests
{
    public class SearchTests
    {
        [Fact]
        public void SearchUsers_HappyPath()
        {
            //  Arrange
            string username = "robconery";
            string location = "Honolulu, HI";
            string avatarUrl = "https://avatars0.githubusercontent.com/u/78586?v=4";
            IUser user = new User()
            {
                Name = username,
                Location = location,
                AvatarUrl = avatarUrl
            };
            Mock<IRepository> repository = new Mock<IRepository>();
            repository.Setup(r => r.SearchUsers(username)).Returns(user);
            ISearch search = new Search(repository.Object);

            //  Act
            IUser userReturned = search.SearchUsers(username);

            //  Assert
            Assert.Equal(username, userReturned.Name);
            Assert.Equal(location, userReturned.Location);
            Assert.Equal(avatarUrl, userReturned.AvatarUrl);
        }
    }
}
