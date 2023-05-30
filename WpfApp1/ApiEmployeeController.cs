using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ExampleRESTFULLAPI.ApiControllers;
using ExampleRESTFULLAPI.Models;
using System;
using System.Text;

namespace ExampleRESTFULLAPI.Controllers
{
    public class ApiEmployeeController : Controller , IApiEmployeeController
    {
        private readonly IConfiguration _config;
        HttpClient httpClient;
        string requestUri;
        public ApiEmployeeController(IConfiguration configuration)
        {
            _config = configuration;
             httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = 
            //    new AuthenticationHeaderValue("bearer", _config.GetValue<string>("ApiSettings:AccessToken"));
            //   requestUri = _config.GetValue<string>("ApiSettings:BaseAddress");

            httpClient.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("bearer", "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023");
            requestUri = "https://gorest.co.in/public/v2/users";
        }

        /// <summary>
        /// Get All employee details
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeDetails>> GetAllEmployeeAsync()
        {

            IEnumerable<EmployeeDetails> employees = null;
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false); ;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    employees = JsonSerializer.Deserialize<IEnumerable<EmployeeDetails>>(json);
                }
            }
            catch (Exception ex)

            {
               
            }
            return employees;
        }

        /// <summary>
        /// Get employee deatils by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
         public async Task<IEnumerable<EmployeeDetails>> GetEmployeeByFirstNameAsync(string name)
        {
            IEnumerable<EmployeeDetails> employees = null;

            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri + "?name=" + name);

            if (responseMessage.IsSuccessStatusCode)
            {
                string json = await responseMessage.Content.ReadAsStringAsync();
                employees = JsonSerializer.Deserialize<IEnumerable<EmployeeDetails>>(json);
            }
            else
            {
                //request fail
            }
            return employees;
        }

        /// <summary>
        /// Get Employee Details by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmployeeDetails> GetEmployeeByIdAsync(int id)
        {
            EmployeeDetails employee = new EmployeeDetails();
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri + "/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                string json = await responseMessage.Content.ReadAsStringAsync();
                employee = JsonSerializer.Deserialize<EmployeeDetails>(json);
            }
            else
            {
                //request fail
            }

            return employee;
        }

        /// <summary>
        /// Save employee Details
        /// </summary>
        /// <param name="emp"></param>
        public async Task PostEmployeeDetailsAsync(EmployeeDetails emp)
        {
            httpClient.DefaultRequestHeaders.Add("access-control-allow-methods", "POST");
            
            EmployeeDetails employee = new EmployeeDetails();
            string jsonData = JsonSerializer.Serialize(emp);
            HttpContent requestContent = new StringContent(jsonData, Encoding.UTF8);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            HttpResponseMessage response = await httpClient.PostAsync(requestUri, requestContent).ConfigureAwait(false); ;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                employee = JsonSerializer.Deserialize<EmployeeDetails>(json);
            }
            else
            {
                //request fail
            }
        }

        /// <summary>
        /// Update employee Details
        /// </summary>
        /// <param name="emp"></param>
        public void UpdateEmployeeDetails(EmployeeDetails emp)
        {
            string jsonData = JsonSerializer.Serialize(emp);
            HttpContent requestContent = new StringContent(jsonData);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = httpClient.PutAsync(requestUri + "/" + emp.id, requestContent);
        }

        /// <summary>
        /// Delete employee Details
        /// </summary>
        /// <param name="emp"></param>
        public async Task<EmployeeDetails> DeleteEmployeeDetails(EmployeeDetails emp)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(requestUri+ "/" + emp.id);
            EmployeeDetails employee = new EmployeeDetails();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                employee = JsonSerializer.Deserialize<EmployeeDetails>(json);
            }
            else
            {
                //request fail
            }
            return employee;
        }
    }
}
