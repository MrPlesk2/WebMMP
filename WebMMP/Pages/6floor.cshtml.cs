using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebMMP.Pages
{
    public class SixthFloor : PageModel
    {
        private readonly ILogger<SixthFloor> _logger;

        public SixthFloor(ILogger<SixthFloor> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
