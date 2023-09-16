using Microsoft.AspNetCore.Mvc.RazorPages;
using StackExchange.Redis;

namespace AZ204Demo.WebApp.Pages
{
    public class RedisModel : PageModel
    {
        private const string RedisConnectionString = "";

        public string Result { get; set; }


        public async Task OnGet()
        {
            // This needs to be reused in real application
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnectionString);

            var db = redis.GetDatabase();
            var result = await db.StringGetAsync("demo");

            if (result.HasValue)
            {
                Result = result.ToString();
            }
            else
            {
                Result = "Did not find value in cache by key: demo";
            }

        }
    }


}
