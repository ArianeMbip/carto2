namespace ApiCartobani.Domain.Historiques.Features;

using ApiCartobani.Domain.Historiques.Services;
using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.Historiques.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddHistorique
{
    public sealed class Command : IRequest<HistoriqueDto>
    {
        public readonly HistoriqueForCreationDto HistoriqueToAdd;

        public Command(HistoriqueForCreationDto historiqueToAdd)
        {
            HistoriqueToAdd = historiqueToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, HistoriqueDto>
    {
        private readonly IHistoriqueRepository _historiqueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IHistoriqueRepository historiqueRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _historiqueRepository = historiqueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HistoriqueDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var historique = Historique.Create(request.HistoriqueToAdd);
            await _historiqueRepository.Add(historique, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var historiqueAdded = await _historiqueRepository.GetById(historique.Id, cancellationToken: cancellationToken);
            return _mapper.Map<HistoriqueDto>(historiqueAdded);
        }
    }
}