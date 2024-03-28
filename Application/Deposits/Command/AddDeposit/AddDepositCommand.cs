using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Deposits.Command.AddDeposit
{
    public class AddDepositCommand : IRequest<Unit>
    {
        public double Amount { get; set; }
        public string UserWalletId { get; set; }
        public string DepositMethod { get; set; }
        public string OperationId { get; set; }
        public IFormFile File { get; set; }
    }

    public class AddDepositCommandHandler : IRequestHandler<AddDepositCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileUploadService _fileUploadService;

        public AddDepositCommandHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IFileUploadService fileUploadService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        public async Task<Unit> Handle(AddDepositCommand request, CancellationToken cancellationToken)
        {

            var user = await _applicationDbContext.Users.FindAsync(_currentUserService.UserId);
            var url = await _fileUploadService.UploadImage(request.File);
            user.AddDepositRequest(request.Amount, request.DepositMethod, request.OperationId, request.UserWalletId,
                url.ToString());

            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Unit.Value
                : throw new CantAddEntityException("Something went wrong please try again!");
        }
    }
}