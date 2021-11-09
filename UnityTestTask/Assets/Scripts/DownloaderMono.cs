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

        SetDownloadStateActive(false);
    }

    private void OnDestroy()
    {
        _downloadButton.onClick.RemoveListener(_downloadAction);
        _cancelButton.onClick.RemoveListener(_cancelAction);
    }

    private void OnCancelButtonClicked()
    {
        CancelTokenAndDispose();
        SetDownloadStateActive(false);
        _cardViewModelCollection.ResetCollectionToDefault();
    }

    private async Task OnDownloadButtonClicked()
    {
        var downloadStrategyType = _selectedDownloadStrategyMono.DownloadStrategyType;
        var downloadStrategy = _downloadStrategyFactoryMono.DownloadStrategyFactory.Provide(downloadStrategyType);

        SetDownloadStateActive(true);

        _cancellationTokenSource = new CancellationTokenSource();

        _cardViewModelCollection.ResetCollectionToDefault();
        await downloadStrategy.StartDownloadAsync(_cardViewModelCollection.GetConsumers(),
            _cancellationTokenSource.Token);

        CancelTokenAndDispose();
        SetDownloadStateActive(false);
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

    private void SetDownloadStateActive(bool isActive)
    {
        _downloadButton.interactable = !isActive;
        _cancelButton.interactable = isActive;
    }
}