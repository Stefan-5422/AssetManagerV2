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

public class RemoteFileService : IRemoteFileService
{
    private readonly HttpClient httpClient;
    private readonly UserConfigProvider userConfigProvider;

    public RemoteFileService(HttpClient client, UserConfigProvider userConfigProvider)
    {
        this.httpClient = client;
        this.userConfigProvider = userConfigProvider;
    }

    public async Task DownloadRemoteFile(string guid)
    {
        HttpRequestMessage message = new(HttpMethod.Get, $"http://{userConfigProvider.Config.ServerName}/Content/{guid}");
        HttpResponseMessage result = await httpClient.SendAsync(message);

        string? filename = result.Content.Headers.ContentDisposition?.FileName;

        if (filename is null)
        {
            //TODO: Give user rich error
            return;
        }

        if (!Directory.Exists("./Downloads"))
        {
            Directory.CreateDirectory("./Downloads");
        }

        FileStream f = File.OpenWrite($"./Downloads/{Guid.NewGuid()}-{filename}");

        result.Content.ReadAsStream().CopyTo(f);
    }

    public async Task<List<Models.RemoteFile>> GetRemoteFiles()
    {
        HttpRequestMessage message = new(HttpMethod.Get, $"http://{userConfigProvider.Config.ServerName}/File/");
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userConfigProvider.Config.ApiToken);
        HttpResponseMessage result = await httpClient.SendAsync(message);

        if (result.IsSuccessStatusCode)
        {
            Stream stream = await result.Content.ReadAsStreamAsync();
            List<Models.RemoteFile> files = await JsonSerializer.DeserializeAsync<List<Models.RemoteFile>>(stream) ?? new List<Models.RemoteFile>();

            return files;
        }
        return new List<Models.RemoteFile>();
    }

    public async Task SendRemoteFiles(List<string> files)
    {
        MultipartFormDataContent content = new();
        foreach (string file in files)
        {
            Debug.WriteLine(file);
            if (!File.Exists(file))
                continue;
            Stream s = File.OpenRead(file);
            HttpContent contentfile = new StreamContent(s);
            content.Add(contentfile, "files", Path.GetFileName(file));
        }
        HttpRequestMessage message = new(HttpMethod.Post, $"http://{userConfigProvider.Config.ServerName}/File/");
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userConfigProvider.Config.ApiToken);
        message.Content = content;

        HttpResponseMessage result = await httpClient.SendAsync(message);

        Debug.WriteLine(await result.Content.ReadAsStringAsync());

        if (!result.IsSuccessStatusCode)
        {
            //TODO: Give the user some rich feedback
        }
    }
}