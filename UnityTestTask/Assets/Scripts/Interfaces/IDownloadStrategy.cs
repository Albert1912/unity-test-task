using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Interfaces
{
    public interface IDownloadStrategy
    {
        UniTask StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers,
            CancellationToken cancellationToken);
    }
}