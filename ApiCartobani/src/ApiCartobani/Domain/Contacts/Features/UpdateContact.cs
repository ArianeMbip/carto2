namespace ApiCartobani.Domain.Contacts.Features;

using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Contacts.Dtos;
using ApiCartobani.Domain.Contacts.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateContact
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly ContactForUpdateDto UpdatedContactData;

        public Command(Guid id, ContactForUpdateDto updatedContactData)
        {
            Id = id;
            UpdatedContactData = updatedContactData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var contactToUpdate = await _contactRepository.GetById(request.Id, cancellationToken: cancellationToken);

            contactToUpdate.Update(request.UpdatedContactData);
            _contactRepository.Update(contactToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}