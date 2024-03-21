using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace ExerciseTracker.Data;

public class ExerciseDapperContext
{
    private readonly IConfiguration _config;

    public ExerciseDapperContext(IConfiguration config)
    {
        _config = config;
        Init();
    }

    private void Init()
    {
        using var con = GetConnection();
        var sql = """
                  CREATE TABLE IF NOT EXISTS "Running" (
                      "Id" INTEGER NOT NULL CONSTRAINT "PK_Running" PRIMARY KEY AUTOINCREMENT,
                      "Comments" TEXT NOT NULL,
                      "DateEnd" TEXT NOT NULL,
                      "DateStart" TEXT NOT NULL,
                      "Duration" TEXT NOT NULL
                  );

                  """;
        con.Execute(sql);
    }

    public SqliteConnection GetConnection()
    {
        var con = new SqliteConnection(_config.GetConnectionString("DefaultConnection"));
        if (con.State != ConnectionState.Open)
            con.Open();

        return con;
    }
}