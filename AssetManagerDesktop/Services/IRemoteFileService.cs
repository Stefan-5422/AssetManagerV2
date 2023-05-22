using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagerDesktop.Services
{
    public interface IRemoteFileService
    {
        Task DownloadRemoteFile(string guid);

        Task<List<Models.RemoteFile>> GetRemoteFiles();

        Task SendRemoteFiles(List<string> files);
    }
}