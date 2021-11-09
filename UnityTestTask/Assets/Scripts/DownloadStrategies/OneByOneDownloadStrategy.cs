using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;

namespace DownloadStrategies
{
    public class OneByOneDownloadStrategy : BaseDownloadStrategy
    {

        public OneByOneDownloadStrategy(IImageDownloader imageDownloader,
            IImageUrlProvider imageUrlProvider) : base(imageDownloader, imageUrlProvider)
        {
        }

        public override async UniTask StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers,
            CancellationToken cancellationToken)
        {
            foreach (var downloadedImageConsumer in downloadedImageConsumers)
            {
                var image = await ImageDownloader.DownloadImageAsync(ImageUrlProvider.GetUrl(), cancellationToken);
                await downloadedImageConsumer.ConsumeDownloadedImageAsync(image, cancellationToken);
            }
        }
    }
}