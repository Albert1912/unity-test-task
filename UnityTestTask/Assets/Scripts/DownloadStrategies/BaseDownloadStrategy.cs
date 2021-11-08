using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces;

namespace DownloadStrategies
{
    public abstract class BaseDownloadStrategy : IDownloadStrategy
    {
        protected readonly IImageDownloader ImageDownloader;
        protected readonly IImageUrlProvider ImageUrlProvider;

        protected BaseDownloadStrategy(IImageDownloader imageDownloader, IImageUrlProvider imageUrlProvider)
        {
            ImageDownloader = imageDownloader;
            ImageUrlProvider = imageUrlProvider;
        }

        public abstract Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers);
    }
}