namespace Checkers.Data.Old;

public class DatabaseFactory
{
    private readonly GameDatabase _database;

    public DatabaseFactory(string connection)
    {
        _database = new GameDatabase(connection);
    }

    public GameDatabase Database => _database;
}