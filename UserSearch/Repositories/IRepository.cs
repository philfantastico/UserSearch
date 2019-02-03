using UserSearch.Models;

namespace UserSearch.Repositories
{
    public interface IRepository
    {
        IUser SearchUsers(string username); 
    }
}
