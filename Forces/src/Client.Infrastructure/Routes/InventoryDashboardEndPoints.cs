using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Client.Infrastructure.Routes
{
    public class InventoryDashboardEndPoints
    {
        public static string GetDataEndpoint(int InventoryId)
        {
            return $"api/v1/InventoryDashboard/InventoryId?InventoryId={InventoryId}";

        }
    }
}
