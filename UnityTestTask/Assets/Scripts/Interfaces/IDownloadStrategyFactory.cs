using Enums;

namespace Interfaces
{
    public interface IDownloadStrategyFactory
    {
        IDownloadStrategy Provide(DownloadStrategyType strategyType);
    }
}