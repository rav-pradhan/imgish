using System.Net.Security;

namespace Imgish.Repositories;

public class LocalFileRepository : ProcessedImageRepository
{
    public void saveTo(string outputPath, byte[] generatedThumbnail)
    {
        File.WriteAllBytes(outputPath, generatedThumbnail);
    }
}