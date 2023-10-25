using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Client.Infrastructure.Routes
{
    public class HouseEndPoints
    {
        public static string Save = "api/v1/House";
        public static string GetAll = "api/v1/House";
        public static string GetAllSections = "api/v1/House/GetAllHouses";
        public static string Delete(int Id) => $"api/v1/House/{Id}";
        public static string GetHouseById(int Id) => $"api/v1/House/{Id}";
    }
}
