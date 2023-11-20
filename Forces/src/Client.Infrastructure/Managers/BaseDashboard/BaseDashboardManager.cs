using Forces.Application.Features.BaseDashboard.GetData;
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

namespace Forces.Client.Infrastructure.Managers.BaseDashboard
{
    public class BaseDashboardManager : IBaseDashboardManager
    {
        private readonly HttpClient _httpClient;

        public BaseDashboardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<BaseDashboardDataResponse>> GetDataAsync(int BaseId)
        {
            var response = await _httpClient.GetAsync(Routes.BaseDashboardEndPoints.GetDataEndpoint(BaseId));
            var data = await response.ToResult<BaseDashboardDataResponse>();
            return data;
        }

    }
}
