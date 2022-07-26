namespace Imgish.Processors;

public interface ImageProcessor
{
    byte[] generateThumbnailFrom(string inputFile);
}