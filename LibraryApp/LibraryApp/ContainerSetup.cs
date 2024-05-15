using LibraryApp.Models.Models;
using LibraryApp.Repository.Common;
using LibraryApp.Repository;
using LibraryApp.Service.Common;
using LibraryApp.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp;

public static class ContainerSetup
{
    public static IServiceProvider Configure()
    {
        return new ServiceCollection()
            .AddDbContext<RoxoftLibraryContext>(
            options => options.UseSqlServer())
            .AddSingleton<IBookService, BookService>()
            .AddSingleton<IBookRepository, BookRepository>()
            .BuildServiceProvider();
    }    
}
