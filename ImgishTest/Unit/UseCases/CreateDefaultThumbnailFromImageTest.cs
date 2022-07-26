using Imgish.Presenters;
using Imgish.Processors;
using Imgish.Repositories;
using Imgish.UseCases;

namespace ImgishTest.Unit.UseCases;

public class CreateDefaultThumbnailFromImageTest
{
    private const string InputFilePath = "./test/penguin.jpg";
    private const string OutputDirectory = "./outputs/";
    private MockRepository _mockRepository = null!;
    private MockPresenter _mockPresenter = null!;
    
    [SetUp]
    public void Setup()
    {
        _mockRepository = new MockRepository();
        _mockPresenter = new MockPresenter();
    }
    
    [Test]
    public void GenerateThumbnailIsCalledByImageProcessorWhenValidInput()
    {
        const bool hasErrored = false;
        var mockProcessor = new MockProcessor(hasErrored);
        var useCase = new CreateDefaultThumbnailFromImage(mockProcessor, _mockRepository, _mockPresenter);

        useCase.Invoke(InputFilePath, OutputDirectory);
        Assert.That(mockProcessor.GenerateThumbnailCalls, Is.EqualTo(1));
    }
    
    [Test]
    public void SaveToIsCalledByRepositoryWhenValidInput()
    {
        const bool hasErrored = false;
        var mockProcessor = new MockProcessor(hasErrored);
        var useCase = new CreateDefaultThumbnailFromImage(mockProcessor, _mockRepository, _mockPresenter);

        useCase.Invoke(InputFilePath, OutputDirectory);
        Assert.That(_mockRepository.SaveToCalls, Is.EqualTo(1));
    }
    
    [Test]
    public void SuccessfulMessageIsCalledByPresenterWhenValidInput()
    {
        const bool hasErrored = false;
        var mockProcessor = new MockProcessor(hasErrored);
        var useCase = new CreateDefaultThumbnailFromImage(mockProcessor, _mockRepository, _mockPresenter);

        useCase.Invoke(InputFilePath, OutputDirectory);
        Assert.That(_mockPresenter.DisplaySuccessfulGenerationMessageCalls, Is.EqualTo(1));
    }
    
    [Test]
    public void DisplayUnsuccessfulGenerationMessageWhenExceptionRaised()
    {
        const bool hasErrored = true;
        var mockProcessor = new MockProcessor(hasErrored);
        var useCase = new CreateDefaultThumbnailFromImage(mockProcessor, _mockRepository, _mockPresenter);

        useCase.Invoke(InputFilePath, OutputDirectory);
        Assert.That(_mockPresenter.DisplayUnsuccessfulGenerationMessageCalls, Is.EqualTo(1));
    }
}

public class MockPresenter : Presenter
{
    public int DisplaySuccessfulGenerationMessageCalls { get; private set; }
    public int DisplayUnsuccessfulGenerationMessageCalls { get; private set; }

    public void displaySuccessfulGenerationMessage()
    {
        DisplaySuccessfulGenerationMessageCalls++;
    }

    public void displayUnsuccessfulGenerationMessage()
    {
        DisplayUnsuccessfulGenerationMessageCalls++;
    }
}

public class MockProcessor : ImageProcessor
{
    private readonly bool _hasErrored;
    public MockProcessor(bool hasErrored)
    {
        _hasErrored = hasErrored;
    }

    public int GenerateThumbnailCalls { get; private set; }

    public byte[] generateThumbnailFrom(string inputFile)
    {
        if (_hasErrored)
        {
            throw new Exception("failed to process image");
        }
        GenerateThumbnailCalls++;
        var mockThumbnail = new byte[3];
        return mockThumbnail;
    }
}

public class MockRepository : ProcessedImageRepository
{
    
    public int SaveToCalls { get; private set; }

    public void saveTo(string outputPath, byte[] generatedThumbnail)
    {
        SaveToCalls++;
    }
}
