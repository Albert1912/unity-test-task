using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Ui;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DownloaderMono : MonoBehaviour
{
    [SerializeField] private SelectedDownloadStrategyMono _selectedDownloadStrategyMono;
    [SerializeField] private DownloadStrategyFactoryMono _downloadStrategyFactoryMono;
    [SerializeField] private CardViewModelCollection _cardViewModelCollection;

    [Space] [SerializeField] private Button _downloadButton;

    private UnityAction _downloadAction;

    private void Awake()
    {
        _downloadAction = UniTask.UnityAction(async () => await OnDownloadButtonClicked());

        _downloadButton.onClick.AddListener(_downloadAction);
    }

    private void OnDestroy()
    {
        _downloadButton.onClick.RemoveListener(_downloadAction);
    }

    private async Task OnDownloadButtonClicked()
    {
        var downloadStrategyType = _selectedDownloadStrategyMono.DownloadStrategyType;
        var downloadStrategy = _downloadStrategyFactoryMono.DownloadStrategyFactory.Provide(downloadStrategyType);

        _downloadButton.interactable = false;

        _cardViewModelCollection.ResetCollectionToDefault();
        await downloadStrategy.StartDownloadAsync(_cardViewModelCollection.GetConsumers());

        _downloadButton.interactable = true;
    }
}