using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader : IImageDownloader
{
    public async UniTask<Texture> DownloadImageAsync(string url, CancellationToken cancellationToken)
    {
        using (var webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            await webRequest.SendWebRequest().WithCancellation(cancellationToken);

            return DownloadHandlerTexture.GetContent(webRequest);
        }
    }
}