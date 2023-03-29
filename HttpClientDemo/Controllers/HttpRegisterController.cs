using HttpClientDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HttpClientDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpRegisterController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public HttpRegisterController(HttpClient client)
        {
            _httpClient = client;
            _httpClient.BaseAddress = new Uri("https://localhost:7027/");
        }

        [HttpGet]
        public async Task<ActionResult> GetRegisterDetails()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = await _httpClient.GetAsync($"api/Customer/GetAllCustomer");
                result.EnsureSuccessStatusCode();
                var responseContent = result.Content.ReadAsStringAsync().Result;

                return Ok(responseContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetDetailsByAccountNumber")]
        public async Task<ActionResult> GetRegisterByIdDetails(long AccountNumber)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Result = await _httpClient.GetAsync($"api/Customer/GetCustomerById?AccountNumber={AccountNumber}");
                if (Result.StatusCode == HttpStatusCode.BadRequest)
                {
                    return Problem(statusCode: 400, detail: $"Unable to find Account Number");
                }
                Result.EnsureSuccessStatusCode();
                var ResponseContent = Result.Content.ReadAsStringAsync().Result;
                return Ok(ResponseContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost("Create Account")]
        public async Task<ActionResult> Create(Register model)
        {
          try
          {
           var Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
           _httpClient.DefaultRequestHeaders.Clear();
           var Result = await _httpClient.PostAsync($"api/Customer/CreateCustomer", Content);

           if (string.IsNullOrEmpty(model.Email))
              return Problem(statusCode: 400, detail: $"Email cannot be empty");
           if (string.IsNullOrEmpty(model.Name))
              return Problem(statusCode: 400, detail: $"Name cannot be empty");
           if (string.IsNullOrEmpty(model.Password))
              return Problem(statusCode: 400, detail: $"Password cannot be empty");

               Result.EnsureSuccessStatusCode();
               var ResponseContent = Result.Content.ReadAsStringAsync().Result;
               return Ok(ResponseContent);
          }
          catch (Exception ex)
          {
            return StatusCode(500, ex);
          }

        }
        
    }
}
