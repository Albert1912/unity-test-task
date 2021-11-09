using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;

namespace DownloadStrategies
{
    public class WhenImageReadyDownloadStrategy : BaseDownloadStrategy
    {
        public WhenImageReadyDownloadStrategy(IImageDownloader imageDownloader,
            IImageUrlProvider imageUrlProvider) : base(imageDownloader, imageUrlProvider)
        {
        }

        public override async UniTask StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers,
            CancellationToken cancellationToken)
        {
            var count = downloadedImageConsumers.Count;
            var tasks = new UniTask[count];

            for (var i = 0; i < count; i++)
            {
                tasks[i] = DownloadSingleAsync(downloadedImageConsumers[i], cancellationToken);
            }

            await UniTask.WhenAll(tasks);
        }

        private async UniTask DownloadSingleAsync(IDownloadedImageConsumer downloadedImageConsumer,
            CancellationToken cancellationToken)
        {
            var image = await ImageDownloader.DownloadImageAsync(ImageUrlProvider.GetUrl(), cancellationToken);
            await downloadedImageConsumer.ConsumeDownloadedImageAsync(image, cancellationToken);
        }
    }
}