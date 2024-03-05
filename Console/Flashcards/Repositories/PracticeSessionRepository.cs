namespace Flashcards.Repositories;

public class PracticeSessionRepository
{
    private readonly FlashcardsContext db = new();

    public DataTable GetMonthlyAverageByYear(int year)
    {
        CheckYearHasSessions(year);
        
        var sql = $"""
                  DECLARE @months NVARCHAR(MAX), @sql NVARCHAR(MAX);  
                    
                  -- Generate a list of month numbers for pivoting  
                  -- Ensure uniqueness in the month numbers for pivoting  
                  SELECT @months = STRING_AGG(QUOTENAME(SessionMonth), ',') WITHIN GROUP (ORDER BY SessionMonth)  
                  FROM (SELECT DISTINCT MONTH(SessionDate) as SessionMonth FROM Sessions WHERE YEAR(SessionDate) = {year}) AS UniqueMonths;  
                    -- FORMAT(SessionDate, 'MMM') AS
                  -- Debugging: Print the corrected months to check  
                  PRINT @months;  
                    
                  -- Construct dynamic SQL for pivot operation  
                  SET @sql = N'SELECT s.Name, '+ @months +' FROM (  
                                 SELECT StackId, Score, MONTH(SessionDate) as Month  
                                 FROM Sessions WHERE YEAR(SessionDate) = {year} ) as SourceTable 
                             PIVOT( AVG(Score) FOR Month IN (' + @months + ')  
                               ) AS PivotTable    
                             JOIN Stacks s ON PivotTable.StackId = s.Id;';  
                    
                  -- Execute the constructed SQL  
                  EXEC sp_executesql @sql;
                  """;

        using var connection = GetConnection();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        using var reader = command.ExecuteReader();
        var pivotResult = new DataTable();
        pivotResult.Load(reader);

        return pivotResult;
    }

    private void CheckYearHasSessions(int year)
    {
        if (!db.Sessions.Any(s => s.SessionDate.Year == year))
            throw new NotFoundException($"No sessions found for {year}.");
    }

    private DbConnection GetConnection()
    {
        var connection = db.Database.GetDbConnection();
        connection.Open();

        return connection;
    }
}