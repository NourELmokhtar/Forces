using Forces.Application.Features.InventoryDashboard.GetData;
using Forces.Application.Features.Dashboards.Queries.GetData;
using Forces.Client.Infrastructure.Extensions;
using Forces.Client.Infrastructure.Managers.Dashboard;
using Forces.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Client.Infrastructure.Managers.InventoryDashboard
{
    public class InventoryDashboardManager : IInventoryDashboardManager
    {
        private readonly HttpClient _httpClient;

        public InventoryDashboardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<InventoryDashboardDataResponse>> GetDataAsync(int InventoryId)
        {
            var response = await _httpClient.GetAsync(Routes.InventoryDashboardEndPoints.GetDataEndpoint(InventoryId));
            var data = await response.ToResult<InventoryDashboardDataResponse>();
            return data;
        }

    }
}
