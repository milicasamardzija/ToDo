namespace NovaLite.ToDo.Dto
{
    public class AttachmentSasTokenResponse
    {
        public string SasToken { get; set; } = string.Empty;
        public Guid AttachmentId { get; set; }
    }
}
