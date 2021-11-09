using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
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

        public abstract UniTask StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers,
            CancellationToken cancellationToken);
    }
}