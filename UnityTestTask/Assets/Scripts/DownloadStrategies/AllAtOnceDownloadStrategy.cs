using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
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

        public override async UniTask StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers,
            CancellationToken cancellationToken)
        {
            var count = downloadedImageConsumers.Count;
            var tasks = new UniTask<Texture>[count];

            for (var i = 0; i < count; i++)
            {
                tasks[i] = ImageDownloader.DownloadImageAsync(ImageUrlProvider.GetUrl(), cancellationToken);
            }

            var images = await UniTask.WhenAll(tasks);

            for (var i = 0; i < count; i++)
            {
                await downloadedImageConsumers[i].ConsumeDownloadedImageAsync(images[i], cancellationToken);
            }
        }
    }
}