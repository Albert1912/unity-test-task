using Enums;
using UnityEngine;

public class SelectedDownloadStrategyMono : MonoBehaviour
{
    [SerializeField] private DownloadStrategyType _downloadStrategyType;
    public DownloadStrategyType DownloadStrategyType => _downloadStrategyType;

    public void OnDownloadStrategyTypeChanged(int newType)
    {
        _downloadStrategyType = (DownloadStrategyType) newType;
    }
}