using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour {

    Scene nextScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void LoadNextScene()
    {
        SceneManager.LoadScene("MainScene");
    } 

    public void GameQuit()
    {
        Application.Quit();
    }
}
