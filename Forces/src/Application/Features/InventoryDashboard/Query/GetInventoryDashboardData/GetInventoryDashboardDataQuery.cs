/*using Forces.Application.Features.BaseDashboard.GetData;
using Forces.Application.Features.BaseDashboards.Queries.GetData;
using Forces.Application.Interfaces.Repositories;
using Forces.Application.Interfaces.Services.Identity;
using Forces.Application.Interfaces.Services;
using Forces.Application.Models;
using Forces.Domain.Entities.Catalog;
using Forces.Domain.Entities.ExtendedAttributes;
using Forces.Domain.Entities.Misc;
using Forces.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.InventoryDashboard.Query.GetInventoryDashboardData
{
    public class GetInventoryDashboardDataQuery : IResult<Result<GetInventoryDashboardDataResponse>>
    {
        public int InventoryId { get; set; }

    }
    internal class GetInventoryDashboardDataQueryHandler : IRequestHandler<GetInventoryDashboardDataQuery, Result<GetInventoryDashboardDataResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IStringLocalizer<GetInventoryDashboardDataQueryHandler> _localizer;
        private readonly IVoteCodeService _voteCodeService;

        public GetBaseDashboardDataQueryHandler(IUnitOfWork<int> unitOfWork, IUserService userService, IRoleService roleService, IStringLocalizer<GetBaseDashboardDataQueryHandler> localizer, IVoteCodeService voteCodeService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _roleService = roleService;
            _localizer = localizer;
            _voteCodeService = voteCodeService;
        }

        public async Task<Result<BaseDashboardDataResponse>> Handle(GetBaseDashboardDataQuery query, CancellationToken cancellationToken)
        {


            var response = new BaseDashboardDataResponse
            {
                BasesSectionsCount = await _unitOfWork.Repository<BasesSections>().Entities.Where(x => x.BaseId == query.BaseId).CountAsync(cancellationToken),
                BuildingCount = await _unitOfWork.Repository<Models.Building>().Entities.Where(x => x.BaseId == query.BaseId).CountAsync(cancellationToken),
                HouseCount = await _unitOfWork.Repository<Models.House>().Entities.Where(x => x.BaseId == query.BaseId).CountAsync(cancellationToken),


                InventoryCount = await _unitOfWork.Repository<Models.Inventory>().Entities
                .Include(x => x.BaseSection)
                .Include(x => x.House)
                .Include(x => x.Room)
                .ThenInclude(x => x.Building)
                .Where(x =>
                    (x.House != null && x.House.BaseId == query.BaseId) ||
                    (x.BaseSection != null && x.BaseSection.BaseId == query.BaseId) ||
                    (x.Room != null && x.Room.Building != null && x.Room.Building.BaseId == query.BaseId)
                )
                .CountAsync(cancellationToken),


                InventoryItemCount = await _unitOfWork.Repository<Models.InventoryItemBridge>().Entities
                .Include(x => x.Inventory)
                    .ThenInclude(inv => inv.House)
                .Include(x => x.Inventory)
                    .ThenInclude(inv => inv.BaseSection)
                .Include(x => x.Inventory)
                    .ThenInclude(inv => inv.Room)
                        .ThenInclude(room => room.Building)
                .Where(x =>
                    (x.Inventory.House != null && x.Inventory.House.BaseId == query.BaseId) ||
                    (x.Inventory.BaseSection != null && x.Inventory.BaseSection.BaseId == query.BaseId) ||
                    (x.Inventory.Room != null && x.Inventory.Room.Building != null && x.Inventory.Room.Building.BaseId == query.BaseId)
                )
                .CountAsync(cancellationToken),


                OfficeCount = await _unitOfWork.Repository<Models.Office>().Entities
                .Where(x => x.BasesId == query.BaseId).CountAsync(cancellationToken),


                PersonCount = await _unitOfWork.Repository<Models.Person>().Entities.
                Include(x => x.Room)
                .ThenInclude(x => x.Building)
                .Where(x => x.Room.Building.BaseId == query.BaseId).CountAsync(cancellationToken),


                RoomCount = await _unitOfWork.Repository<Models.Room>().Entities
                .Include(x => x.Building)
                .Where(x => x.Building.BaseId == query.BaseId).CountAsync(cancellationToken),

            };

            var selectedYear = DateTime.Now.Year;
            double[] productsFigure = new double[13];
            double[] brandsFigure = new double[13];
            double[] documentsFigure = new double[13];
            double[] documentTypesFigure = new double[13];
            double[] documentExtendedAttributesFigure = new double[13];
            double[] ForcesFigure = new double[13];
            double[] BasesFigure = new double[13];
            double[] BasesSectionsFigure = new double[13];
            for (int i = 1; i <= 12; i++)
            {
                var month = i;
                var filterStartDate = new DateTime(selectedYear, month, 01);
                var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59); // Monthly Based

                productsFigure[i - 1] = await _unitOfWork.Repository<Product>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                brandsFigure[i - 1] = await _unitOfWork.Repository<Brand>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                documentsFigure[i - 1] = await _unitOfWork.Repository<Document>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                documentTypesFigure[i - 1] = await _unitOfWork.Repository<DocumentType>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                documentExtendedAttributesFigure[i - 1] = await _unitOfWork.Repository<DocumentExtendedAttribute>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                ForcesFigure[i - 1] = await _unitOfWork.Repository<Models.Forces>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                BasesFigure[i - 1] = await _unitOfWork.Repository<Models.Bases>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                BasesSectionsFigure[i - 1] = await _unitOfWork.Repository<Models.BasesSections>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
            }


            return await Result<BaseDashboardDataResponse>.SuccessAsync(response);
        }
    }

}
*/