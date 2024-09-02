using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivation : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject buttonMenu;
    public GameObject pressEPrompt;
    private bool isInTrigger;


    public void Awake(){
        isInTrigger = false;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(!(other.CompareTag("Player") && !other.isTrigger)) return;

        isInTrigger = true;
        pressEPrompt.SetActive(true);
        
    }

    public void OnTriggerExit2D(Collider2D other){
        if(!(other.CompareTag("Player") && !other.isTrigger)) return;

        isInTrigger = false;
        pressEPrompt.SetActive(false);
        
    }

    public void Update(){
        if(!isInTrigger) return;
        if(!Input.GetKeyDown(KeyCode.E)) return;

        playerController.playerCanMove(true);
        buttonMenu.SetActive(true);
        
    }

}
