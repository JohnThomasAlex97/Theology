using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using Th_Dpt.Models;

namespace Th_Dpt.Controllers
{
    public class ReportController : Controller
    {
        private readonly string _connectionString;

        public ReportController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new NpgsqlConnection(_connectionString);
        public IActionResult ClassReport()
        {
            return View();
        }
        public IActionResult GenerateReport()
        {
            return View();
        }

        // ===============================
        // GET ALL DATA
        // ===============================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var db = Connection;

            string query = @"SELECT 
                            id,
                            ""ClassName"",
                            ""Instructor"",
                            ""AttendanceCount"",
                            ""NoBB"",
                            ""SpecialNotes"",
                            created_at
                         FROM public.""ClassReport""
                         ORDER BY id DESC;";

            var data = await db.QueryAsync<ClassReport>(query);

            return Json(data);
        }

        // ===============================
        // INSERT DATA
        // ===============================
        [HttpPost]
        public async Task<IActionResult> Add(ClassReport model)
        {
            try
            {
                using var db = Connection;

                string query = @"INSERT INTO public.""ClassReport""
                         (""ClassName"", ""Instructor"", ""AttendanceCount"", ""NoBB"", ""SpecialNotes"")
                         VALUES
                         (@ClassName, @Instructor, @AttendanceCount, @NoBB, @SpecialNotes)
                         RETURNING id;";

                var id = await db.ExecuteScalarAsync<long>(query, model);

                return Json(new { success = true, id });
            } 
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetCenterReport(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                using var connection = Connection;

                string sql = @"

SELECT 
    c.""Level"",
    c.""ClassName"",
    c.""RegCount"",

    COALESCE(cr.ParticipationCount,0) as ParticipationCount,
    COALESCE(cr.OfferingCount,0) as OfferingCount,
    COALESCE(se.EvangelismCount,0) as EvangelismCount

FROM public.""Class"" c

LEFT JOIN (
    SELECT 
        ""ClassName"",
        SUM(""AttendanceCount"") as ParticipationCount,
        SUM(""NoBB"") as OfferingCount
    FROM public.""ClassReport""
    WHERE (@FromDate::date IS NULL OR created_at::date >= @FromDate::date)
      AND (@ToDate::date IS NULL OR created_at::date <= @ToDate::date)
    GROUP BY ""ClassName""
) cr ON cr.""ClassName"" = c.""ClassName""

LEFT JOIN (
    SELECT 
        ""ClassName"",
        COUNT(id) as EvangelismCount
    FROM public.""Students_Evang""
    WHERE (@FromDate::date IS NULL OR created_at::date >= @FromDate::date)
      AND (@ToDate::date IS NULL OR created_at::date <= @ToDate::date)
    GROUP BY ""ClassName""
) se ON se.""ClassName"" = c.""ClassName""

ORDER BY c.""Level"";
";

                var data = (await connection.QueryAsync<CenterReportVM>(sql,
                    new { FromDate = fromDate, ToDate = toDate })).ToList();

                // Calculate percentage
                foreach (var item in data)
                {
                    if (item.RegCount > 0)
                    {
                        item.ParticipationRate =
                            Math.Round((double)item.ParticipationCount / item.RegCount * 100, 1);

                        item.EvangelismRate =
                            Math.Round((double)item.EvangelismCount / item.RegCount * 100, 1);

                        item.OfferingRate =
                            Math.Round((double)item.OfferingCount / item.RegCount * 100, 1);
                    }
                }

                return Json(data);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
