namespace ApiCartobani.Domain.InterfacesUtilisateur.Features;

using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using ApiCartobani.Domain.InterfacesUtilisateur.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetInterfaceUtilisateur
{
    public sealed class Query : IRequest<InterfaceUtilisateurDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, InterfaceUtilisateurDto>
    {
        private readonly IInterfaceUtilisateurRepository _interfaceUtilisateurRepository;
        private readonly IMapper _mapper;

        public Handler(IInterfaceUtilisateurRepository interfaceUtilisateurRepository, IMapper mapper)
        {
            _mapper = mapper;
            _interfaceUtilisateurRepository = interfaceUtilisateurRepository;
        }

        public async Task<InterfaceUtilisateurDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _interfaceUtilisateurRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<InterfaceUtilisateurDto>(result);
        }
    }
}