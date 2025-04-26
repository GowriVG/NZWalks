using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{
    //https://localhost:portnumber/api/students

    [Route("api/[controller]")] //route is pointing to api/controller. while running the controller will take the name 
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //create action method 
        // Get: https://localhost:portnumber/api/students 
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            //student names are coming from the db
            string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily", "David" };
            return Ok(studentNames);
        }
    }
}
