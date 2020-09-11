using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour {

	// Use this for initialization
	
        //quits the game
    public void ExitGame()
    {
        Application.Quit();
    }
    //changes the scene based on the index given
    public void ChooseScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        if(GameManager.instance != null)
        {
            GameManager.instance.Reset();
        }
           
    }
}
