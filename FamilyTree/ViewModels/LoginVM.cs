using Microsoft.AspNetCore.Authentication;

namespace FamilyTree.ViewModels;

public class LoginVM
{
    public string Email { get; set; }
    
    public string Password { get; set; }

    public bool RememberMe { get; set; }

    public IEnumerable<AuthenticationScheme> ExternalLogins { get; set; }
}