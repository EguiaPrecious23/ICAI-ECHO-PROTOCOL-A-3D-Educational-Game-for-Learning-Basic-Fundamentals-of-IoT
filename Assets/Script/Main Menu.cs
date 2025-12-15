using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Cutscene")]
    public int Cutscene= 1;

    [Header("UI Panels")]
    public GameObject mainMenuCanvas;
    public GameObject optionsCanvas;

    public void StartNewGame()
    {
        SceneManager.LoadScene(Cutscene);
    }

    public void OpenOptions()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode
    #else
            Application.Quit(); // Close the standalone build
    #endif
    }
}