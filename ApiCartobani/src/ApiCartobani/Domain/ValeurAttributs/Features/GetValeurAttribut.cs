namespace ApiCartobani.Domain.ValeurAttributs.Features;

using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.Domain.ValeurAttributs.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetValeurAttribut
{
    public sealed class Query : IRequest<ValeurAttributDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, ValeurAttributDto>
    {
        private readonly IValeurAttributRepository _valeurAttributRepository;
        private readonly IMapper _mapper;

        public Handler(IValeurAttributRepository valeurAttributRepository, IMapper mapper)
        {
            _mapper = mapper;
            _valeurAttributRepository = valeurAttributRepository;
        }

        public async Task<ValeurAttributDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _valeurAttributRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ValeurAttributDto>(result);
        }
    }
}