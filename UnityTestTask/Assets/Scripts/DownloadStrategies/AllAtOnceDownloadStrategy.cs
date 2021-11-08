using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces;
using UnityEngine;

namespace DownloadStrategies
{
    public class AllAtOnceDownloadStrategy : IDownloadStrategy
    {
        private readonly IImageDownloader _imageDownloader;
        private readonly IImageUrlProvider _imageUrlProvider;

        public AllAtOnceDownloadStrategy(IImageDownloader imageDownloader,
            IImageUrlProvider imageUrlProvider)
        {
            _imageDownloader = imageDownloader;
            _imageUrlProvider = imageUrlProvider;
        }

        public async Task StartDownloadAsync(List<IDownloadedImageConsumer> downloadedImageConsumers)
        {
            var count = downloadedImageConsumers.Count;
            var tasks = new Task<Texture>[count];

            for (var i = 0; i < count; i++)
            {
                tasks[i] = _imageDownloader.DownloadImageAsync(_imageUrlProvider.GetUrl());
            }

            var images = await Task.WhenAll(tasks);

            for (var i = 0; i < count; i++)
            {
                await downloadedImageConsumers[i].ConsumeDownloadedImageAsync(images[i]);
            }
        }
    }
}