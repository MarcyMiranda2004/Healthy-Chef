using UnityEngine;

public class PausaManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Panel di pausa
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Ferma il tempo di gioco
        isPaused = true;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true); // Mostra il menu pausa
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Riattiva il tempo
        isPaused = false;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false); // Nasconde il menu pausa
    }

    public void QuitGame()
    {
        Debug.Log("Uscita dal gioco...");
        Application.Quit();
    }
}
