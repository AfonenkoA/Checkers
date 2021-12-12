using System;
using System.Data.SqlClient;

namespace WebService.Repository.MSSqlImplementation;


public sealed class RepositoryFactory
{
    private readonly RepositoryBase[] _repositories;

    internal RepositoryFactory(string connectionString)
    {
        var connection = new SqlConnection(connectionString);
        _repositories = new RepositoryBase[]
        {
            new ChatRepository(connection),
            new ForumRepository(connection),
            new ItemRepository(connection),
            new NewsRepository(connection),
            new ResourceRepository(connection),
            new StatisticsRepository(connection),
            new UserRepository(connection)
        };
    }

    internal T Get<T>() where T : RepositoryBase
    {
        foreach (var rep in _repositories)
            if (rep is T repository)
                return repository;
        throw new InvalidOperationException();
    }
}