using System.Threading.Tasks;
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

    public async Task ConsumeDownloadedImageAsync(Texture image)
    {
        _cardViewModel.SetImage(image);
    }
}