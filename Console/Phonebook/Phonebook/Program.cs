using Phonebook.Data;
using Spectre.Console;

InitDatabase();

AnsiConsole.Write(new FigletText("Phone Book").Color(Color.Yellow2));

return ;

static void InitDatabase()
{
    using PhoneBookContext db = new();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}