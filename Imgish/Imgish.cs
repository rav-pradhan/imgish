using System.Reflection;
using FluentArgs;
using Imgish.Presenters;
using Imgish.Processors;
using Imgish.Repositories;
using Imgish.UseCases;

namespace Imgish;

public class Imgish
{
    public static void Main(String[] args)
    {
        Presenter consolePresenter = new ConsolePresenter();
        ProcessedImageRepository repository = new LocalFileRepository();
        ImageProcessor imageProcessor = new ImageSharpAdapter();
        var createDefaultThumbnail = new CreateDefaultThumbnailFromImage(imageProcessor, repository, consolePresenter);
        
        FluentArgsBuilder.New()
            .Parameter("-i", "--input").IsRequired()
            .Parameter("-o", "--output").IsRequired()
            .Call(outputDirectory => inputFile =>
            {
                createDefaultThumbnail.Invoke(inputFile, outputDirectory);
            })
            .Parse(args);
    }
}