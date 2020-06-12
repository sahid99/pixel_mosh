using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayTDM(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void PlayFFA(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }
    public void PlayCTF(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+3);
    }
    public void ExitGame(){
        Application.Quit();   
    }
}
