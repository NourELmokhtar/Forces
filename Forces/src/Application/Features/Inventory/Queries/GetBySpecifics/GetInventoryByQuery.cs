using Forces.Application.Interfaces.Repositories;
using Forces.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forces.Application.Features.Inventory.Queries.GetBySpecifics
{
    internal class GetInventoryByQuery : IRequest<IResult<List<GetInventoryByResponse>>>
{
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    internal class GetInventoryByQueryHandler : IRequestHandler<GetInventoryByQuery, IResult<List<GetInventoryByResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetInventoryByQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<List<GetInventoryByResponse>>> Handle(GetInventoryByQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
