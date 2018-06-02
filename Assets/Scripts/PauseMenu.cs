using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenu;
    public bool gameOver = false;
    public bool pauseActive = false;

	private void Awake()
	{
		Time.timeScale = 1f;
	}

	void Update ()
	{
        if (Input.GetButtonDown("Cancel") && gameOver == false)
        {
            if (pauseActive)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

	public void Gameover()
	{
		gameOver = true;
		Time.timeScale = 0f;
	}


    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseActive = false;
    }


	public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseActive = true;
    }
}
