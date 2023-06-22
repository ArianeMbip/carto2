namespace ApiCartobani.Domain.TypeElements.Features;

using ApiCartobani.Domain.TypeElements.Dtos;
using ApiCartobani.Domain.TypeElements.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetTypeElement
{
    public sealed class Query : IRequest<TypeElementDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, TypeElementDto>
    {
        private readonly ITypeElementRepository _typeElementRepository;
        private readonly IMapper _mapper;

        public Handler(ITypeElementRepository typeElementRepository, IMapper mapper)
        {
            _mapper = mapper;
            _typeElementRepository = typeElementRepository;
        }

        public async Task<TypeElementDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _typeElementRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<TypeElementDto>(result);
        }
    }
}