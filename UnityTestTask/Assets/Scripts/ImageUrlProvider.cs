using Interfaces;

public class ImageUrlProvider : IImageUrlProvider
{
    private const string BaseUrl = "https://picsum.photos";
    private readonly int _width;
    private readonly int _height;

    public ImageUrlProvider(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public string GetUrl()
    {
        return $"{BaseUrl}/{_width}/{_height}";
    }
}