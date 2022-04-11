using ESourcing.Core.Common;
using ESourcing.Core.ResultModels;
using ESourcing.UI.ViewModel;
using Newtonsoft.Json;

namespace ESourcing.UI.Clients
{
    public class ProductClient
    {
        public HttpClient _client { get; }//sadece get yazmak = readonly 
        public ProductClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(CommonInfo.LocalProductBaseAddress);
        }

        public async Task<Result<List<ProductViewModel>>> GetProducts()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/Product");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                List<ProductViewModel> result = JsonConvert.DeserializeObject<List<ProductViewModel>>(responseData);
                if (result.Any())
                    return new Result<List<ProductViewModel>>(true, ResultConstat.RecordFound, result.ToList());
                return new Result<List<ProductViewModel>>(false, ResultConstat.RecordNotFound);
            }
            return new Result<List<ProductViewModel>>(false, ResultConstat.RecordNotFound);
        }
    }
}
