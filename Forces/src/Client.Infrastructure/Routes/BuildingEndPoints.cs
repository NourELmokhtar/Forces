using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Client.Infrastructure.Routes
{
    public class BuildingEndPoints
    {
        public static string Save = "api/v1/Building";
        public static string GetAll = "api/v1/Building";
        public static string GetAllBuildings = "api/v1/Building/GetAllBuildings";
        public static string Delete(int Id) => $"api/v1/Building/{Id}";
        public static string GetBuildingById(int Id) => $"api/v1/Building/{Id}";
    }
}
