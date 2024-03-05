namespace Flashcards.Views;

public class PracticeView : BaseView
{
    public void ShowLog(DataTable logTable)
    {
        var table = new Table();
        
        foreach (DataColumn column in logTable.Columns)
        {
            table.AddColumn(int.TryParse(column.ColumnName, out var columnMonthNumber)
                ? GetMonthName(columnMonthNumber)
                : column.ColumnName);
        }

        foreach (DataRow row in logTable.Rows)
        {
            table.AddRow(row.ItemArray.Select(i => i is DBNull ? "0" : i.ToString()).ToArray());
        }

        AnsiConsole.Write(table);
        
        Console.ReadKey();
    }

    private string GetMonthName(int month, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.DateTimeFormat.GetMonthName(month);
    }
}