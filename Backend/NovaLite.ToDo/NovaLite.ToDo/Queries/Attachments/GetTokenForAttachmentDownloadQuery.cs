using MediatR;
using NovaLite.ToDo.Dto;
using NovaLite.ToDo.Model;
using NovaLite.ToDo.Services;

namespace NovaLite.ToDo.Queries.Attachments
{
    public class GetTokenForAttachmentDownloadQuery :IRequest<AttachmentSasTokenResponse>
    {
        public string Id { get; set; }
    }
    public class GetTokenForAttachmentDownloadQueryHandler : IRequestHandler<GetTokenForAttachmentDownloadQuery, AttachmentSasTokenResponse>
    {
        private readonly ToDoContext _toDoContext;
        private readonly AttachmentSasUriGenerator _sasGenerator;
        public GetTokenForAttachmentDownloadQueryHandler(ToDoContext toDoContext, AttachmentSasUriGenerator sasGenerator)
        {
            _toDoContext = toDoContext;
            _sasGenerator = sasGenerator;
        }

        public Task<AttachmentSasTokenResponse> Handle(GetTokenForAttachmentDownloadQuery request, CancellationToken cancellationToken)
        {
            Attachment attachment = GetAttachment(request.Id);
            string sasToken = _sasGenerator.Generate(attachment, Azure.Storage.Sas.BlobSasPermissions.Read);
            return Task.FromResult(new AttachmentSasTokenResponse { SasToken = sasToken, AttachmentId = attachment.Id });
        }

        private Attachment GetAttachment(string id)
        {
            return _toDoContext.Attachment
                               .FirstOrDefault(a => a.Id.ToString() == id);
        }
    }
}
