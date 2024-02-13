namespace frontservice_dotnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/hello", Hello);

            app.Run();
        }

        static async Task Hello(HttpContext ctx)
        {
            var message = @$"DOTNET frontservice: {DateTime.Now.ToLongTimeString()} says --> ";
            await ctx.Response.WriteAsync(message);
        }
    }
}
