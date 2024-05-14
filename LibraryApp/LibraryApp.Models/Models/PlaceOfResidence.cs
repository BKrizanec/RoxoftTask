using System;
using System.Collections.Generic;

namespace LibraryApp.Models.Models;

public partial class PlaceOfResidence
{
    public int PlaceOfResidenceId { get; set; }

    public string Name { get; set; } = null!;

    public int PostalCode { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
