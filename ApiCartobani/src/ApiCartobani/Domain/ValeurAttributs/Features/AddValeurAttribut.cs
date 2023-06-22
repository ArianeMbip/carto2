namespace ApiCartobani.Domain.ValeurAttributs.Features;

using ApiCartobani.Domain.ValeurAttributs.Services;
using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddValeurAttribut
{
    public sealed class Command : IRequest<ValeurAttributDto>
    {
        public readonly ValeurAttributForCreationDto ValeurAttributToAdd;

        public Command(ValeurAttributForCreationDto valeurAttributToAdd)
        {
            ValeurAttributToAdd = valeurAttributToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, ValeurAttributDto>
    {
        private readonly IValeurAttributRepository _valeurAttributRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IValeurAttributRepository valeurAttributRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _valeurAttributRepository = valeurAttributRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValeurAttributDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var valeurAttribut = ValeurAttribut.Create(request.ValeurAttributToAdd);
            await _valeurAttributRepository.Add(valeurAttribut, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var valeurAttributAdded = await _valeurAttributRepository.GetById(valeurAttribut.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ValeurAttributDto>(valeurAttributAdded);
        }
    }
}