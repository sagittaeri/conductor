using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        if (!SceneManager.GetSceneByName("_Game").isLoaded)
        {
            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame()
    {
        var canvasGroup = gameObject.GetComponent<CanvasGroup>();
        LeanTween.value(gameObject, (f) => canvasGroup.alpha = f, 1f, 0f, 0.3f);

        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene("_Game", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
