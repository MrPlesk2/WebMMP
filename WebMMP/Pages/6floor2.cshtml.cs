using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebMMP.Pages
{
    public class SixthFloor2 : PageModel
    {
        private readonly ILogger<SixthFloor2> _logger;

        public SixthFloor2(ILogger<SixthFloor2> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}