using AccessAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AccessAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.customersAsync(Program.getAccessToken());
            Console.ReadKey();
        }


        static string getAccessToken()
        {
            string token = string.Empty;
            string loginURL = "YourHost/api/API_Auth/Login";

            LoginModel user = new LoginModel();
            user.UserName = "your username";
            user.Password = "your password";
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            //Use this to prevent "The SSL connection could not be established" error
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            HttpClient client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var response = client.PostAsync(loginURL, new StringContent(jsonUser, System.Text.Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonToken = response.Content.ReadAsStringAsync().Result;
                JWTTokenResponse tokenResponse = JsonConvert.DeserializeObject<JWTTokenResponse>(jsonToken);
                token = tokenResponse.access_token;
            }
            return token;
        }

        static async void customersAsync(string accessToken)
        {

            //Use this to prevent "The SSL connection could not be established" error
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            var url = "https://10.70.66.97:11448/api/customers/customers";
            HttpClient client = new HttpClient(clientHandler);
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = HttpMethod.Get;
            requestMessage.RequestUri = new Uri(url);
            requestMessage.Headers.Add("Authorization", "bearer " + accessToken);            
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("ContentType", "application/json");

            HttpResponseMessage response = client.SendAsync(requestMessage).Result;
            string body = await response.Content.ReadAsStringAsync();
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(body);
            foreach(var customer in customers)
            {
                string content = string.Format("FirstName: {0} - Lastname: {1} - Phone: {2} - CompanyName: {3}",customer.FirstName,customer.LastName,customer.Phone,customer.CompanyName);
                Console.WriteLine(content);
            }            
            Console.ReadLine();
        }
    }
}
