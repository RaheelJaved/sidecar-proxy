using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace frontservice_dotnet
{
    
    public class MyHttpHandler : DelegatingHandler
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        public MyHttpHandler(IConfiguration configuration, ILogger<MyHttpHandler> logger) {
            this.configuration = configuration;
            this.logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)       
        {

            logger.LogInformation($"***********Executing MyHttpHandler.SendAsync()");
            //request.Headers.Add("X-Custom-Header", "CustomValue");

            logger.LogInformation("-----before----");
            logger.LogInformation($"RequestUri: {request.RequestUri}");
            DumpHeaders(request);
            UseProxy(request, configuration);
            logger.LogInformation("-----after----");
            logger.LogInformation($"RequestUri: {request.RequestUri}");
            DumpHeaders(request);
            logger.LogInformation($"***********Finished executing MyHttpHandler.SendAsync()");

            return await base.SendAsync(request, cancellationToken);
        }

        private void UseProxy(HttpRequestMessage request, IConfiguration configuration)
        {
            ProxySetting proxySetting = new ProxySetting();

            configuration.GetSection("Proxy").Bind(proxySetting);

            if (proxySetting?.Enabled == false) return;

            string proxyAddress = proxySetting?.Address ?? string.Empty;

            var host = request.RequestUri.Host;
            var path = request.RequestUri.AbsolutePath;
            var port = request.RequestUri.Port;
            var query = request.RequestUri.Query;
            var pathAndQuery = request.RequestUri.PathAndQuery;
            var hostWithPort = $"{host}:{port}";

            string proxyWithPath = $"{proxyAddress}{request.RequestUri.PathAndQuery}";
            request.RequestUri = new System.Uri(proxyWithPath);
            request.Headers.Add("X-Fwd-Host", host);
            request.Headers.Add("X-Fwd-Path", path);
            request.Headers.Add("X-Fwd-Port", port.ToString());
            request.Headers.Add("X-Fwd-Query", query);
            request.Headers.Add("X-Fwd-PathAndQuery", pathAndQuery);
            request.Headers.Add("X-Fwd-HostWithPort", hostWithPort);
            //DumpHeaders(request);
            //logger.LogInformation($"*************{Environment.NewLine}****************{Environment.NewLine}After setting RequestUri to proxy, replacing the host to {host}{Environment.NewLine}*************{Environment.NewLine}****************{Environment.NewLine}");
            //request.Headers.Add("Host", host);
            //request.Headers.Add("Port", port.ToString());
        }

        private void DumpHeaders(HttpRequestMessage request)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine($"Headers...\r\n{request.Headers}");
            sb.AppendLine($"RequestUri: {request.RequestUri.ToString()}");
            sb.AppendLine($"AbsoluteUri: {request.RequestUri.AbsoluteUri}");
            sb.AppendLine($"AbsolutePath: {request.RequestUri.AbsolutePath}");
            sb.AppendLine($"Host: {request.RequestUri.Host}");
            sb.AppendLine($"DnsSafeHost: {request.RequestUri.DnsSafeHost}");
            sb.AppendLine($"PathAndQuery: {request.RequestUri.PathAndQuery}");
            sb.AppendLine($"Segments: {request.RequestUri.Segments.ToString()}");
            sb.AppendLine($"Port: {request.RequestUri.Port}");
            sb.AppendLine($"Scheme: {request.RequestUri.Scheme}");


            logger.LogInformation(sb.ToString());

            //logger.LogInformation($"Headers...\r\n{request.Headers}");
            //logger.LogInformation($"RequestUri: {request.RequestUri.ToString()}");
            //logger.LogInformation($"AbsoluteUri: {request.RequestUri.AbsoluteUri}");
            //logger.LogInformation($"AbsolutePath: {request.RequestUri.AbsolutePath}");
            //logger.LogInformation($"Host: {request.RequestUri.Host}");
            //logger.LogInformation($"DnsSafeHost: {request.RequestUri.DnsSafeHost}");
            //logger.LogInformation($"PathAndQuery: {request.RequestUri.PathAndQuery}");
            //logger.LogInformation($"Segments: {request.RequestUri.Segments.ToString()}");
            //logger.LogInformation($"Port: {request.RequestUri.Port}");
            //logger.LogInformation($"Scheme: {request.RequestUri.Scheme}");


        }
    }

    public class ProxySetting
    {
        public bool Enabled { get; set; }
        public string Address { get; set; }
    }
}



