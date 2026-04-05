public class DbControllerMock
{
    private List<User> _users;
    private int _nextId;

    private DbControllerMock()
    {
        _users = new List<User>
        {
            new User(1, "Alice", "alice@mail.com"),
            new User(2, "Bob", "bob@mail.com"),
            new User(3, "Carol", "carol@mail.com"),
            new User(4, "David", "david@mail.com"),
            new User(5, "Eva", "eva@mail.com"),
            new User(6, "Frank", "frank@mail.com"),
            new User(7, "Grace", "grace@mail.com"),
            new User(8, "Henry", "henry@mail.com"),
            new User(9, "Isla", "isla@mail.com"),
            new User(10, "Jack", "jack@mail.com"),
        };
        _nextId = _users.Count + 1;
    }

    private static readonly DbControllerMock _instance = new DbControllerMock();

    public static DbControllerMock getInstance() => _instance;

    public Task<User?> GetUserByIdAsync(int id)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        return Task.FromResult(_users);
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Email == email));
    }

    public async Task<User?> UpdateUserNameAsync(int id, string name)
    {
        var user = await GetUserByIdAsync(id);
        if (user != null)
        {
            user.Name = name;
        }
        return user;
    }

    public async Task<User> AddUserAsync(string name, string email)
    {
        //Some basic validation for the sake of the project
        // works but user{name: "a, email: "a@"} passes.
        if (string.IsNullOrWhiteSpace(name) || !email.Contains('@'))
        {
            throw new ArgumentException("invalid e-mail or name");
        }
        if (await GetUserByEmailAsync(email) != null)
        {
            throw new InvalidOperationException("User with that e-mail already exists");
        }

        var user = new User(_nextId++, name, email);
        _users.Add(user);

        return user;
    }

    public async Task<bool> RemoveUserAsync(int id)
    {
        var user = await GetUserByIdAsync(id);

        if (user == null)
        {
            return false;
        }
        _users.Remove(user);
        return true;
    }
}
