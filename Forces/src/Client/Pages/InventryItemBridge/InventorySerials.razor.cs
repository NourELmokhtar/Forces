using Blazored.FluentValidation;
using Forces.Application.Features.Inventory.Queries.GetAll;
using Forces.Application.Features.InventoryItemBridge.Commands.AddEdit;
using Forces.Client.Extensions;
using Forces.Client.Infrastructure.Managers.Inventory;
using Forces.Client.Infrastructure.Managers.InventoryItem;
using Forces.Client.Infrastructure.Managers.InventoryItemBridge;
using Forces.Client.Infrastructure.Managers.Items;
using Forces.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace Forces.Client.Pages.InventryItemBridge
{
    public partial class InventorySerials
    {
        [Inject] private IInventoryItemBridgeManager InventoryItemBridgeManager { get; set; }
        [Inject] private IInventoryManager InventoryManager { get; set; }
        private List<GetAllInventoriesResponse> _InventoryList = new();
        private List<string> _SerialsList = new();

        [Parameter] public int count { get; set; }
        [Parameter] public int InventoryId { get; set; }
        [Parameter] public int ItemId { get; set; }

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [Inject] IDialogService DialogService { get; set; }


        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            foreach (var serials in SerialNumbers.Where(x => x != "0"))
            {
                AddEditInventoryItemBridgeCommand AddEditInventoryItemBridgeModel = new AddEditInventoryItemBridgeCommand();
                AddEditInventoryItemBridgeModel.InventoryId = InventoryId;
                AddEditInventoryItemBridgeModel.ItemId = ItemId;
                AddEditInventoryItemBridgeModel.SerialNumber = serials;
                AddEditInventoryItemBridgeModel.DateOfEnter = DateTime.Now;

                var response = await InventoryItemBridgeManager.SaveAsync(AddEditInventoryItemBridgeModel);
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

        private int? converterForInventories(string ss)
        {
            return _InventoryList.FirstOrDefault(s => s.Name == ss).Id;
        }


        private async Task LoadDataAsync()
        {

            await Task.CompletedTask;
        }
    }
}
