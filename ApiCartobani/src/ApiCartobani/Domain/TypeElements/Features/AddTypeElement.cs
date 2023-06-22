namespace ApiCartobani.Domain.TypeElements.Features;

using ApiCartobani.Domain.TypeElements.Services;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.TypeElements.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddTypeElement
{
    public sealed class Command : IRequest<TypeElementDto>
    {
        public readonly TypeElementForCreationDto TypeElementToAdd;

        public Command(TypeElementForCreationDto typeElementToAdd)
        {
            TypeElementToAdd = typeElementToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, TypeElementDto>
    {
        private readonly ITypeElementRepository _typeElementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(ITypeElementRepository typeElementRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _typeElementRepository = typeElementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TypeElementDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var typeElement = TypeElement.Create(request.TypeElementToAdd);
            await _typeElementRepository.Add(typeElement, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var typeElementAdded = await _typeElementRepository.GetById(typeElement.Id, cancellationToken: cancellationToken);
            return _mapper.Map<TypeElementDto>(typeElementAdded);
        }
    }
}