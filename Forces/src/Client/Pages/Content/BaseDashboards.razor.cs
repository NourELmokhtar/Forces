using FluentValidation;
using Forces.Application.Enums;
using Forces.Application.Features.Bases.Queries.GetAll;
using Forces.Application.Features.Dashboards.Queries.GetData;
using Forces.Application.Responses.VoteCodes;
using Forces.Client.Infrastructure.Managers.BaseDashboard;
using Forces.Client.Infrastructure.Managers.BasicInformation.Bases;
using Forces.Client.Infrastructure.Managers.VoteCodes;
using Forces.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Claims;

namespace Forces.Client.Pages.Content
{
    public partial class BaseDashboards
    {
        [Inject] private IBaseDashboardManager BaseDashboardManager { get; set; }
        [Inject] private IBaseManager BasesManager { get; set; }
        private List<GetAllBasesResponse> _BasesList = new();

        [Inject] private IVoteCodesManager _voteCodesmanager { get; set; }
        public List<VoteCodeResponse> voteCodesList { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        public string SelectedBase { get; set; }
        public int OfficeCount { get; set; }
        public int InventoryItemCount { get; set; }
        public int InventoryCount { get; set; }
        public int BuildingCount { get; set; }
        public int PersonCount { get; set; }
        public int RoomCount { get; set; }
        public int HouseCount { get; set; }
        public int ForcesCount { get; set; }
        public int BasesSectionsCount { get; set; }
        public int EmptyRoomsCount { get; set; }
        public int FullRoomsCount { get; set; }

        private readonly string[] _dataEnterBarChartXAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private readonly List<ChartSeries> _dataEnterBarChartSeries = new();
        private bool _loaded;
       
        protected override async Task OnInitializedAsync()
        {
            //await LoadDataAsync();
            await GetBasesAsync();
            /*SelectedBase = "SEEB";
            await GetBaseData();*/
        
            _loaded = true;
        }
        private async Task GetBasesAsync()
        {
            var response = await BasesManager.GetAllAsync();
            if (response.Succeeded)
            {
                _BasesList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, MudBlazor.Severity.Error);
                }
            }
        }
        
        private int? converterForBases(string ss)
        {
            return _BasesList.FirstOrDefault(s => s.BaseName == ss).Id;
        }
    }
}
