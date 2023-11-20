using Forces.Application.Features.BaseDashboard.GetData;
using Forces.Application.Features.Dashboards.Queries.GetData;
using Forces.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Client.Infrastructure.Managers.BaseDashboard
{
    public interface IBaseDashboardManager : IManager
    {
        Task<IResult<BaseDashboardDataResponse>> GetDataAsync(int BaseId);

    }
}
