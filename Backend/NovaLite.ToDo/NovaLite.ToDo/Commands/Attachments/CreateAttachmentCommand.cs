using MediatR;
using NovaLite.ToDo.Dto;
using NovaLite.ToDo.Model;
using NovaLite.ToDo.Services;

namespace NovaLite.ToDo.Commands.Attachments
{
    public class CreateAttachmentCommand : IRequest<AttachmentSasTokenResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
    public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand,AttachmentSasTokenResponse>
    {
        private readonly ToDoContext _toDoContext;
        private readonly AttachmentSasUriGenerator _sasGenerator;
        public CreateAttachmentCommandHandler(ToDoContext toDoContext, AttachmentSasUriGenerator sasGenerator)
        {
            _toDoContext = toDoContext;
            _sasGenerator = sasGenerator;
        }

        public Task<AttachmentSasTokenResponse> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            Attachment attachment = AddNewAttachment(request.Name);
            string sasToken = _sasGenerator.Generate(attachment, Azure.Storage.Sas.BlobSasPermissions.Write);

            return Task.FromResult(new AttachmentSasTokenResponse { SasToken = sasToken, AttachmentId = attachment.Id });
        }

        private Attachment AddNewAttachment(string name)
        {
            Attachment attachment = new Attachment { Name = name };
            _toDoContext.Set<Attachment>().Add(attachment);
            _toDoContext.SaveChanges();

            return attachment;
        }
    }
}
