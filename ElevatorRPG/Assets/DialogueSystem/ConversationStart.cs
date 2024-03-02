using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationStart : MonoBehaviour
{
    public Conversation convo;

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            DialogueManager.StartConversation(convo);
        }
    }
}
