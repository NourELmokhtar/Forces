using FluentValidation;
using Forces.Application.Enums;
using Forces.Application.Features.Dashboards.Queries.GetData;
using Forces.Application.Responses.VoteCodes;
using Forces.Client.Infrastructure.Managers.InventoryDashboard;
using Forces.Client.Infrastructure.Managers.VoteCodes;
using Forces.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Claims;
using Forces.Client.Infrastructure.Managers.Inventory;
using Forces.Application.Features.Inventory.Queries.GetAll;
using DevExpress.XtraRichEdit.Model;
using Forces.Application.Models;
using Forces.Application.Features.InventoryDashboard.GetData;
using Forces.Shared.Wrapper;

namespace Forces.Client.Pages.Content
{
    public partial class InventoryDashboards
    {
        [Inject] private IInventoryDashboardManager InventoryDashboardManager { get; set; }
        [Inject] private IInventoryManager InventorysManager { get; set; }
        private List<GetAllInventoriesResponse> _InventorysList = new();
        private IResult<InventoryDashboardDataResponse> response { get; set; }

        [Inject] private IVoteCodesManager _voteCodesmanager { get; set; }
        public List<VoteCodeResponse> voteCodesList { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        public string SelectedInventory { get; set; }
        public string BuildingName { get; set; }
        public string BaseSectionName { get; set; }
        public string BaseName { get; set; }
        public int? RoomNumber { get; set; }
        public string HouseName { get; set; }
        public string InventoryName { get; set; }
        public List<ItemsData> Item { get; set; }
        public List<PersonData> Person { get; set; }

        private readonly string[] _dataEnterBarChartXAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private readonly List<ChartSeries> _dataEnterBarChartSeries = new();
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            //await LoadDataAsync();
            await GetInventorysAsync();
            await LogToConsole();
            /*SelectedInventory = "SEEB";
            await GetInventoryData();*/

            _loaded = true;
        }
        private async Task GetInventorysAsync()
        {
            var response = await InventorysManager.GetAllAsync();
            if (response.Succeeded)
            {
                _InventorysList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, MudBlazor.Severity.Error);
                }
            }
        }

        private int? converterForInventorys(string ss)
        {
            return _InventorysList.FirstOrDefault(s => s.Name == ss).Id;
        }
    }
}
