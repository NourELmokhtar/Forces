using AutoMapper;
using Forces.Application.Enums;
using Forces.Application.Interfaces.Repositories;
using Forces.Application.Interfaces.Services;
using Forces.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Forces.Application.Features.InventoryInventoryItem.Commands.AddEdit
{
    public class AddEditInventoryItemCommand : IRequest<IResult<int>>
    {
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; }
        public string ItemArName { get; set; }
        [Required]
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        [Required]
        public string ItemNsn { get; set; }
        [Required]
        public int MeasureUnitId { get; set; }
        [Required]
        public int? VoteCodesId { get; set; }
        public string VoteCode { get; set; }
        public ItemClass ItemClass { get; set; } = ItemClass.A;
        public string MadeIn { get; set; }

        public DateTime? DateOfEnter { get; set; }
        public DateTime? FirstUseDate { get; set; }
        public DateTime? EndOfServiceDate { get; set; }
        public string SerialNumber { get; set; }
    }
    internal class AddEditInventoryItemCommandHandler : IRequestHandler<AddEditInventoryItemCommand, IResult<int>>
    {
        private protected IInventoryItemRepository _InventoryItemsRepository;
        private protected IUnitOfWork<int> _unitOfWork;
        private protected IMapper _mapper;
        private readonly IStringLocalizer<AddEditInventoryItemCommandHandler> _localizer;
        private readonly IVoteCodeService _voteCodeService;
        public AddEditInventoryItemCommandHandler(IInventoryItemRepository InventoryItemsRepository, IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditInventoryItemCommandHandler> localizer, IVoteCodeService voteCodeService)
        {
            _InventoryItemsRepository = InventoryItemsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _voteCodeService = voteCodeService;
        }

        public async Task<IResult<int>> Handle(AddEditInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var ForceOfVoteCode = await _voteCodeService.GetCodeBy(request.VoteCodesId.Value);
            if (string.IsNullOrEmpty(request.SerialNumber) && request.ItemClass != ItemClass.C)
            {
                return await Result<int>.FailAsync(_localizer["Item Serail Number Is Required"]);
            }
            if (request.Id == 0)
            {

                if (await _InventoryItemsRepository.IsCodeExist(request.ItemCode))
                {
                    var item = await _InventoryItemsRepository.GetByCode(request.ItemCode);
                    var vCode = await _voteCodeService.GetCodeBy(item.VoteCodesId);
                    if (vCode.ForceId == ForceOfVoteCode.ForceId)
                    {
                        return await Result<int>.FailAsync(_localizer["Item Code: {0} is Already Exist!", request.ItemCode]);
                    }
                }
                if (await _InventoryItemsRepository.IsNsnExist(request.ItemNsn))
                {
                    var item = await _InventoryItemsRepository.GetByNSN(request.ItemNsn);
                    var vCode = await _voteCodeService.GetCodeBy(item.VoteCodesId);
                    if (vCode.ForceId == ForceOfVoteCode.ForceId)
                    {
                        return await Result<int>.FailAsync(_localizer["Item NSN: {0} is Already Exist!", request.ItemNsn]);
                    }

                }
                var Item = _mapper.Map<Models.Items>(request);
                await _unitOfWork.Repository<Application.Models.Items>().AddAsync(Item);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(Item.Id, _localizer["Item Added!"]);
            }
            else
            {
                var Item = _mapper.Map<Models.Items>(request);
                var dbItem = await _unitOfWork.Repository<Models.Items>().GetByIdAsync(Item.Id);
                if (dbItem != null)
                {

                    if (Item.ItemCode != dbItem.ItemCode)
                    {
                        if (await _InventoryItemsRepository.IsCodeExist(Item.ItemCode))
                        {
                            var item = await _InventoryItemsRepository.GetByCode(request.ItemCode);
                            var vCode = await _voteCodeService.GetCodeBy(item.VoteCodesId);
                            if (vCode.ForceId == ForceOfVoteCode.ForceId)
                            {
                                return await Result<int>.FailAsync(_localizer["Item Code: {0} is Already Exist!", request.ItemCode]);
                            }
                        }
                    }
                    if (Item.ItemNsn != dbItem.ItemNsn)
                    {
                        if (await _InventoryItemsRepository.IsNsnExist(Item.ItemNsn))
                        {
                            var item = await _InventoryItemsRepository.GetByNSN(request.ItemNsn);
                            var vCode = await _voteCodeService.GetCodeBy(item.VoteCodesId);
                            if (vCode.ForceId == ForceOfVoteCode.ForceId)
                            {
                                return await Result<int>.FailAsync(_localizer["Item NSN: {0} is Already Exist!", request.ItemNsn]);
                            }

                        }
                    }
                    await _unitOfWork.Repository<Models.Items>().UpdateAsync(Item);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(Item.Id, _localizer["Item Updated!"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Item Not Found!"]);
                }
            }
        }
    }
}
