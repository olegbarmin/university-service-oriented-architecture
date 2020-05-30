using Newtonsoft.Json;
using StudentsClient.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StudentsClient
{
    public static class StudentsApi
    {
        private static HttpClient HttpClient = new HttpClient();

        private static readonly string baseUrl = "https://localhost:44313";
        private static readonly string allSudentsUrl = baseUrl + "/GetAllStudents";
        private static readonly string filteredSudentsUrl = baseUrl + "/GetStudentsFilteredByAverageMark";

        public static async Task<Student[]> GetAllStudents()
        {
            string jsonResponse = await Get(allSudentsUrl);
            Student[] result = ParseStudentsArrayFrom(jsonResponse);
            return result;
        }

        public static async Task<Student[]> GetStudentsFilteredByAverageMark(float lowerBound, float upperBound)
        {
            var body = new StringContent("{ Item1: " + lowerBound + ", Item2: " + upperBound + " }");
            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string jsonResponse = await Post(filteredSudentsUrl, body);
            Student[] result = ParseStudentsArrayFrom(jsonResponse);
            return result;
        }

        private static async Task<string> Post(string url, HttpContent body)
        {
            var requestMessage = buildRequestMessage(url, HttpMethod.Post);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = body;
            return await sendRequestMessage(requestMessage);
        }

        private static async Task<string> Get(string url)
        {
            var requestMessage = buildRequestMessage(url, HttpMethod.Get);
            string jsonResponse = await sendRequestMessage(requestMessage);
            return jsonResponse;
        }

        private static async Task<string> sendRequestMessage(HttpRequestMessage requestMessage)
        {
            var response = await HttpClient.SendAsync(requestMessage).ConfigureAwait(true);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            requestMessage.Dispose();
            return responseBody;
        }


        private static HttpRequestMessage buildRequestMessage(string url, HttpMethod method)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, url);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return requestMessage;
        }

        private static Student[] ParseStudentsArrayFrom(string json)
        {
            return JsonConvert.DeserializeObject<Student[]>(json);
        }
    }
}
