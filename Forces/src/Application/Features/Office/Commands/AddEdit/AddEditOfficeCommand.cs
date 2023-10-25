using AutoMapper;
using Forces.Application.Interfaces.Repositories;
using Forces.Application.Interfaces.Services;
using Forces.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Forces.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Forces.Application.Features.Office.Commands.AddEdit
{
    public class AddEditOfficeCommand : IRequest<IResult<int>>
    {
        public int Id { get; set; }
        public int BasesSectionsId { get; set; }
        public string Name { get; set; }
    }
    internal class AddEditOfficeCommandHandler : IRequestHandler<AddEditOfficeCommand, IResult<int>>
    {
        private protected IItemRepository _ItemsRepository;
        private protected IUnitOfWork<int> _unitOfWork;
        private protected IMapper _mapper;
        private readonly IStringLocalizer<AddEditOfficeCommandHandler> _localizer;
        private readonly IVoteCodeService _voteCodeService;
        public AddEditOfficeCommandHandler(IItemRepository itemsRepository, IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditOfficeCommandHandler> localizer, IVoteCodeService voteCodeService)
        {
            _ItemsRepository = itemsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _voteCodeService = voteCodeService;
        }

        public async Task<IResult<int>> Handle(AddEditOfficeCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var ExistOffice = await _unitOfWork.Repository<Models.Office>().Entities.FirstOrDefaultAsync(x => x.Name == request.Name && x.BasesSectionsId == request.BasesSectionsId);
                if (ExistOffice != null)
                {
                    return await Result<int>.FailAsync(_localizer["This Office Name Is Already Exist!"]);
                }
                else
                {
                    Models.Office office = new Models.Office()
                    {
                        Name = request.Name
                    };
                    await _unitOfWork.Repository<Models.Office>().AddAsync(office);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(office.Id, _localizer["Office Added Successfuly!"]);
                }
            }
            else
            {
                var ExistOffice = await _unitOfWork.Repository<Models.Office>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (ExistOffice == null)
                {
                    return await Result<int>.FailAsync(_localizer["Office Not Found!!"]);
                }
                else
                {
                    var ExistnameOffice = await _unitOfWork.Repository<Models.Office>().Entities.FirstOrDefaultAsync(x => x.Name == request.Name && x.Id != request.Id);
                    if (ExistnameOffice != null)
                    {
                        return await Result<int>.FailAsync(_localizer["This Office Is Already Exist!"]);
                    }
                    else
                    {
                        ExistOffice.Name = request.Name;
                        await _unitOfWork.Repository<Models.Office>().UpdateAsync(ExistOffice);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(ExistOffice.Id, _localizer["Office Updated Successfuly!"]);
                    }
                }
            }
        }


    }
    }

