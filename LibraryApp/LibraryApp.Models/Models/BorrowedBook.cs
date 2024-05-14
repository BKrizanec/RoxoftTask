using System;
using System.Collections.Generic;

namespace LibraryApp.Models.Models;

public partial class BorrowedBook
{
    public int BorrowedBookId { get; set; }

    public int? Book { get; set; }

    public int? Member { get; set; }

    public DateOnly CheckoutDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book? BookNavigation { get; set; }

    public virtual Member? MemberNavigation { get; set; }
}
