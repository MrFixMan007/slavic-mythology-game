using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Example : MonoBehaviour
{
    private IGameLoadConsumer _sceneLoader;
    public ScreenFader screenFader; // ���������� ��� ������ �� ScreenFader (�����������)

    private void Start()
    {
        Debug.Log("Example: Start ������");
    }

    public void Initialize(IGameLoadConsumer sceneLoader)
    {
        _sceneLoader = sceneLoader;
        _sceneLoader.StartGame += OnStartGame;
        Debug.Log("Example: �������� �� StartGame ���������");
    }

    private void OnStartGame()
    {
        Debug.Log("Example: StartGame �������, ��������� ����� ����������");
        StartCoroutine(LoadSceneWithFade(SceneRoutes.CutsceneRoute));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        // ���� ScreenFader ��������, ��������� �����
        if (screenFader != null)
        {
            yield return StartCoroutine(screenFader.FadeIn(1f));
        }

        // ���� ScreenFader ��������, ��������� �����
        if (screenFader != null)
        {
            yield return StartCoroutine(screenFader.FadeIn(1f));
        }

        // ����������� �������� �����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // ���������� ����������, ���������� �������� �������� �������
        while (!asyncLoad.isDone)
        {
            // ���������, ������ �� �������� 100%
            if (asyncLoad.progress >= 0.9f)
            {
                // ��������� ��������� ����� ������ ����� ���������� 100% ��������
                asyncLoad.allowSceneActivation = true;

                // ������� ���������� ����� ��������� �����
                if (screenFader != null)
                {
                    yield return StartCoroutine(screenFader.FadeOut(1f));
                }

                yield return null;
            }
        }
    }
}