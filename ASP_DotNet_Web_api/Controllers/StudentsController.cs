using Microsoft.AspNetCore.Mvc;

namespace ASP_DotNet_Web_api.Controllers
{
    //http://localhost:1234/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentName = new string[] { "mzk", "khan", "mka", "alam", "roja" };

            return Ok(studentName);
        }
    }
}
