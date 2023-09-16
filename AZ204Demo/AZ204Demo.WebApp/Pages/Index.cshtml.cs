using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AZ204Demo.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogCritical("This is critical message");
            _logger.LogError("This is error message");
            _logger.LogInformation("This is information message");
            _logger.LogTrace("This is trace message");
            _logger.LogWarning("This is warning message");
            _logger.LogDebug("This is debug message");
        }
    }
}