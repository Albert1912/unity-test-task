using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDownloadStrategy
    {
        Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers);
    }
}