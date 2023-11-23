using Forces.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Application.Features.InventoryDashboard.GetData
{
    public class InventoryDashboardDataResponse
    {

        public string? InventoryName { get; set; }
        public string? HouseName { get; set; }
        public string? BaseSectionName { get; set; }
        public int? RoomNumber{ get; set; }
        public string? BaseName { get; set; }
        public string? BuildingName { get; set; }
        public List<ItemsData> Item{ get; set; }
        public List<PersonData> Persons{ get; set; }
        

        
    }
    public class ItemsData
    {
        public string ItemName { get; set; }
        public string ItemNSN { get; set; }
        public string ItemClass { get; set; }
        public string ItemCode { get; set; }
        public string ItemSerial { get; set; }
    }

    public class PersonData
    {
        public string Name { get; set; }
        public string NationalNumber { get; set; }
        public string Section { get; set; }
        public int RoomNumber { get; set; }
        public string BuildingName { get; set; }
        public string Phone { get; set; }
        public string OfficePhone { get; set; }
        public string Rank { get; set; }
    }
}
