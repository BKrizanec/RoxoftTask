using System;
using System.Collections.Generic;

namespace LibraryApp.Models.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string Contact { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? PlaceOfResidence { get; set; }

    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();

    public virtual PlaceOfResidence? PlaceOfResidenceNavigation { get; set; }
}
