using Microsoft.AspNetCore.Mvc;
using POS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace POS.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TestController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("db")]
        public async Task<IActionResult> testDB()
        {
            var Connected = await _db.Database.CanConnectAsync();
            return Ok(Connected);
        }

    }

}