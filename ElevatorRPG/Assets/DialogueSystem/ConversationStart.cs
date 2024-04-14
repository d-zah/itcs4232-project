using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationStart : MonoBehaviour
{
    public Conversation convo;
    private PlayerController playerController;
    [SerializeField] private bool canTalkToTwice;
    [SerializeField] private bool hasTalkedTo;

    public void Awake(){
        hasTalkedTo = false;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(!canTalkToTwice && hasTalkedTo) return;
        if(other.CompareTag("Player") && !other.isTrigger) {
            playerController.playerCanMove(true);
            DialogueManager.StartConversation(convo);
            hasTalkedTo = true;
        }
    }
}
