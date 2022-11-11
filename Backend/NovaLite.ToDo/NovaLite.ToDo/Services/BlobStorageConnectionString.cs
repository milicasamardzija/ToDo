namespace NovaLite.ToDo.Services
{
    public record BlobStorageConnectionString(string AccountKey, string AccountName, string EndpointSuffix)
    {
        public static BlobStorageConnectionString FromString(string value)
        {
            string? accountKey = value.Split(';')
                .FirstOrDefault(v => v.StartsWith(nameof(AccountKey)))?
                .Substring(11) ?? string.Empty;

            string? endpointSuffix = value.Split(';')
                 .FirstOrDefault(v => v.StartsWith(nameof(EndpointSuffix)))?
                 .Split('=')[1] ?? string.Empty;

            string? accountName = value.Split(';')
                 .FirstOrDefault(v => v.StartsWith(nameof(AccountName)))?
                 .Split('=')[1] ?? string.Empty;

            return new BlobStorageConnectionString(accountKey, accountName, endpointSuffix);
        }

        public override string ToString()
        {
            return $"{nameof(AccountKey)}={AccountKey};{nameof(AccountName)}={AccountName};{nameof(EndpointSuffix)}={EndpointSuffix}";
        }
    }
}
