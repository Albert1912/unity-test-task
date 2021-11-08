using System;
using System.Collections.Generic;
using Enums;
using Interfaces;

public class DownloadStrategyFactory : IDownloadStrategyFactory
{
    private readonly Dictionary<DownloadStrategyType, IDownloadStrategy> _downloadStrategies;

    public DownloadStrategyFactory(Dictionary<DownloadStrategyType, IDownloadStrategy> downloadStrategies)
    {
        _downloadStrategies = downloadStrategies;
    }

    public IDownloadStrategy Provide(DownloadStrategyType strategyType)
    {
        if (_downloadStrategies.TryGetValue(strategyType, out var strategy))
        {
            return strategy;
        }

        throw new ArgumentException($"Strategy for type {strategyType} not found");
    }
}