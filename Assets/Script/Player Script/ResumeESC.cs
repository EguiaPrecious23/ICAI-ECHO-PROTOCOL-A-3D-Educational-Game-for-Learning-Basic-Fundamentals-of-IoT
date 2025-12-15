using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeESC : MonoBehaviour
{
    [Header("Resume But")]
    public GameObject pauseMenuUI;
    public GameObject settingsVolume;

    [Header("Keybind")]
    public KeyCode escape = KeyCode.Escape;

    [Header("Movement Disabled")]
    public MonoBehaviour playerMovementScript;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(escape))
        {
            if (!isPaused)
                PauseGame();
            else if (isPaused && pauseMenuUI.activeSelf)
                ResumeGame();
            else if (isPaused && settingsVolume.activeSelf)
            {
                OpenPauseMenu();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        settingsVolume.SetActive(false);
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerMovementScript != null) playerMovementScript.enabled = true;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        settingsVolume.SetActive(false);
        isPaused = true;

        foreach (var hover in pauseMenuUI.GetComponentsInChildren<HoverEffect>(true))
        {
            hover.ResetColor();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerMovementScript != null) playerMovementScript.enabled = false;
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsVolume.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        settingsVolume.SetActive(false);
        pauseMenuUI.SetActive(true);

        foreach (var hover in pauseMenuUI.GetComponentsInChildren<HoverEffect>(true))
        {
            hover.ResetColor();
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}