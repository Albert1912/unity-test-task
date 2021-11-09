using System.Threading;
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
    [SerializeField] private Button _cancelButton;

    private UnityAction _downloadAction;
    private UnityAction _cancelAction;

    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _downloadAction = UniTask.UnityAction(async () => await OnDownloadButtonClicked());
        _cancelAction = OnCancelButtonClicked;

        _downloadButton.onClick.AddListener(_downloadAction);
        _cancelButton.onClick.AddListener(_cancelAction);
    }

    private void OnDestroy()
    {
        _downloadButton.onClick.RemoveListener(_downloadAction);
        _cancelButton.onClick.RemoveListener(_cancelAction);
    }

    private void OnCancelButtonClicked()
    {
        CancelTokenAndDispose();
        _cardViewModelCollection.ResetCollectionToDefault();
    }

    private async Task OnDownloadButtonClicked()
    {
        var downloadStrategyType = _selectedDownloadStrategyMono.DownloadStrategyType;
        var downloadStrategy = _downloadStrategyFactoryMono.DownloadStrategyFactory.Provide(downloadStrategyType);

        _downloadButton.interactable = false;
        _cancelButton.interactable = true;

        _cancellationTokenSource = new CancellationTokenSource();

        _cardViewModelCollection.ResetCollectionToDefault();
        await downloadStrategy.StartDownloadAsync(_cardViewModelCollection.GetConsumers(),
            _cancellationTokenSource.Token);

        CancelTokenAndDispose();

        _downloadButton.interactable = true;
        _cancelButton.interactable = false;
    }

    private void OnApplicationQuit()
    {
        CancelTokenAndDispose();
    }

    private void CancelTokenAndDispose()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;
    }
}