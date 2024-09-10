using System;
using UnityEngine.SceneManagement;

public class GameLoaderImpl : IGameLoadConsumer, IGameLoadProducer
{
    public event Action StartGame;

    public GameLoaderImpl()
    {
        StartGame += OnGameLoad;
    }
    void IGameLoadProducer.StartGame()
    {
        StartGame?.Invoke();
    }
    private void OnGameLoad()
    {
        SceneManager.LoadScene(SceneRoutes.MainGameRoute);
    }
}