using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public VectorValue playerStorage;

    public void PlayGame()
    {
        LoadPlayer();
        // SceneManager.LoadScene("Floor1");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }

    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path)){
            PlayerData data = SaveSystem.LoadPlayer();

            Vector2 insertPosition = Vector2.zero;
            insertPosition.x = data.position[0];
            insertPosition.y = data.position[1];
            playerStorage.initialValue = insertPosition;

            playerStorage.elevatorProgress = data.elevatorProgress;

            SceneManager.LoadScene(data.floor);
        } else {
            SceneManager.LoadScene("Floor1");
        }
    }

    public void ResetPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path)){
            PlayerData data = SaveSystem.LoadPlayer();

            Vector2 insertPosition = Vector2.zero;
            insertPosition.y = -3;
            playerStorage.initialValue = insertPosition;

            playerStorage.elevatorProgress = 0;

            SceneManager.LoadScene("Floor1");
        } else {
            SceneManager.LoadScene("Floor1");
        }
    }
}