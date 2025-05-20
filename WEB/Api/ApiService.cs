using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;

namespace Web.Api
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET method
        public async Task<T> GetDataAsync<T>(string url)
        {
            try 
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                var errorMessage = await GetErrorMessage(ex);
                throw new Exception($"Lỗi khi gọi GET {url}: {errorMessage}");
            }
        }

        // POST method
        public async Task<T> PostDataAsync<T>(string url, object data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, data);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API trả về lỗi: {response.StatusCode} - {errorContent}");
                }

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                var errorMessage = await GetErrorMessage(ex);
                throw new Exception($"Lỗi khi gọi POST {url}: {errorMessage}");
            }
        }

        private async Task<string> GetErrorMessage(Exception ex)
        {
            if (ex is HttpRequestException httpEx && httpEx.InnerException != null)
            {
                return httpEx.InnerException.Message;
            }
            else if (ex is JsonException jsonEx)
            {
                return "Lỗi khi xử lý dữ liệu JSON: " + jsonEx.Message;
            }
            return ex.Message;
        }

        public async Task<T> PostDataFormAsync<T>(string url, MultipartFormDataContent data)
        {
            try
            {
                var response = await _httpClient.PostAsync(url, data);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API trả về lỗi: {response.StatusCode} - {errorContent}");
                }

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                var errorMessage = await GetErrorMessage(ex);
                throw new Exception($"Lỗi khi gọi POST Form {url}: {errorMessage}");
            }
        }

        // PUT method
        public async Task<T> PutDataAsync<T>(string url, object data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(url, data);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API trả về lỗi: {response.StatusCode} - {errorContent}");
                }

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                var errorMessage = await GetErrorMessage(ex);
                throw new Exception($"Lỗi khi gọi PUT {url}: {errorMessage}");
            }
        }

        // PUT method
        public async Task<T> PutDataFormAsync<T>(string url, MultipartFormDataContent data)
        {
            try
            {
                var response = await _httpClient.PutAsync(url, data);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API trả về lỗi: {response.StatusCode} - {errorContent}");
                }

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                var errorMessage = await GetErrorMessage(ex);
                throw new Exception($"Lỗi khi gọi PUT Form {url}: {errorMessage}");
            }
        }

        // DELETE method
        public async Task<bool> DeleteDataAsync(string url)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API trả về lỗi: {response.StatusCode} - {errorContent}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                var errorMessage = await GetErrorMessage(ex);
                throw new Exception($"Lỗi khi gọi DELETE {url}: {errorMessage}");
            }
        }
    }
}
