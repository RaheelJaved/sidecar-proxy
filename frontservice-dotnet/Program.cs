namespace frontservice_dotnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<MyHttpHandler>();

            var baseUrl = builder.Configuration["HelloService:BaseUrl"];
            builder.Services.AddHttpClient("HelloServiceClient", client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            }).AddHttpMessageHandler<MyHttpHandler>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
