using UserSearch.Models;
using UserSearch.Repositories;

namespace UserSearch
{
    public class Search : ISearch
    {
        private readonly IRepository repository;

        public Search(IRepository repositoryToSearch)
        {
            repository = repositoryToSearch;
        }

        public IUser SearchUsers(string username)
        {
            return repository.SearchUsers(username);
        }
    }
}
