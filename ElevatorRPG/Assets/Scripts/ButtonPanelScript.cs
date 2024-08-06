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
    public Animator anim;
    public int elevatorBuildIndex;
    public Conversation convo;
    public List<AudioClip> audioClickSounds = new List<AudioClip>();
    public AudioClip elevatorDing;
    public int elevatorNumber;

    private int totalSelections;
    private int selectionCode;
    private bool triggered;
    private bool timerActive;
    private float duration = 1f;
    private float elapsedTime;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        totalSelections = 0;
        selectionCode = 0;
        triggered = false;
        timerActive = false;

        if(playerStorage.elevatorProgress >= elevatorNumber){
            triggered = true;
        }
    }

    void Update(){
        //timer
            if(timerActive == false && triggered == true) {
                anim.SetBool("isSceneActive", false);
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
                switch(elevatorBuildIndex) {
                    case 14:
                        if(playerStorage.elevatorProgress < 1)
                            playerStorage.elevatorProgress = 1;
                        break;
                    case 15:
                        if(playerStorage.elevatorProgress < 2)
                            playerStorage.elevatorProgress = 2;
                        break;
                    case 16:
                        if(playerStorage.elevatorProgress < 3)
                            playerStorage.elevatorProgress = 3;
                        break;
                    case 17:
                        if(playerStorage.elevatorProgress < 4)
                            playerStorage.elevatorProgress = 4;
                        break;
                    case 18:
                        if(playerStorage.elevatorProgress < 5)
                            playerStorage.elevatorProgress = 5;
                        break;
                    default:
                        Debug.Log("Error: invalid elevator index");
                        break;
                }
                audioSource.PlayOneShot(elevatorDing, 0.25f);
                triggered = true;
                //goBack();

                foreach (Selectable selectableUI in selectablesArray) {
                    //make all buttons selectable again
                    selectableUI.interactable = true;
                }
                //reset password detector
                totalSelections = 0;
                selectionCode = 0;

            } else { //wrong
            
                DialogueManager.StartConversation(convo);
                goBack();
            }
            
        } else {
            int index = Random.Range(0, audioClickSounds.Count);
            audioSource.PlayOneShot(audioClickSounds[index], 0.5f);
        }
    }

    public void goBack(){
        foreach (Selectable selectableUI in selectablesArray) {
            //make all buttons selectable again
            selectableUI.interactable = true;
        }
        //reset password detector
        totalSelections = 0;
        selectionCode = 0;

        buttonMenu.SetActive(false);
        playerController.playerCanMove(false);
    }

    public void disableButton(Selectable button){
        button.interactable = false;
    }
}
