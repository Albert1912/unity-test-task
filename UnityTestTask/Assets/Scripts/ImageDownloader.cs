using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader : IImageDownloader
{
    public async Task<Texture> DownloadImageAsync(string url)
    {
        using (var webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            await webRequest.SendWebRequest();

            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.LogError($"An error occured while downloading image from {url}.Http code: {webRequest.responseCode}. Message: {webRequest.error}");
                return null;
            }

            return DownloadHandlerTexture.GetContent(webRequest);
        }
    }
}