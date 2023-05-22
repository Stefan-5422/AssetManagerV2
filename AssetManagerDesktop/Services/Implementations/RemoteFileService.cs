using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace AssetManagerDesktop.Services.Implementations;

public class RemoteFileService: IRemoteFileService
{
    private readonly HttpClient httpClient;
    private readonly UserConfigProvider userConfigProvider;

    public RemoteFileService(HttpClient client, UserConfigProvider userConfigProvider)
    {
        this.httpClient = client;
        this.userConfigProvider = userConfigProvider;
    }

    public async Task<List<RemoteFile>> GetRemoteFiles()
    {
        HttpRequestMessage message = new(HttpMethod.Get, $"http://{userConfigProvider.Config.ServerName}/File/");
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userConfigProvider.Config.ApiToken);
        HttpResponseMessage result = await httpClient.SendAsync(message);

        if (result.IsSuccessStatusCode)
        {
             Stream stream = await result.Content.ReadAsStreamAsync();
             List<RemoteFile> files = await JsonSerializer.DeserializeAsync<List<RemoteFile>>(stream) ?? new List<RemoteFile>();
            foreach (RemoteFile remoteFile in files)
            {
                remoteFile.DataContext = remoteFile;
            }
             return files;
        }
        return new List<RemoteFile>();
    }

}