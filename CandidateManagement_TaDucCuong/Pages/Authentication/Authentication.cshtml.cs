using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Candidate_Service;

public class AuthenticationModel : PageModel
{
    private readonly IHRAccountService _hrAccountService;

    [BindProperty]
    public InputModel Input { get; set; }

    public string ErrorMessage { get; set; }

    // Add property to store user role
    public int UserRole { get; private set; }

    public AuthenticationModel(IHRAccountService hrAccountService)
    {
        _hrAccountService = hrAccountService;
    }

    public class InputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public void OnGet()
    {
        ErrorMessage = string.Empty;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            // Authenticate the user and get role
            var (isAuthenticated, userRole) = await AuthenticateAsync(Input.Email, Input.Password);

            if (isAuthenticated)
            {
                UserRole = userRole;
                // You might want to store the user role in session or claims here

                return RedirectToPage("/CandidateProfilePage/Index");
            }
            else
            {
                ErrorMessage = "Invalid login attempt. Please check your credentials and try again.";
            }
        }

        return Page();
    }

    // Updated to return both authentication status and user role
    private async Task<(bool isAuthenticated, int userRole)> AuthenticateAsync(string email, string password)
    {
        // Call the AuthenticateAsync method from HRAccountService
        return await _hrAccountService.AuthenticateAsync(email, password);
    }
}