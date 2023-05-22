using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagerDesktop.Services
{
    public interface IRemoteFileService
    {
        Task<List<RemoteFile>> GetRemoteFiles();

        void SendRemoteFiles(List<string> files);
    }
}