using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDownloadStrategy
    {
        Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers,
            CancellationToken cancellationToken);
    }
}