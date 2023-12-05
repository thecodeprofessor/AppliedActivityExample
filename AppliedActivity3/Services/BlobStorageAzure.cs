using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AppliedActivity3.Services;

internal class BlobStorageAzure : IBlobStorage
{
    //Related Documentation:
    //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
    //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-xamarin
    //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-list
    //https://docs.microsoft.com/en-us/visualstudio/data-tools/how-to-save-and-edit-connection-strings?view=vs-2019

    private readonly BlobServiceClient service = new(ConnectionString);

    //Connection strings and secrets should not be stored in code, a secrets manager should be used.
    //Do not commit connection strings or secrets to your repository.
    private static string ConnectionString => "connectionstring";
    private static string Container => "container";

    public async Task<MemoryStream> DownloadStreamAsync(string name)
    {
        try
        {
            var blob = service.GetBlobContainerClient(Container).GetBlobClient(name);

            if (blob.Exists())
            {
                var stream = new MemoryStream();
                await blob.DownloadToAsync(stream);

                stream.Position = 0;
                return stream;
            }
        }
        catch (Exception e)
        {
            //Catch errors here.
        }

        return null;
    }

    public async Task<List<string>> ListBlobsAsync(string prefix = "", int? size = null)
    {
        var blobs = new List<string>();
        try
        {
            var results = service.GetBlobContainerClient(Container).GetBlobsAsync().AsPages(default, size);

            await foreach (var blobPage in results)
                blobs.AddRange(from BlobItem blob in blobPage.Values
                    where prefix != "" && blob.Name.StartsWith(prefix)
                    select blob.Name);

            return blobs;
        }
        catch (Exception e)
        {
            //Catch errors here.
        }

        return null;
    }

    public async Task UploadStreamAsync(string name, MemoryStream stream)
    {
        try
        {
            stream.Position = 0;
            var blob = service.GetBlobContainerClient(Container).GetBlobClient(name);
            await blob.UploadAsync(stream);
        }
        catch (Exception e)
        {
            //Catch errors here.
        }
    }
}