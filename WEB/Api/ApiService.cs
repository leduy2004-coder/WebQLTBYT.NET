using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        // POST method
        public async Task<T> PostDataAsync<T>(string url, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {(int)response.StatusCode} {response.ReasonPhrase} \nDetails: {content}");
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<T> PostDataFormAsync<T>(string url, MultipartFormDataContent data)
        {
            // Gửi yêu cầu POST với multipart/form-data
            var response = await _httpClient.PostAsync(url, data);

            // Kiểm tra xem phản hồi có thành công không
            response.EnsureSuccessStatusCode();

            // Đọc nội dung trả về và chuyển đổi thành kiểu T
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // PUT method
        public async Task<T> PutDataAsync<T>(string url, object data)
        {
            var response = await _httpClient.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        // PUT method
        public async Task<T> PutDataFormAsync<T>(string url, MultipartFormDataContent data)
        {
            var response = await _httpClient.PutAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        // DELETE method
        public async Task<bool> DeleteDataAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
