using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Interfaces
{
    public interface IDownloadedImageConsumer
    {
        Task ConsumeDownloadedImageAsync(Texture image, CancellationToken cancellationToken);
    }
}