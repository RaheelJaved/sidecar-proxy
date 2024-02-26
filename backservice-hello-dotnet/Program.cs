
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace backservice_hello_dotnet
{
    public class Program
    {

        private static readonly Random random = new Random();
        private static int failureRate;

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            failureRate = builder.Configuration.GetValue<int>("FailureRate");
            var app = builder.Build();            

            app.MapGet("/hello", async (HttpContext ctx, ILogger<Program> logger) => await Hello(ctx, logger));


            app.Run();
        }

        static async Task Hello(HttpContext ctx, ILogger logger)
        {
            LogRequestHeaders(ctx, logger);

            var randomValue = random.Next(0, 100);
            if (randomValue < failureRate)
            {
                logger.LogInformation($"*********** randomValue is {randomValue}, going to return error");
                ctx.Response.StatusCode = 500;
                await ctx.Response.WriteAsync(@$"Random failur value: {randomValue}" );
                return;
            }
            logger.LogInformation($"*********** randomValue is {randomValue}, returning success");
            var message = @$"Hello from backservice at {DateTime.Now.ToLongTimeString()} - random value: {randomValue}";
            await ctx.Response.WriteAsync(message);
        }

        private static void LogRequestHeaders(HttpContext ctx, ILogger logger)
        {
            StringBuilder headersBuilder = new StringBuilder();
            foreach (var header in ctx.Request.Headers)
            {
                headersBuilder.AppendLine($"{header.Key}: {header.Value}");
            }

            logger.LogInformation("HTTP Request Headers: \n{Headers}", headersBuilder);
        }
    }
}
