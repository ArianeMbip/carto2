namespace ApiCartobani.Domain.DAs.Features;

using ApiCartobani.Domain.DAs.Services;
using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.DAs.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddDA
{
    public sealed class Command : IRequest<DADto>
    {
        public readonly DAForCreationDto DAToAdd;

        public Command(DAForCreationDto dAToAdd)
        {
            DAToAdd = dAToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, DADto>
    {
        private readonly IDARepository _dARepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IDARepository dARepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _dARepository = dARepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DADto> Handle(Command request, CancellationToken cancellationToken)
        {
            var dA = DA.Create(request.DAToAdd);
            await _dARepository.Add(dA, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var dAAdded = await _dARepository.GetById(dA.Id, cancellationToken: cancellationToken);
            return _mapper.Map<DADto>(dAAdded);
        }
    }
}