public class RestClient
{
    private readonly HttpClient client;

    public RestClient()
    {
        client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5001");
    }
    public async Task<HttpContent> _get(string url)
    {   
        var response =  this.client.GetAsync(url).Result;
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