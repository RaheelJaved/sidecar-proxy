using Microsoft.AspNetCore.Mvc;

namespace frontservice_dotnet
{
    public class ByeController
    {
    }
}



namespace frontservice_dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ByeController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger logger;

        public ByeController(IHttpClientFactory httpClientFactory, ILogger<HelloController> logger)

        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> Get()

        {
            logger.LogInformation("*********** Executing ByeController.Get");
            var client = httpClientFactory.CreateClient("ByeServiceClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "bye");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            var message = $@"frontservice {DateTime.Now.ToLongTimeString()} says --> {responseString}";

            return Ok(message);
        }
    }
}
