using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using Ui;
using UnityEngine;

public class DownloadedImageConsumer : IDownloadedImageConsumer
{
    private readonly CardViewModel _cardViewModel;

    public DownloadedImageConsumer(CardViewModel cardViewModel)
    {
        _cardViewModel = cardViewModel;
    }

    public async UniTask ConsumeDownloadedImageAsync(Texture image, CancellationToken cancellationToken)
    {
        //stub for async
        await UniTask.CompletedTask;
        _cardViewModel.SetImage(image);
    }
}