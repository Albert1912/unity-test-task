using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Interfaces
{
    public interface IDownloadedImageConsumer
    {
        UniTask ConsumeDownloadedImageAsync(Texture image, CancellationToken cancellationToken);
    }
}