using Imgish.Presenters;
using Imgish.Processors;
using Imgish.Repositories;

namespace Imgish.UseCases;

public class CreateDefaultThumbnailFromImage
{
    private readonly ImageProcessor _imageProcessor;
    private readonly ProcessedImageRepository _processedImageRepository;
    private readonly Presenter _thumbnailPresenter; 
    public CreateDefaultThumbnailFromImage(ImageProcessor processor, ProcessedImageRepository repository, Presenter presenter)
    {
        _imageProcessor = processor;
        _processedImageRepository = repository;
        _thumbnailPresenter = presenter;
    }
    
    public void Invoke(string inputFilePath, string outputDirectory)
    {
        try
        {
            var fileName =
                $"{outputDirectory}{Path.GetFileNameWithoutExtension(inputFilePath)}_thumbnail{Path.GetExtension(inputFilePath)}";
            var thumbnail = _imageProcessor.generateThumbnailFrom(inputFilePath);
            _processedImageRepository.saveTo(fileName, thumbnail);
            _thumbnailPresenter.displaySuccessfulGenerationMessage();
        }
        catch (Exception e)
        {
            _thumbnailPresenter.displayUnsuccessfulGenerationMessage();
        }
    }
}