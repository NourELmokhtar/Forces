using Blazored.FluentValidation;
using FluentValidation;
using Forces.Application.Features.Forces.Queries.GetAll;
using Forces.Application.Features.Inventory.Commands.AddEdit;
using Forces.Client.Extensions;
using Forces.Client.Infrastructure.Managers.BasicInformation.Forces;
using Forces.Client.Infrastructure.Managers.Inventory;
using Forces.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace Forces.Client.Pages.Inventory
{
    public partial class AddEditInventoryModal
    {
        [Inject] private IInventoryManager InventoryManager { get; set; }
        private List<GetAllForcesResponse> _ForceList = new();
        [Parameter] public AddEditInventoryCommand AddEditInventoryModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [Inject] private IForceManager ForceManager { get; set; }



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
        private async Task SaveAsync()
        {
            var response = await InventoryManager.SaveAsync(AddEditInventoryModel);
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
        private async Task LoadDataAsync()
        {
            await GetForcesAsync();
            
            await Task.CompletedTask;
        }
    }
}
