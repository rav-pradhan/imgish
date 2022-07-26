namespace Imgish.Presenters;

public class ConsolePresenter : Presenter
{
    public void displaySuccessfulGenerationMessage()
    {
        Console.WriteLine("Successfully processed image.");
    }

    public void displayUnsuccessfulGenerationMessage()
    {
        Console.WriteLine("Failed to process image.");
    }
}