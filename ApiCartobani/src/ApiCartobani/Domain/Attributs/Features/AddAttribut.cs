namespace ApiCartobani.Domain.Attributs.Features;

using ApiCartobani.Domain.Attributs.Services;
using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.Attributs.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddAttribut
{
    public sealed class Command : IRequest<AttributDto>
    {
        public readonly AttributForCreationDto AttributToAdd;

        public Command(AttributForCreationDto attributToAdd)
        {
            AttributToAdd = attributToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, AttributDto>
    {
        private readonly IAttributRepository _attributRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IAttributRepository attributRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _attributRepository = attributRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AttributDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var attribut = Attribut.Create(request.AttributToAdd);
            await _attributRepository.Add(attribut, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var attributAdded = await _attributRepository.GetById(attribut.Id, cancellationToken: cancellationToken);
            return _mapper.Map<AttributDto>(attributAdded);
        }
    }
}