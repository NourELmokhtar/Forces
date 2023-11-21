using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Client.Infrastructure.Routes
{
    public class BaseDashboardEndPoints
    {
        public static string GetDataEndpoint(int BaseId)
        {
            return $"api/v1/BaseDashboard/BaseId?BaseId={BaseId}";
            
        }
    }
}
