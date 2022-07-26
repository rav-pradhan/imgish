using Imgish.Presenters;
using Imgish.Processors;
using Imgish.Repositories;
using Imgish.UseCases;
using SixLabors.ImageSharp;

namespace ImgishTest.Acceptance;

public class CreateDefaultThumbnailFromImageTest
{
    private string _outputDirectory;
    private string _inputFile;

    [TearDown]
    public void TearDown()
    {
        var outputDirectoryInfo = new DirectoryInfo(_outputDirectory);
        foreach (var file in outputDirectoryInfo.GetFiles())
        {
            file.Delete();
        }
    }

    [Test]
    public void CanCreateThumbnailFromImageAndOutputToDirectory()
    {
        var consolePresenter = new ConsolePresenter();
        var localFileRepository = new LocalFileRepository();
        var imageProcessor = new ImageSharpAdapter();
        var createThumbnailFromImage = new CreateDefaultThumbnailFromImage(imageProcessor, localFileRepository, consolePresenter);

        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;
        
        _inputFile = projectDirectory + "/Acceptance/inputs/penguin.jpeg";
        _outputDirectory = projectDirectory + "/Acceptance/outputs/";
        
        createThumbnailFromImage.Invoke(_inputFile, _outputDirectory);

        var expectedFileOutput = projectDirectory + "/Acceptance/outputs/penguin_thumbnail.jpeg";
        Assert.That(File.Exists(expectedFileOutput));
    }

    [Test]
    public void DisplayFailMessageWhenImageIsNotFound()
    {
        var consolePresenter = new ConsolePresenter();
        var localFileRepository = new LocalFileRepository();
        var imageProcessor = new ImageSharpAdapter();
        var createThumbnailFromImage = new CreateDefaultThumbnailFromImage(imageProcessor, localFileRepository, consolePresenter);

        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;
        
        _inputFile = projectDirectory + "/Acceptance/inputs/nothing_here.txt";
        _outputDirectory = projectDirectory + "/Acceptance/outputs/";

        var expectedFileOutput = projectDirectory + "/Acceptance/outputs/nothing_here.txt";
        
        createThumbnailFromImage.Invoke(_inputFile, _outputDirectory);
        Assert.That(!File.Exists(expectedFileOutput));
    }
}