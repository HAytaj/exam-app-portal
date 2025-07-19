using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExamAppApi.Middlewares
{
  public class ExamApiExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExamApiExceptionMiddleware> _logger;

    public ExamApiExceptionMiddleware(RequestDelegate next, ILogger<ExamApiExceptionMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
      try
      {
        await _next(httpContext);
      }
      catch (DbUpdateException ex) when (ex.InnerException?.Message != null)
      {
        var message = ex.InnerException.Message;

        if (message.Contains("FK__exams__subject_"))
        {
          if (message.Contains("INSERT") || message.Contains("UPDATE"))
          {
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsJsonAsync(new { message = "Belə bir fənn mövcud deyil." });
          }
          else if (message.Contains("DELETE"))
          {
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsJsonAsync(new { message = "Bu fənnə bağlı imtahanlar var. Əvvəlcə imtahanları silin." });
          }
          else
          {
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsJsonAsync(new { message = "Fənn ilə əlaqəli məlumatlarla bağlı çətinlik yaranıb. Müvafiq şəxsə bildirin." });
          }
        }
        else
        {
          httpContext.Response.StatusCode = 400;
          await httpContext.Response.WriteAsJsonAsync(new { message = "Əlaqəli məlumatla bağlı problem baş verdi." });
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Xəta baş verdi");
        httpContext.Response.StatusCode = 500;
        await httpContext.Response.WriteAsJsonAsync(new { message = ex.Message + ex.InnerException?.InnerException });
      }
    }
  }

  public static class ExamApiExceptionMiddlewareExtensions
  {
    public static IApplicationBuilder UseExamApiExceptionMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<ExamApiExceptionMiddleware>();
    }
  }
}
