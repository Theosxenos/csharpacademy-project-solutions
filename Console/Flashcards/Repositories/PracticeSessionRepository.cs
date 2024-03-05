namespace Flashcards.Repositories;

public class PracticeSessionRepository
{
    private FlashcardsContext db = new();

    public DataTable GetMonthlyAverageByYear(int year)
    {
        // var sql = """
        //           DECLARE @months NVARCHAR(MAX), @sql NVARCHAR(MAX);
        //           
        //           -- Generate a list of month numbers for pivoting
        //           -- Ensure uniqueness in the month numbers for pivoting
        //           SELECT @months = STRING_AGG(QUOTENAME(SessionDate), ',') WITHIN GROUP (ORDER BY SessionDate)
        //           FROM (SELECT DISTINCT FORMAT(SessionDate, 'MMM') AS SessionDate FROM Sessions WHERE YEAR(SessionDate) = @year) AS UniqueMonths;
        //           
        //           -- Debugging: Print the corrected months to check
        //           PRINT @months;
        //           
        //           -- Construct dynamic SQL for pivot operation
        //           SET @sql = N'SELECT s.Name, '+ @months +' FROM (
        //                          SELECT StackId, Score, FORMAT(SessionDate, ''MMM'') as Month
        //                          FROM Sessions YEAR(SessionDate) = @year
        //                        ) as SourceTable
        //                        PIVOT(
        //                          AVG(Score) FOR Month IN (' + @months + ')
        //                        ) AS PivotTable ' +
        //                      'JOIN Stacks s ON PivotTable.StackId = s.Id;';
        //           
        //           -- Execute the constructed SQL
        //           EXEC sp_executesql @sql;
        //           """;

        var columns = db.Sessions
            .Where(s => s.SessionDate.Year == year)
            .GroupBy(s => s.SessionDate.Month)
            // .OrderBy(g => g.Key) // Optionally, order by month
            .Select(g => $"[{g.FirstOrDefault().SessionDate:MMMM}]");
            // .Distinct();
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