namespace ApiCartobani.Domain.Fonctionnalites.Features;

using ApiCartobani.Domain.Fonctionnalites.Dtos;
using ApiCartobani.Domain.Fonctionnalites.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetFonctionnalite
{
    public sealed class Query : IRequest<FonctionnaliteDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, FonctionnaliteDto>
    {
        private readonly IFonctionnaliteRepository _fonctionnaliteRepository;
        private readonly IMapper _mapper;

        public Handler(IFonctionnaliteRepository fonctionnaliteRepository, IMapper mapper)
        {
            _mapper = mapper;
            _fonctionnaliteRepository = fonctionnaliteRepository;
        }

        public async Task<FonctionnaliteDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _fonctionnaliteRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<FonctionnaliteDto>(result);
        }
    }
}