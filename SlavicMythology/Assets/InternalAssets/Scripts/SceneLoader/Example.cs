using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Example : MonoBehaviour
{
    private IGameLoadConsumer _sceneLoader;
    public ScreenFader screenFader; // Переменная для ссылки на ScreenFader (опционально)

    private void Start()
    {
        Debug.Log("Example: Start вызван");
    }

    public void Initialize(IGameLoadConsumer sceneLoader)
    {
        _sceneLoader = sceneLoader;
        _sceneLoader.StartGame += OnStartGame;
        Debug.Log("Example: Подписка на StartGame выполнена");
    }

    private void OnStartGame()
    {
        Debug.Log("Example: StartGame вызвано, загружаем сцену асинхронно");
        StartCoroutine(LoadSceneWithFade(SceneRoutes.CutsceneRoute));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Если ScreenFader назначен, затемняем экран
        if (screenFader != null)
        {
            yield return StartCoroutine(screenFader.FadeIn(1f));
        }

        // Если ScreenFader назначен, затемняем экран
        if (screenFader != null)
        {
            yield return StartCoroutine(screenFader.FadeIn(1f));
        }

        // Асинхронная загрузка сцены
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // Постоянное затемнение, показываем прогресс загрузки текстом
        while (!asyncLoad.isDone)
        {
            // Проверяем, достиг ли прогресс 100%
            if (asyncLoad.progress >= 0.9f)
            {
                // Разрешаем активацию сцены только после достижения 100% загрузки
                asyncLoad.allowSceneActivation = true;

                // Убираем затемнение после активации сцены
                if (screenFader != null)
                {
                    yield return StartCoroutine(screenFader.FadeOut(1f));
                }

                yield return null;
            }
        }
    }
}