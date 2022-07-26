using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Imgish.Processors;

public class ImageSharpAdapter : ImageProcessor
{
    public byte[] generateThumbnailFrom(string inputFile)
    {
        using var memoryStream = new MemoryStream();
        var image = Image.Load(inputFile) ?? throw new ImageProcessingException("Image could not be loaded");
        
        var width = SetThumbnailWidth(image);
        var height = SetThumbnailHeight(image);
        
        image.Mutate(x => x.Resize(width, height));
        image.Save(memoryStream, new JpegEncoder());
        return memoryStream.ToArray();
    }

    private static int SetThumbnailHeight(IImageInfo image)
    {
        return image.Height / 3;
    }

    private static int SetThumbnailWidth(IImageInfo image)
    {
        return image.Width / 3;
    }
}