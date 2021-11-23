using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MyAccounting.Infrastructure.Persistence;

namespace MyAccounting.Application.Tests
{
    public class DbContextFixture : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        
        private readonly SqliteConnection _connection;

        protected readonly ApplicationDbContext DbContext;

        protected DbContextFixture()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);

            _connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            DbContext = new ApplicationDbContext(options);

            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}