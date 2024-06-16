using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void home(int sceneID){
        Time.timeScale = 0f;
        SceneManager.LoadScene(sceneID);
    }
}
