namespace frontservice_dotnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<MyHttpHandler>();

            var helloServiceBaseUrl = builder.Configuration["HelloService:BaseUrl"];
            builder.Services.AddHttpClient("HelloServiceClient", client =>
            {
                client.BaseAddress = new Uri(helloServiceBaseUrl);
            }).AddHttpMessageHandler<MyHttpHandler>();


            var byeServiceBaseUrl = builder.Configuration["ByeService:BaseUrl"];
            builder.Services.AddHttpClient("ByeServiceClient", client =>
            {
                client.BaseAddress = new Uri(byeServiceBaseUrl);
            }).AddHttpMessageHandler<MyHttpHandler>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
