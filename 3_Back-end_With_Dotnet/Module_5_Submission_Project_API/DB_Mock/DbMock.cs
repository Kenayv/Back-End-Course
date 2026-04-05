public class DbControllerMock
{
    private int _userCount;

    // userCount works both as NextId and the intended userCount value.
    // for the mock purposes it can stay that way.

    private DbControllerMock()
    {
        _userCount = 10;
    }

    private static readonly DbControllerMock _instance = new DbControllerMock();

    public static DbControllerMock getInstance() => _instance;

    public User? GetUser(int id)
    {
        if (id < 0 || id >= _userCount)
        {
            return null;
        }

        return new User(id, $"user[{id}]");
    }

    public User? UpdateUser(int id, string name)
    {
        if (id < 0 || id >= _userCount)
        {
            return null;
        }

        return new User(id, name);
    }

    public User AddUser(string name)
    {
        return new User(_userCount++, name);
    }

    public bool RemoveUser(int id)
    {
        if (id < 0 || id >= _userCount)
        {
            return false;
        }
        --_userCount;
        return true;
    }
}
