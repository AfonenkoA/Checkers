using System;
using Microsoft.Data.SqlClient;

namespace Checkers.Data.Repository.MSSqlImplementation;


public sealed class RepositoryFactory
{
    private readonly Repository[] _repositories;

    public RepositoryFactory(string connectionString)
    {
        var connection = new SqlConnection(connectionString);
        _repositories = new Repository[]
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

    public T Get<T>() where T : Repository
    {
        foreach (var rep in _repositories)
            if (rep is T repository)
                return repository;
        throw new InvalidOperationException();
    }
}