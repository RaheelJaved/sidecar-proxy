using Microsoft.AspNetCore.Mvc;

namespace frontservice_dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger logger;

        public HelloController(IHttpClientFactory httpClientFactory, ILogger<HelloController> logger)
        
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        
        {
            logger.LogInformation("*********** Executing HelloController.Get");
            var client = httpClientFactory.CreateClient("HelloServiceClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "hello");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //var responseString = "the response";
            
            var message = $@"frontservice {DateTime.Now.ToLongTimeString()} says --> {responseString}";

            return Ok(message);
        }
    }
}
