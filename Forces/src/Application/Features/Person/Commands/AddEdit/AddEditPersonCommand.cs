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

namespace Forces.Application.Features.Person.Commands.AddEdit
{
    public class AddEditPersonCommand : IRequest<IResult<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NationalNumber { get; set; }
    }
    internal class AddEditPersonCommandHandler : IRequestHandler<AddEditPersonCommand, IResult<int>>
    {
        private protected IPersonRepository _personRepository;
        private protected IUnitOfWork<int> _unitOfWork;
        private protected IMapper _mapper;
        private readonly IStringLocalizer<AddEditPersonCommandHandler> _localizer;
        private readonly IVoteCodeService _voteCodeService;

        public AddEditPersonCommandHandler(IPersonRepository personRepository, IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditPersonCommandHandler> localizer)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IResult<int>> Handle(AddEditPersonCommand request, CancellationToken cancellationToken)
        {
            /* if (request.Id == 0)
             {*/
            var Person = _mapper.Map<Application.Models.Person>(request);
            var ISExist = _unitOfWork.Repository<Application.Models.Person>().Entities.
                Any(x => x.NationalNumber == Person.NationalNumber);
            if (ISExist)
            {
                return await Result<int>.FailAsync(_localizer["Person With this Name Is Already Exist!"]);
            }
            await _unitOfWork.Repository<Application.Models.Person>().AddAsync(Person);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(Person.Id, _localizer["Person Added!"]);

            /*}
            else
            {
                var Person = await _unitOfWork.Repository<Application.Models.Person>().GetByIdAsync(request.Id);
                if (Person != null)
                {
                    Person.Name = request.Name ?? Person.Name;
                }
            }


            }*/
        }

    }
}
