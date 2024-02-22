using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPanelScript : MonoBehaviour
{
    public GameObject buttonMenu;
    public int targetSelectionCode;
    public Selectable[] selectablesArray;
    public GameObject wipePanel;
    public int elevatorABuildIndex;

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
                // playerStorage.initialValue = playerPosition;
                //currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(elevatorABuildIndex);
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
    }

    public void disableButton(Selectable button){
        button.interactable = false;
    }
}
