using ESourcing.Core.Common;
using ESourcing.Core.ResultModels;
using ESourcing.UI.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ESourcing.UI.Clients
{
    public class AuctionClient
    {
        public HttpClient _client { get; }
        public AuctionClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(CommonInfo.LocalAuctionBaseAddress);
        }

        public async Task<Result<AuctionViewModel>> CreateAuction(AuctionViewModel model)
        {
            string dataAsString = JsonConvert.SerializeObject(model);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("/api/v1/Auction", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuctionViewModel>(responseData);
                if (result != null)
                    return new Result<AuctionViewModel>(true, ResultConstat.RecordCreateSuccessfully, result);
                else
                    return new Result<AuctionViewModel>(false, ResultConstat.RecordNotCreateSuccessfully);
            }
             return new Result<AuctionViewModel>(false, ResultConstat.RecordNotCreateSuccessfully);
;        }
    }
}
