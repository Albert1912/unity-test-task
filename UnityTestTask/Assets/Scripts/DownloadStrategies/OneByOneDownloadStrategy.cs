using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces;

namespace DownloadStrategies
{
    public class OneByOneDownloadStrategy : BaseDownloadStrategy
    {

        public OneByOneDownloadStrategy(IImageDownloader imageDownloader,
            IImageUrlProvider imageUrlProvider) : base(imageDownloader, imageUrlProvider)
        {
        }

        public override async Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers)
        {
            foreach (var downloadedImageConsumer in downloadedImageConsumers)
            {
                var image = await ImageDownloader.DownloadImageAsync(ImageUrlProvider.GetUrl());
                await downloadedImageConsumer.ConsumeDownloadedImageAsync(image);
            }
        }
    }
}