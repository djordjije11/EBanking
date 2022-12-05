using Newtonsoft.Json;

namespace EBanking.Services.HttpClients.Helper
{
    internal class HelperMethods
    {
        public static async Task<T?> GetEntityFromHttpResponse<T>(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new Exception(responseBody);
            }
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
        public static async Task<IEnumerable<T>?> GetEntitiesFromHttpResponse<T>(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new Exception(responseBody);
            }
            return JsonConvert.DeserializeObject<IEnumerable<T>?>(responseBody);
        }
    }
}
