using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Application.Features.BaseDashboard.GetData
{
    public class BaseDashboardDataResponse
    {
        public int BuildingCount { get; set; }
        public int HouseCount { get; set; }
        public int InventoryCount { get; set; }
        public int InventoryItemCount { get; set; }
        public int BasesSectionsCount { get; set; }
        public int RoomCount { get; set; }
        public int PersonCount { get; set; }
        public int OfficeCount { get; set; }
        public int FullRoomsCount { get; set; }
        public int EmptyRoomsCount { get; set; }
    }
}
