using LibraryApp.Service.Common;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp;

public class Program
{ 
    static async Task Main(string[] args)
    {
        var serviceProvider = ContainerSetup.Configure();
        var bookService = serviceProvider.GetService<IBookService>();
        BookMenuHandler.Initialize(bookService);        

        Dictionary<string, Func<Task>> bookMenu = new Dictionary<string, Func<Task>>()
        {
            {"1", BookMenuHandler.GetBooksAsync},
            {"2", BookMenuHandler.GetBookByIdAsync},
            {"3", BookMenuHandler.AddBookAsync},
            {"4", BookMenuHandler.DeleteBookAsync},
            {"5", BookMenuHandler.UpdateBookAsync}
        };

        MainMenuHelper.Roleplay();

        string userSelection;
        do
        {
            MainMenuHelper.MainMenu();

            userSelection = Console.ReadLine();

            if (bookMenu.TryGetValue(userSelection, out var action))
            {
                await action();
            }
            else if (userSelection != MainMenuHelper.ExitOption)
            {
                Console.WriteLine(MainMenuHelper.InvalidInput);
            }
        }
        while (userSelection != MainMenuHelper.ExitOption);
    }    
}
