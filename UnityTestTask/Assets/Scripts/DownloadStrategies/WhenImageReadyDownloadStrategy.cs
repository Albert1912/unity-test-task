using System.Collections.Generic;
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

        public override async Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers)
        {
            var count = downloadedImageConsumers.Count;
            var tasks = new Task[count];

            for (var i = 0; i < count; i++)
            {
                tasks[i] = DownloadSingleAsync(downloadedImageConsumers[i]);
            }

            await Task.WhenAll(tasks);
        }

        private async Task DownloadSingleAsync(IDownloadedImageConsumer downloadedImageConsumer)
        {
            var image = await ImageDownloader.DownloadImageAsync(ImageUrlProvider.GetUrl());
            await downloadedImageConsumer.ConsumeDownloadedImageAsync(image);
        }
    }
}