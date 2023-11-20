
using FluentValidation;
using Forces.Application.Enums;
using Forces.Application.Features.Dashboards.Queries.GetData;
using Forces.Application.Responses.VoteCodes;
using Forces.Client.Infrastructure.Managers.BaseDashboard;
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
        [Inject] private IVoteCodesManager _voteCodesmanager { get; set; }
        public List<VoteCodeResponse> voteCodesList { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        public int SelectedBase { get; set; }
        public int OfficeCount { get; set; }
        public int InventoryItemCount { get; set; }
        public int InventoryCount { get; set; }
        public int BuildingCount { get; set; }
        public int PersonCount { get; set; }
        public int RoomCount { get; set; }
        public int HouseCount { get; set; }
        public int ForcesCount { get; set; }
        public int BasesSectionsCount { get; set; }

        private readonly string[] _dataEnterBarChartXAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private readonly List<ChartSeries> _dataEnterBarChartSeries = new();
        private bool _loaded;
        private UserType userType;
        private ClaimsPrincipal _currentUser;
        private bool _canViewItems;
        private bool _canViewMeasureUnits;
        private bool _canViewDepo;
        private bool _canViewHQ;
        private bool _canViewVoteCodes;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            
            HubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri(ApplicationConstants.SignalR.HubUrl))
            .Build();
            HubConnection.On(ApplicationConstants.SignalR.ReceiveUpdateBaseDashboard, async () =>
            {
                await LoadDataAsync();
                if (userType == UserType.VoteHolder)
                {
                    await GetVoteCodes();
                }
            });
            await HubConnection.StartAsync();
            if (userType == UserType.VoteHolder)
            {
                await GetVoteCodes();
            }
            _loaded = true;
        }
        private async Task GetVoteCodes()
        {
            var response = await _voteCodesmanager.GetAllByCurrentUser();
            if (response.Succeeded)
            {
                voteCodesList = response.Data.Where(x => x.IsPrimery).ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, (MudBlazor.Severity)Severity.Error);
                }
            }
        }
        private async Task LoadDataAsync()
        {
            var userTypeResponse = await _userManager.GetUserType();
            userType = userTypeResponse.Data;

            var response = await BaseDashboardManager.GetDataAsync(SelectedBase);
            if (response.Succeeded)
            {
                BasesSectionsCount = response.Data.BasesSectionsCount;
                HouseCount = response.Data.HouseCount;
                RoomCount = response.Data.RoomCount;
                PersonCount = response.Data.PersonCount;
                BuildingCount = response.Data.BuildingCount;
                InventoryCount = response.Data.InventoryCount;
                InventoryItemCount = response.Data.InventoryItemCount;
                OfficeCount = response.Data.OfficeCount;

               
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, (MudBlazor.Severity)Severity.Error);
                }
            }
        }
    }
}