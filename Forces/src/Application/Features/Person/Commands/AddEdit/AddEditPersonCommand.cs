using AutoMapper;
using Forces.Application.Features.Person.Commands.AddEdit;
using Forces.Application.Interfaces.Repositories;
using Forces.Application.Interfaces.Services;
using Forces.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.Person.Commands.AddEdit
{
    public class AddEditPersonCommand : IRequest<IResult<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NationalNumber { get; set; }
        public int? RoomId { get; set; }
        public int? HouseId { get; set; }
        public string Phone { get; set; }
        public string OfficePhone { get; set; }
        public string Section { get; set; }
        public string Rank { get; set; }   

    }
    internal class AddEditPersonCommandHandler : IRequestHandler<AddEditPersonCommand, IResult<int>>
    {
        private protected IItemRepository _ItemsRepository;
        private protected IUnitOfWork<int> _unitOfWork;
        private protected IMapper _mapper;
        private readonly IStringLocalizer<AddEditPersonCommandHandler> _localizer;
        private readonly IVoteCodeService _voteCodeService;
        public AddEditPersonCommandHandler(IItemRepository itemsRepository, IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditPersonCommandHandler> localizer, IVoteCodeService voteCodeService)
        {
            _ItemsRepository = itemsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _voteCodeService = voteCodeService;
        }

        public async Task<IResult<int>> Handle(AddEditPersonCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var ExistPerson = await _unitOfWork.Repository<Models.Person>().Entities.FirstOrDefaultAsync(
                    (x => (x.Name == request.Name 
                    && x.NationalNumber == request.NationalNumber 
                    && (x.RoomId == request.RoomId || x.HouseId== request.HouseId)
                    )));

                if (ExistPerson != null)
                {
                    return await Result<int>.FailAsync(_localizer["This Person Name Is Already Exist!"]);
                }
                else
                {
                    var room = _unitOfWork.Repository<Models.Room>().Entities.FirstOrDefaultAsync(_ => _.Id == request.RoomId).Result;
                    if (room != null && room.Size <= room.Persons.Count)
                    {
                        return await Result<int>.FailAsync(_localizer["The Room Is Full!"]);
                    }
                    else
                    {
                        Models.Person Person = new Models.Person()
                        {
                            Id = request.Id,
                            Name = request.Name,
                            RoomId = request.RoomId,
                            HouseId = request.HouseId,
                            NationalNumber = request.NationalNumber,
                            Phone = request.Phone,
                            OfficePhone = request.OfficePhone,
                            Rank = request.Rank,
                            Section = request.Section
                        };
                        await _unitOfWork.Repository<Models.Person>().AddAsync(Person);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(Person.Id, _localizer["Person Added Successfuly!"]);
                    }
                }
            }
            else
            {
                var ExistPerson = await _unitOfWork.Repository<Models.Person>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (ExistPerson == null)
                {
                    return await Result<int>.FailAsync(_localizer["Person Not Found!!"]);
                }
                else
                {
                    var ExistnamePerson = await _unitOfWork.Repository<Models.Person>().Entities.FirstOrDefaultAsync(x => x.Name == request.Name && x.Id != request.Id);
                    
                    if (ExistnamePerson != null)
                    {
                        return await Result<int>.FailAsync(_localizer["This Person Is Already Exist!"]);
                    }
                    else
                    {
                        ExistPerson.Name = request.Name;
                        ExistPerson.OfficePhone = request.OfficePhone;
                        ExistPerson.Rank = request.Rank;
                        ExistPerson.Phone = request.Phone;
                        ExistPerson.RoomId = request.RoomId;
                        await _unitOfWork.Repository<Models.Person>().UpdateAsync(ExistPerson);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(ExistPerson.Id, _localizer["Person Updated Successfuly!"]);
                    }
                }
            }
        }

    }
}
