using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    //pause check
    public static bool isPaused = false;
    public static bool isGameOver = false;
    public GameObject pauseMenuUI;
    [SerializeField] GameObject Hud;

    public Text gameOverText;
    public Image black;
    float alpha = 0f;

    // Update is called once per frame
    void Update ()
    {
        if (isGameOver && alpha < 1f)
        {
            alpha += Time.deltaTime * 0.5f;
            Color newColor = Color.black;
            newColor.a = alpha;
            black.color = newColor;
        }
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}

    //Something needs to trigger a game over

    void Pause()
    {
        Hud.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }


    public void Resume()
    {
        
        Hud.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Pressed");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Satch");
    }

    public void GameOver()
    {
        if (gameOverText == null || black == null)
            return;
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        black.enabled = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
