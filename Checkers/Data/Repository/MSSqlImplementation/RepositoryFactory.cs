using System;
using Microsoft.Data.SqlClient;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class RepositoryFactory
{
    private readonly SqlConnection connection;
    public RepositoryFactory(string connection)
    {
        this.connection = new SqlConnection(connection);
    }

    public T GetRepository<T>() where T : Repository
    {
        return (T?) Activator.CreateInstance(typeof(T),connection) ?? throw new InvalidOperationException();
    }
}