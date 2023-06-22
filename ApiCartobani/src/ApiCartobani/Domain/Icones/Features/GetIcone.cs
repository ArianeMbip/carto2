namespace ApiCartobani.Domain.Icones.Features;

using ApiCartobani.Domain.Icones.Dtos;
using ApiCartobani.Domain.Icones.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetIcone
{
    public sealed class Query : IRequest<IconeDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, IconeDto>
    {
        private readonly IIconeRepository _iconeRepository;
        private readonly IMapper _mapper;

        public Handler(IIconeRepository iconeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _iconeRepository = iconeRepository;
        }

        public async Task<IconeDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _iconeRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<IconeDto>(result);
        }
    }
}