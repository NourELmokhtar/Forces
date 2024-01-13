using Blazored.FluentValidation;
using Forces.Application.Features.Bases.Queries.GetAll;
using Forces.Application.Features.Person.Commands.AddEdit;
using Forces.Application.Features.Forces.Queries.GetAll;
using Forces.Client.Infrastructure.Managers.BasicInformation.Bases;
using Forces.Client.Infrastructure.Managers.BasicInformation.Forces;
using Forces.Client.Infrastructure.Managers.Person;
using Forces.Shared.Constants.Application;
using Forces.Shared.Constants.Permission;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Forces.Client.Extensions;
using Forces.Client.Infrastructure.Managers.Building;
using Forces.Application.Features.Building.Queries.GetAll;
using Forces.Client.Infrastructure.Managers.Room;
using Forces.Application.Features.Room.Queries.GetAll;
using Forces.Client.Infrastructure.Managers.House;
using Forces.Application.Features.House.Queries.GetAll;
using Forces.Client.Pages.Building;
using Forces.Application.Enums;
using Forces.Client.Pages.House;

namespace Forces.Client.Pages.Person
{
    public partial class AddEditPersonModal
    {
        [Inject] private IPersonManager PersonManager { get; set; }
        [Inject] private IRoomManager RoomManager { get; set; }
        private List<GetAllRoomsResponse> _RoomList = new();
        [Inject] private IHouseManager HouseManager { get; set; }
        private List<GetAllHousesResponse> _HouseList = new();
        private IEnumerable<GetAllRoomsResponse> filteredRooms;
        [Inject] private IBuildingManager BuildingManager { get; set; }
        private List<GetAllBuildingsResponse> _BuildingList = new();

        private List<GetAllForcesResponse> _ForceList = new();
        [Parameter] public AddEditPersonCommand AddEditPersonModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [Inject] private IForceManager ForceManager { get; set; }
        private string selectedBuilding;
        private string BuildingName;
        private int RoomNumber;
        private string selectedHouse;
        private string selectedType;
        private string selectedRank;
        private bool _canCreateBaseSection;
        private bool _canEditBaseSection;
        private bool _canDeleteBaseSection;
        private bool _canSearchBaseSection;
        private ClaimsPrincipal _currentUser;
        private List<string> dropdownItems = new List<string> { "Room", "House"};
        private List<string> RanksList = new List<string> { "JUD", "SNCO", "OC", "OC1" };
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        public void Cancel()
        {
            MudDialog.Cancel();
        }
        private async Task GetForcesAsync()
        {
            var response = await ForceManager.GetAllAsync();
            if (response.Succeeded)
            {
                _ForceList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, MudBlazor.Severity.Error);
                }
            }

        }
        private async Task GetBuildingsAsync()
        {
            var response = await BuildingManager.GetAllAsync();
            if (response.Succeeded)
            {
                _BuildingList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, MudBlazor.Severity.Error);
                }
            }
        }
        private async Task GetRoomsAsync()
        {
            var response = await RoomManager.GetAllAsync();
            if (response.Succeeded)
            {
                _RoomList = response.Data.ToList();
                filteredRooms = _RoomList.Where(x => x.BuildingId == converterForBuildings(BuildingName));
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, MudBlazor.Severity.Error);
                }
            }
        }
        private async Task GetHousesAsync()
        {
            var response = await HouseManager.GetAllAsync();
            if (response.Succeeded)
            {
                _HouseList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, MudBlazor.Severity.Error);
                }
            }
        }

        private async Task SaveAsync()
        {
            if (RoomNumber!=null && RoomNumber!=0)
            {
                AddEditPersonModel.RoomId = (int)converterForRooms();

            }
            else if (selectedHouse!=null && selectedHouse!=string.Empty)
            {
                AddEditPersonModel.HouseId = (int)converterForHouses();
            }
            if (selectedRank == "جنود")
            {
                AddEditPersonModel.Rank = PersonRank.JUD;

            }
            else if (selectedRank == "ضباط الصف")
            {
                AddEditPersonModel.Rank = PersonRank.SNCO;

            }
            else if (selectedRank == "الضباط")
            {
                AddEditPersonModel.Rank = PersonRank.OC;

            }
            else if (selectedRank == "كبار الضباط")
            {
                AddEditPersonModel.Rank = PersonRank.OC1;

            }
            var response = await PersonManager.SaveAsync(AddEditPersonModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], MudBlazor.Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, MudBlazor.Severity.Error);
                }
            }
            await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
        }
        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateBaseSection = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BasicInformations.CreateBasesSection)).Succeeded;
            _canEditBaseSection = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BasicInformations.EditBasesSection)).Succeeded;
            _canDeleteBaseSection = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BasicInformations.DeleteBasesSection)).Succeeded;
            _canSearchBaseSection = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BasicInformations.SearchBasesSection)).Succeeded;

            await LoadDataAsync();

            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }
        Func<int, string> converter()
        {
            return p => $"{_ForceList.FirstOrDefault(x => x.Id == p).ForceName} | {_ForceList.FirstOrDefault(x => x.Id == p).ForceCode}";
        }
        private int? converterForBuildings(string ss)
        {
            return _BuildingList.FirstOrDefault(s => s.BuildingName == ss).Id;
        }
        private int? converterForRooms()
        {
            return _RoomList.FirstOrDefault(s => s.RoomNumber == RoomNumber).Id;
        }
        private int? converterForHouses()
        {
            return _HouseList.FirstOrDefault(s => s.HouseName == selectedHouse).Id;
        }

        private async Task LoadDataAsync()
        {
            await GetBuildingsAsync();
            await GetRoomsAsync();
            await GetForcesAsync();
            await GetHousesAsync();

            await Task.CompletedTask;
        }
    }
}
