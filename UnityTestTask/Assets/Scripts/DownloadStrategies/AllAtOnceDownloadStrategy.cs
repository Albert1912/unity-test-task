using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;
using UnityEngine;

namespace DownloadStrategies
{
    public class AllAtOnceDownloadStrategy : BaseDownloadStrategy
    {
        public AllAtOnceDownloadStrategy(IImageDownloader imageDownloader,
            IImageUrlProvider imageUrlProvider) : base(imageDownloader, imageUrlProvider)
        {
        }

        public override async Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers,
            CancellationToken cancellationToken)
        {
            var count = downloadedImageConsumers.Count;
            var tasks = new Task<Texture>[count];

            for (var i = 0; i < count; i++)
            {
                tasks[i] = ImageDownloader.DownloadImageAsync(ImageUrlProvider.GetUrl(), cancellationToken);
            }

            var images = await Task.WhenAll(tasks);

            for (var i = 0; i < count; i++)
            {
                await downloadedImageConsumers[i].ConsumeDownloadedImageAsync(images[i], cancellationToken);
            }
        }
    }
}