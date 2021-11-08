using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces;

namespace DownloadStrategies
{
    public class WhenImageReadyDownloadStrategy : IDownloadStrategy
    {
        private readonly IImageDownloader _imageDownloader;
        private readonly IImageUrlProvider _imageUrlProvider;

        public WhenImageReadyDownloadStrategy(IImageDownloader imageDownloader,
            IImageUrlProvider imageUrlProvider)
        {
            _imageDownloader = imageDownloader;
            _imageUrlProvider = imageUrlProvider;
        }

        public async Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers)
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
            var image = await _imageDownloader.DownloadImageAsync(_imageUrlProvider.GetUrl());
            await downloadedImageConsumer.ConsumeDownloadedImageAsync(image);
        }
    }
}