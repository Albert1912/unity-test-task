using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces;

namespace DownloadStrategies
{
    public class OneByOneDownloadStrategy : IDownloadStrategy
    {
        private readonly IImageDownloader _imageDownloader;
        private readonly IImageUrlProvider _imageUrlProvider;

        public OneByOneDownloadStrategy(IImageDownloader imageDownloader,
            IImageUrlProvider imageUrlProvider)
        {
            _imageDownloader = imageDownloader;
            _imageUrlProvider = imageUrlProvider;
        }

        public async Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers)
        {
            foreach (var downloadedImageConsumer in downloadedImageConsumers)
            {
                var image = await _imageDownloader.DownloadImageAsync(_imageUrlProvider.GetUrl());
                await downloadedImageConsumer.ConsumeDownloadedImageAsync(image);
            }
        }
    }
}