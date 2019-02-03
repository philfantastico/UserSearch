namespace UserSearch.WebClients
{
    public interface IWebClient
    {
        T Query<T>(string url);
    }
}
