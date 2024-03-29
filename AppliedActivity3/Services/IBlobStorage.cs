﻿namespace AppliedActivity3.Services;

public interface IBlobStorage
{
    //Related Documentation:
    //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/introduction
    //https://github.com/xamarin/xamarin-forms-samples/tree/main/DependencyService

    Task UploadStreamAsync(string name, MemoryStream stream);
    Task<MemoryStream> DownloadStreamAsync(string name);
    Task<List<string>> ListBlobsAsync(string prefix, int? size = null);
}