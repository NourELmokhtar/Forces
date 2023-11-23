using Forces.Application.Features.InventoryDashboard.GetData;
using Forces.Application.Features.Dashboards.Queries.GetData;
using Forces.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Client.Infrastructure.Managers.InventoryDashboard
{
    public interface IInventoryDashboardManager : IManager
    {
        Task<IResult<InventoryDashboardDataResponse>> GetDataAsync(int InventoryId);

    }
}
