using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebMMP.Pages
{
    public class FifthFloor : PageModel
    {
        private readonly ILogger<FifthFloor> _logger;

        public FifthFloor(ILogger<FifthFloor> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}