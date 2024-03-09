using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;

namespace ExcelReader.Data;

public class DapperContext(IConfiguration configuration)
{
    public IDbConnection GetConnection()
    {
        var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        return connection;
    }
    
    public void EnsureDeleted()
    {
        if (File.Exists("reader.db"))
        {
            File.Delete("reader.db");
        }
    }

    public void EnsureCreated()
    {
        using var connection = GetConnection();
        var sql = """
                  CREATE TABLE Gods (
                      Name TEXT not null,
                      Domain TEXT not null,
                      Symbol text not null,
                      Fame text not null
                  )
                  """;
        connection.Execute(sql);
    }
}