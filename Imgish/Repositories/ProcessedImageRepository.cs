namespace Imgish.Repositories;

public interface ProcessedImageRepository
{
    void saveTo(string outputPath, byte[] generatedThumbnail);
}