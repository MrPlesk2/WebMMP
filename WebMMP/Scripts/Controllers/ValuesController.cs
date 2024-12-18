using Microsoft.AspNetCore.Mvc;

namespace WebMMP.Scripts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet("")]
        public string Get()
        {
            var res = DatabaseController.GetAllAuditoryBook();
            return res;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{number}")]
        public string Get(string number)
        {
            var res = DatabaseController.GetCurrentAuditoryBook(number);
            return res;
        }

        // POST api/<ValuesController>
        [HttpPost("{number}/{book}")]
        public string Post(string number, bool book)
        {
            var ip = HttpContext.Connection.RemoteIpAddress;
            Console.WriteLine(number);
            Console.WriteLine(book);
            var code = DatabaseController.SetCurrentAuditoryBook(number, book, ip.ToString());
            return code.ToString();
        }
    }
}
