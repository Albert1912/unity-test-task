using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Interfaces
{
    public interface IImageDownloader
    {
        UniTask<Texture> DownloadImageAsync(string url, CancellationToken cancellationToken);
    }
}