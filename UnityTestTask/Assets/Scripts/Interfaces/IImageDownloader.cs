using System.Threading.Tasks;
using UnityEngine;

namespace Interfaces
{
    public interface IImageDownloader
    {
        Task<Texture> DownloadImageAsync(string url);
    }
}