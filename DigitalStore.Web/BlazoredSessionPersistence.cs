using Blazored.LocalStorage;
using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;

public class BlazoredSessionPersistence : IGotrueSessionPersistence<Session>
{
    private readonly ISyncLocalStorageService _syncLocalStorage;  // синхронная версия!
    private const string Key = "supabase.auth.token";

    public BlazoredSessionPersistence(ISyncLocalStorageService syncLocalStorage)
    {
        _syncLocalStorage = syncLocalStorage;
    }

    public Session? LoadSession()
    {
        try
        {
            return _syncLocalStorage.GetItem<Session>(Key);
        }
        catch
        {
            return null;
        }
    }

    public void SaveSession(Session session)
    {
        _syncLocalStorage.SetItem(Key, session);
    }

    public void DestroySession()
    {
        _syncLocalStorage.RemoveItem(Key);
    }
}