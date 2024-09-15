using VContainer;

public class MainMenuViewModel
{
    public MainMenuState MenuState;
    private readonly IGameLoadProducer _gameLoadProducer;

    [Inject]
    public MainMenuViewModel(IGameLoadProducer gameLoadProducer)
    {
        MenuState = new MainMenuState(
            title: "Title",
            helloText: "Hello");
        _gameLoadProducer = gameLoadProducer;
    }

    public void StartGame()
    {
        _gameLoadProducer.StartIntroCatScene();
    }
}