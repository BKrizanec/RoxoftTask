using System;
using System.Collections.Generic;

namespace LibraryApp.Models.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Genre { get; set; }

    public int? Author { get; set; }

    public virtual Author? AuthorNavigation { get; set; }

    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
}
