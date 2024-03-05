namespace Flashcards.Repositories;

public class PracticeSessionRepository
{
    private readonly FlashcardsContext db = new();

    public DataTable GetMonthlyAverageByYear(int year)
    {
        var columns = db.Sessions
            .Where(s => s.SessionDate.Year == year)
            .GroupBy(s => s.SessionDate.Month)
            .Select(g => $"[{g.FirstOrDefault().SessionDate:MMMM}]");
        var columnsFormatted = string.Join(',', columns);

        var sql = $"""
                   SELECT s.Name, {columnsFormatted}
                   FROM (
                       SELECT StackId, Score, FORMAT(SessionDate, 'MMMM') as Month
                       FROM Sessions
                   ) as SourceTable
                   PIVOT ( AVG(Score) FOR Month in ({columnsFormatted}) ) as PivotTable
                   JOIN Stacks as s ON s.Id = PivotTable.StackId
                   """;

        using var connection = GetConnection();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        using var reader = command.ExecuteReader();
        var pivotResult = new DataTable();
        pivotResult.Load(reader);

        return pivotResult;
    }

    private DbConnection GetConnection()
    {
        var connection = db.Database.GetDbConnection();
        connection.Open();

        return connection;
    }
}