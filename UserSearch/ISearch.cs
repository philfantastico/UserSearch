using UserSearch.Models;

namespace UserSearch
{
    public interface ISearch
    {
        IUser SearchUsers(string username);
    }
}
