using Forces.Application.Features.Building.Queries.GetBySpecifics;
using Forces.Application.Interfaces.Repositories;
using Forces.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.Office.Queries.GetAllBySpecifics
{
    internal class GetOfficeByQuery : IRequest<IResult<List<GetOfficeByResponse>>>
    {
        public int? Id { get; set; }
        public string Name { get; set; }

    }
    internal class GetAllOfficeQuesryHandler : IRequestHandler<GetOfficeByQuery, IResult<List<GetOfficeByResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllOfficeQuesryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<List<GetOfficeByResponse>>> Handle(GetOfficeByQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
