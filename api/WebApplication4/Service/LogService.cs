using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.EntityModels;

namespace WebApplication4.Service
{
    public class LogService
    {
        private readonly ApplicationDbContext _dbContext;

        public LogService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Dohvati logove po vremenskom razdoblju
        public List<LogKretanjeSistem> GetLogs(DateTime? startDate = null, DateTime? endDate = null)
        {
            IQueryable<LogKretanjeSistem> logs = _dbContext.LogKretanjeSistem;

            if (startDate.HasValue)
                logs = logs.Where(log => log.Vrijeme >= startDate.Value);

            if (endDate.HasValue)
                logs = logs.Where(log => log.Vrijeme <= endDate.Value);

            return logs.ToList();
        }
        public int SaveLog(LogKretanjeSistem log)
        {
           

            
          

            _dbContext.LogKretanjeSistem.Add(log);
            _dbContext.SaveChanges();
            return log.Id;
        }


        // Dohvati statistiku aktivnosti po korisnicima i putanji
        public List<object> GetActivityStats(DateTime startDate, DateTime endDate)
        {
            return _dbContext.LogKretanjeSistem
                .Where(log => log.Vrijeme >= startDate && log.Vrijeme <= endDate)
                .GroupBy(log => log.Vrijeme.Date)
                .Select(group => new
                {
                    Datum = group.Key,
                    BrojAktivnosti = group.Count()
                })
                .ToList<object>();
        }
    }

    // DTO za statistiku aktivnosti
    public class ActivityStatsDto
    {
        public int KorisnikId { get; set; }
        public string QueryPath { get; set; }
        public int ActivityCount { get; set; }
    }

}
