using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonInsidePanelScript : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject buttonMenu;
    public Selectable[] selectablesArray;
    public int destinationBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    private GameObject player;
    [SerializeField] private Animator anim;

    private bool triggered;
    private bool timerActive;
    private float duration = 1f;
    private float elapsedTime;

    private const int ELEVATOR_A_INDEX = 14;
    private const int ELEVATOR_B_INDEX = 15;
    private const int ELEVATOR_C_INDEX = 16;
    private const int ELEVATOR_D_INDEX = 17;
    private const int ELEVATOR_E_INDEX = 18;

    void Start()
    {
        triggered = false;
        timerActive = false;
    }

    void Update(){
        //timer
            if(timerActive == false && triggered == true) {
                anim.SetBool("isSceneActive", false);
                timerActive = true;
            } else if(elapsedTime / duration > .99 && triggered == true) {
                timerActive = false;
                // playerStorage.initialValue = playerPosition;
                int currentScene = SceneManager.GetActiveScene().buildIndex;
                switch (currentScene){
                    case ELEVATOR_A_INDEX:
                        playerStorage.initialValue = new Vector2(-6f, 3.75f); 
                        break;
                    case ELEVATOR_B_INDEX:
                        playerStorage.initialValue = new Vector2(-3f, 3.75f); 
                        break;
                    case ELEVATOR_C_INDEX:
                        playerStorage.initialValue = new Vector2(-0f, 3.75f); 
                        break;
                    case ELEVATOR_D_INDEX:
                        playerStorage.initialValue = new Vector2(3f, 3.75f); 
                        break;
                    case ELEVATOR_E_INDEX:
                        playerStorage.initialValue = new Vector2(6f, 3.75f); 
                        break;
                    
                }
                SceneManager.LoadScene(destinationBuildIndex);
            } else if(triggered == true) {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / duration;
            }
    
    }

    public void addSelection(int selection){
        // Debug.Log("selected");
        destinationBuildIndex = selection;
        triggered = true;

    }

    public void goBack(){
        buttonMenu.SetActive(false);
        playerController.playerCanMove(false);
    }

    public void disableButton(Selectable button){
        button.interactable = false;
    }
}
