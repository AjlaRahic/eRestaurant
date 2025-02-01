using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.ModulLog.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        public LogController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetLogs(DateTime startDate, DateTime endDate)
        {
            var logs = await _dbContext.LogKretanjeSistem
                           .Where(l => l.Vrijeme >= startDate && l.Vrijeme <= endDate)
                           .GroupBy(l => l.Vrijeme.Date)
                           .Select(g => new
                           {
                               Vrijeme = g.Key.ToString("yyyy-MM-dd"),
                               BrojAktivnosti = g.Count()
                           })
                           .ToListAsync();
            
            return Ok(logs);
        }
    }
}
