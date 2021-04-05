using System;
using Microsoft.AspNetCore.Mvc;

namespace week1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Books : ControllerBase
    {

        [HttpGet]
        public IActionResult Get(string name)
        {
            var result = "Hello" + name;
            return Ok(result);
        }

        [HttpGet("News")]
        public IActionResult GetNews(string name)
        {
            var result = "Hello" + name;
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(int id, string name)
        {
            var result = id.ToString() + " " + name;
            return Ok(result);
        }

        [HttpGet("Now")]
        public IActionResult GetNow()
        {
            var result = DateTime.Now;
            return Ok(result);
        }

    }
}