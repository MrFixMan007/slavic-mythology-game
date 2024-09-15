using System;
using UnityEngine.SceneManagement;

public class GameLoaderImpl : IGameLoadConsumer, IGameLoadProducer
{
    public event Action StartGame;
    public event Action StartIntroCatScene;

    public GameLoaderImpl()
    {
        StartGame += OnGameLoad;
        StartIntroCatScene += OnIntroCutSceneGameLoad;
    }
    void IGameLoadProducer.StartGame()
    {
        StartGame?.Invoke();
    }

    void IGameLoadProducer.StartIntroCatScene()
    {
        StartIntroCatScene?.Invoke();
    }

    private void OnGameLoad()
    {
        SceneManager.LoadScene(SceneRoutes.MainGameRoute);
    }
    
    private void OnIntroCutSceneGameLoad()
    {
        SceneManager.LoadScene(SceneRoutes.CutsceneRoute);
    }
}