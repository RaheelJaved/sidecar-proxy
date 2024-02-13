namespace backservice_hello_dotnet
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
            var message = @$"Hello from backservice";
            await ctx.Response.WriteAsync(message);
        }
    }
}
