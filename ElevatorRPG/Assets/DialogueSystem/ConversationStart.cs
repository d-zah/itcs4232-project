using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationStart : MonoBehaviour
{
    public Conversation convo;
    private PlayerController playerController;
    [SerializeField] private bool canTalkToTwice;
    [SerializeField] private bool requiresEPress;
    [SerializeField] private bool hasTalkedTo;
    [SerializeField] private bool isBoss;
    private bool isTalkingTo;
    public GameObject pressEPrompt;
    private bool isInTrigger;

    public void Awake(){
        isInTrigger = false;
        hasTalkedTo = false;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            isInTrigger = true;
            if(requiresEPress) pressEPrompt.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger) {
            isInTrigger = false;
            isTalkingTo = false;
            if(requiresEPress) pressEPrompt.SetActive(false);
            if(isBoss) SceneManager.LoadScene(20); //reset to win screen
        }
    }

    public void Update(){
        if(!isInTrigger) return;
        if(!canTalkToTwice && hasTalkedTo) return;
        if(isTalkingTo) return;
        if(!Input.GetKeyDown(KeyCode.E) && requiresEPress) return;
        if(requiresEPress) pressEPrompt.SetActive(false);

        playerController.playerCanMove(true);
        DialogueManager.StartConversation(convo);
        isTalkingTo = true;
        hasTalkedTo = true;
        
    }
}
