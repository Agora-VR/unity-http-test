using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class RestClient
{
    public static async Task<string> GetJsonString(string url)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync(url);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            string resourceString = await response.Content.ReadAsStringAsync();

            return resourceString;
        }
    }

    public static async Task<string> PostJsonString<T>(string url, T payload)
    {
        string payloadString = JsonConvert.SerializeObject(payload);

        var content = new StringContent(payloadString, Encoding.UTF8, "application/json");

        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync(url, content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
