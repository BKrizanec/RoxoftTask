using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsTestingApp.PageObjectModels;

public struct LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsLocked { get; set; }
    public string ExpectedErrorMessage { get; set; }

    public string InfoText()
    {
        if (IsLocked)
            return $"Username: {Username} locked out and unable to log in.";
        else
            return $"Username: {Username} logged in";
    }
}
