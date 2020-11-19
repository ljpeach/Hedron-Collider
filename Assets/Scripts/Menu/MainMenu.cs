using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int firstLevelIndex;
    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevelIndex);
    }

    public void SetDifficulty(int dif)
    {
        DifficultySetting.difficulty = dif;
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
