using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Метод для выхода из игры
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}