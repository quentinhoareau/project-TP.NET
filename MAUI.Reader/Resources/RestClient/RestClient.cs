public class RestClient
{
    private readonly HttpClient client;

    public RestClient()
    {
        var handler = new HttpClientHandler()
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        
        client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://localhost:5001");
    }
    public async Task<HttpContent> _get(string url)
    {   
        var response =  await this.client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
           return response.Content;
           
        }

        return null; 
    }

    public async Task<HttpContent> _post(string url, HttpContent content)
    {
        var response =  this.client.PostAsync(url, content).Result;
        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        return null;
    }

    public HttpClient getInstance()
    {
        return this.client;
    }
}