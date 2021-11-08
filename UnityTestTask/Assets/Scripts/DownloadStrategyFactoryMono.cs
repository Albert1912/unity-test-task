using System.Collections.Generic;
using DownloadStrategies;
using Enums;
using Interfaces;
using UnityEngine;

public class DownloadStrategyFactoryMono : MonoBehaviour
{
    public IDownloadStrategyFactory DownloadStrategyFactory { get; private set; }

    private void Awake()
    {
        IImageDownloader imageDownloader = new ImageDownloader();
        IImageUrlProvider imageUrlProvider = new ImageUrlProvider(200, 200);

        DownloadStrategyFactory = new DownloadStrategyFactory(
            new Dictionary<DownloadStrategyType, IDownloadStrategy>
            {
                [DownloadStrategyType.AllAtOnce] = new AllAtOnceDownloadStrategy(imageDownloader, imageUrlProvider),
                [DownloadStrategyType.OneByOne] = new OneByOneDownloadStrategy(imageDownloader, imageUrlProvider),
                [DownloadStrategyType.WhenImageReady] =
                    new WhenImageReadyDownloadStrategy(imageDownloader, imageUrlProvider)
            });
    }
}