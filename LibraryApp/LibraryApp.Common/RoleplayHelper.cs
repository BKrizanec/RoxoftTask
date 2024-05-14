namespace LibraryApp.Common;

public static class RoleplayHelper
{
    private static void MainMenu()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("*************************************");
        Console.WriteLine("* Welcome to the Roxoft Library app *");
        Console.WriteLine("*************************************");
        Console.ForegroundColor = ConsoleColor.White;

        string userSelection;

        do
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("**********************************");
            Console.WriteLine("* Select a book related activity *");
            Console.WriteLine("**********************************");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("1: Find a book using the book's id");
            Console.WriteLine("2: List all the books in the library");
            Console.WriteLine("3: Add a book to the library");
            Console.WriteLine("4: Edit a book using the book's id");
            Console.WriteLine("5: Delete a book using the book's id");
            Console.WriteLine("9: You don't actually like books, you just want to leave the application");

            userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    RegisterEmployee();
                    break;
                case "2":
                    RegisterWork();
                    break;
                case "3":
                    PayEmployee();
                    break;
                case "4":
                    ChangeRate();
                    break;
                case "5":
                    GiveBonus();
                    break;
                case "9": break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
        while (userSelection != "9");

        Console.WriteLine("Thank you for using the application");
        Console.Read();
    }


    
}
