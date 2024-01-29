using DataSetADO.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataSetADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class datasetController : ControllerBase
    {
        private readonly IStudents _students;
        public datasetController(IStudents students)
        {
            _students=students;
        }



        [HttpGet]
        public IActionResult GetData()
        {
            return Ok(_students.GetData());
        }
    }
}
