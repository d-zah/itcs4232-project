using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationStart : MonoBehaviour
{
    public Conversation convo;
    private PlayerController playerController;

    public void Awake(){
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            playerController.playerCanMove(true);
            DialogueManager.StartConversation(convo);
        }
    }
}
