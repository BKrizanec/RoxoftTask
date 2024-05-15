namespace LibraryApp;

public static class MainMenuHelper
{
    public const string ExitOption = "9";
    public const string InvalidInput = "Your input is invalid, please try again.";
    public const string ProcessFailed = "Operation unsuccessful.";
    public const string ProcessSuccess = "Operation successful.";
    public static void Roleplay()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("**********************************");
        Console.WriteLine("* Welcome to the Roxoft Library! *");
        Console.WriteLine("**********************************");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;

        Thread.Sleep(1000);
        Console.WriteLine("...");
        Thread.Sleep(1000);
        Console.WriteLine("Loading...");
        Thread.Sleep(1000);
        Console.WriteLine("...");
        Console.WriteLine();
    }

    public static void MainMenu()
    {

        Console.WriteLine("**********************************");
        Console.WriteLine("* Select a book related activity *");
        Console.WriteLine("**********************************");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine("1: List all the books in the library");
        Console.WriteLine("2: Find a book using the book's id");
        Console.WriteLine("3: Add a book to the library");
        Console.WriteLine("4: Delete a book using the book's id");
        Console.WriteLine("5: Edit a book using the book's id");
        Console.WriteLine("9: You don't actually like books, you just want to leave the application");
        Console.WriteLine();
    }
}
