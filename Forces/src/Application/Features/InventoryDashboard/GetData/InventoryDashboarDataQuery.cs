using Forces.Application.Features.InventoryDashboard.GetData;
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
using Microsoft.EntityFrameworkCore;

namespace Forces.Application.Features.InventoryDashboard.GetData
{
        public class InventoryDashboarDataQuery : IRequest<Result<InventoryDashboardDataResponse>>
        {
            public int InventoryId { get; set; }
        }

        internal class GetInventoryDashboardDataQueryHandler : IRequestHandler<InventoryDashboarDataQuery, Result<InventoryDashboardDataResponse>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            private readonly IUserService _userService;
            private readonly IRoleService _roleService;
            private readonly IStringLocalizer<GetInventoryDashboardDataQueryHandler> _localizer;
            private readonly IVoteCodeService _voteCodeService;

            public GetInventoryDashboardDataQueryHandler(IUnitOfWork<int> unitOfWork, IUserService userService, IRoleService roleService, IStringLocalizer<GetInventoryDashboardDataQueryHandler> localizer, IVoteCodeService voteCodeService)
            {
                _unitOfWork = unitOfWork;
                _userService = userService;
                _roleService = roleService;
                _localizer = localizer;
                _voteCodeService = voteCodeService;
            }

            public async Task<Result<InventoryDashboardDataResponse>> Handle(InventoryDashboarDataQuery query, CancellationToken cancellationToken)
            {
            var Inventory = await _unitOfWork.Repository<Models.Inventory>().GetByIdAsync(query.InventoryId);


            var response = new InventoryDashboardDataResponse
            {
                InventoryName = Inventory.Name,
                BaseSectionName = Inventory.BaseSectionId != null
                ? _unitOfWork.Repository<Models.BasesSections>().GetAllAsync().Result
                    .Where(y => y.Id == Inventory.BaseSectionId)
                    .FirstOrDefault()?.SectionName
                : null,
                HouseName = Inventory.HouseId != null
                ? _unitOfWork.Repository<Models.House>().GetAllAsync().Result
                    .Where(y => y.Id == Inventory.HouseId)
                    .FirstOrDefault()?.HouseName
                : null,
                RoomNumber = Inventory.RoomId != null
                ? _unitOfWork.Repository<Models.Room>().GetAllAsync().Result
                    .Where(y => y.Id == Inventory.RoomId)
                    .FirstOrDefault()?.RoomNumber
                : null,
                BaseName = Inventory.BaseSectionId != null
                ? _unitOfWork.Repository<Models.Bases>().GetAllAsync().Result
                    .Where(y => y.Id == _unitOfWork.Repository<Models.BasesSections>().GetAllAsync().Result
                    .Where(y => y.Id == Inventory.BaseSectionId)
                    .FirstOrDefault()?.BaseId)
                    .FirstOrDefault()?.BaseName
                : Inventory.HouseId != null
                ? _unitOfWork.Repository<Models.Bases>().GetAllAsync().Result
                    .Where(y => y.Id == _unitOfWork.Repository<Models.House>().GetAllAsync().Result
                    .Where(y => y.Id == Inventory.HouseId)
                    .FirstOrDefault()?.BaseId)
                    .FirstOrDefault()?.BaseName :
                    Inventory.RoomId != null
                ? _unitOfWork.Repository<Models.Bases>().GetAllAsync().Result
                    .Where(y => y.Id == _unitOfWork.Repository<Models.Building>().GetAllAsync().Result
                    .Where(y => y.Id == _unitOfWork.Repository<Models.Room>().GetAllAsync().Result
                    .Where(y => y.Id == Inventory.RoomId)
                    .FirstOrDefault()?.BuildingId)
                    .FirstOrDefault()?.BaseId)
                    .FirstOrDefault()?.BaseName : null,


                BuildingName = Inventory.RoomId != null
                ? _unitOfWork.Repository<Models.Building>().GetAllAsync().Result.Where(y => y.Id == (
                _unitOfWork.Repository<Models.Room>().GetAllAsync().Result.Where(y => y.Id == Inventory.RoomId).FirstOrDefault().BuildingId)).FirstOrDefault().BuildingName : "",


                Item = _unitOfWork.Repository<Models.InventoryItemBridge>()
                .Entities
                .Include(x => x.InventoryItem)
                .Where(y => y.InventoryId == Inventory.Id)
                .GroupBy(x => new {
                    ItemCode = x.InventoryItem.ItemCode,
                    ItemName = x.InventoryItem.ItemName
                })
                .Select(group => new ItemsData
                {
                    ItemClass = group.First().InventoryItem.ItemClass.ToString(),
                    ItemCode = group.Key.ItemCode,
                    ItemName = group.Key.ItemName,
                    ItemNSN = group.First().InventoryItem.ItemNsn,
                    ItemSerial = group.Select(item => item.SerialNumber).Where(serial => serial != null).ToList(),
                    Quantity = group.Count() 
                })
                .ToList(),


            Persons = Inventory.RoomId != null ? _unitOfWork.Repository<Models.Person>().Entities.Include(x => x.Room).ThenInclude(x=>x.Building)
                .Where(y => _unitOfWork.Repository<Models.Inventory>().Entities
                .Where(x => x.RoomId == y.RoomId).FirstOrDefault().Id == Inventory.Id)
                .Select(x =>
                new PersonData
                {
                    BuildingName = x.Room.Building.BuildingName,
                    Name = x.Name,
                    NationalNumber = x.NationalNumber,
                    OfficePhone = x.OfficePhone,
                    Phone = x.Phone,
                    Rank = x.Rank,
                    RoomNumber = x.Room.RoomNumber,
                    Section = x.Section,
                }).ToList():
                Inventory.HouseId != null ? _unitOfWork.Repository<Models.Person>().Entities.Include(x => x.House)
                .Where(y => _unitOfWork.Repository<Models.Inventory>().Entities
                .Where(x => x.HouseId == y.HouseId).OrderBy(x=>x.Id).LastOrDefault().Id == Inventory.Id)
                .Select(x =>
                new PersonData
                {
                    HouseName = x.House.HouseName,
                    Name = x.Name,
                    NationalNumber = x.NationalNumber,
                    OfficePhone = x.OfficePhone,
                    Phone = x.Phone,
                    Rank = x.Rank,
                    Section = x.Section,
                }).ToList():
                null,

            };

                                
                

                return await Result<InventoryDashboardDataResponse>.SuccessAsync(response);
            }
        }
}
