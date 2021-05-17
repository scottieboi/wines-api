using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WinesApi.Models;

namespace UnitTests.TestInfrastructure
{
    public abstract class SqliteTestBase : IDisposable
    {
        private readonly DbConnection _connection;
        protected DbContextOptions<DataContext> ContextOptions { get; }

        protected SqliteTestBase()
        {
            ContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;

            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        private DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();
    }
}