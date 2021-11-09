using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;

namespace DownloadStrategies
{
    public class WhenImageReadyDownloadStrategy : BaseDownloadStrategy
    {
        public WhenImageReadyDownloadStrategy(IImageDownloader imageDownloader,
            IImageUrlProvider imageUrlProvider) : base(imageDownloader, imageUrlProvider)
        {
        }

        public override async Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers,
            CancellationToken cancellationToken)
        {
            var count = downloadedImageConsumers.Count;
            var tasks = new Task[count];

            for (var i = 0; i < count; i++)
            {
                tasks[i] = DownloadSingleAsync(downloadedImageConsumers[i], cancellationToken);
            }

            await Task.WhenAll(tasks);
        }

        private async Task DownloadSingleAsync(IDownloadedImageConsumer downloadedImageConsumer,
            CancellationToken cancellationToken)
        {
            var image = await ImageDownloader.DownloadImageAsync(ImageUrlProvider.GetUrl(), cancellationToken);
            await downloadedImageConsumer.ConsumeDownloadedImageAsync(image, cancellationToken);
        }
    }
}