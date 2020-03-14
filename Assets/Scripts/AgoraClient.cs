using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class AgoraClient
{
    // For running on the deployed server
    private static string apiUrl = "https://agoravr.online/api";
    // For running a local deployment
    // private static string apiUrl = "http://localhost:8080"; //

    public string token;

    public static async Task<HttpResponseMessage> PostJson<T>(string url, T payload)
    {
        var payloadString = JsonConvert.SerializeObject(payload);

        var content = new StringContent(payloadString, Encoding.UTF8, "application/json");

        using (var httpClient = new HttpClient())
        {
            return await httpClient.PostAsync(url, content);
        }
    }

    public async Task<HttpResponseMessage> GetJsonWithAuth(string endpoint)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);

            return await client.GetAsync(apiUrl + endpoint);
        }
    }

    public async Task<HttpResponseMessage> PostJsonWithAuth<T>(string url, T payload)
    {
        var payloadString = JsonConvert.SerializeObject(payload);

        var content = new StringContent(payloadString, Encoding.UTF8, "application/json");

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);

            return await client.PostAsync(url, content);
        }
    }

    public async Task<string> authenticate(string username, string password)
    {
        var postPayload = new Dictionary<string, object>();

        postPayload.Add("user_name", username);
        postPayload.Add("user_pass", password);

        var response = await PostJson(apiUrl + "/authenticate", postPayload);

        var responseString = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK)
            token = responseString;

        return responseString;
    }
}