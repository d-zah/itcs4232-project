using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPanelScript : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject buttonMenu;
    public int targetSelectionCode;
    public Selectable[] selectablesArray;
    public VectorValue playerStorage;
    public GameObject wipePanel;
    public int elevatorBuildIndex;

    private int totalSelections;
    private int selectionCode;
    private bool triggered;
    private bool timerActive;
    private float duration = 1f;
    private float elapsedTime;

    void Start()
    {
        totalSelections = 0;
        selectionCode = 0;
        triggered = false;
        timerActive = false;
    }

    void Update(){
        //timer
            if(timerActive == false && triggered == true) {
                GameObject panel = Instantiate(wipePanel, Vector3.zero, Quaternion.identity) as GameObject;
                Destroy(panel, 2);
                timerActive = true;
            } else if(elapsedTime / duration > .99 && triggered == true) {
                timerActive = false;
                playerStorage.initialValue = new Vector2(0f, 0f);
                //currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(elevatorBuildIndex);
            } else if(triggered == true) {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / duration;
            }
    
    }

    public void addSelection(int selection){

        selectionCode += selection;
        totalSelections++;
        //Debug.Log(totalSelections);
        if(totalSelections == 4){
            if(selectionCode == targetSelectionCode){
                //open elevator
                triggered = true;
                //goBack();
            } else {
                Debug.Log("incorrect");
                goBack();
            }
            foreach (Selectable selectableUI in selectablesArray) {
                //make all buttons selectable again
                selectableUI.interactable = true;
            }
            //reset password detector
            totalSelections = 0;
            selectionCode = 0;
        }
    }

    public void goBack(){
        buttonMenu.SetActive(false);
        playerController.playerCanMove(false);
    }

    public void disableButton(Selectable button){
        button.interactable = false;
    }
}
