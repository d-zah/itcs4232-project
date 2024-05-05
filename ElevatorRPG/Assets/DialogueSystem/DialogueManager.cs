using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue, navButtonText;
    public Image speakerSprite;
    public List<AudioClip> maleSpeakingSounds = new List<AudioClip>();
    public List<AudioClip> femaleSpeakingSounds = new List<AudioClip>();
    public AudioClip elevatorIncorrect;

    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    private Animator anim;
    private Coroutine typing;
    private PlayerController playerController;
    private AudioSource audioSource;

    //make certain this is the only DialogueManager
    private void Awake(){

        audioSource = GetComponent<AudioSource>();

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if(instance == null){
            instance = this;
            anim = GetComponent<Animator>();
        } else {
            Destroy(gameObject);
        }
    }

    public static void StartConversation(Conversation convo){

        instance.anim.SetBool("isOpen", true);
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.navButtonText.text = "Click to continue";

        instance.ReadNext();
    }

    public void ReadNext(){
        if(currentIndex > currentConvo.GetLength()){
            instance.anim.SetBool("isOpen", false);
            playerController.playerCanMove(false);
            return;
        }

        playAudio();

        speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();

        //reset typing if dialogue line is skipped
        if(typing == null){
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        } else {
            audioSource.Stop();
            instance.StopCoroutine(typing);
            typing = null;
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
            playAudio();
        }

        speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();

        currentIndex++;

        if(currentIndex > currentConvo.GetLength()){
            navButtonText.text = "Click to close";
        }
    }

    //typing effect for dialogue
    private IEnumerator TypeText(string text){
        dialogue.text = "";
        bool complete = false;
        int index = 0;

        while(!complete){
            dialogue.text += text[index];
            index++;
            yield return new WaitForSeconds(0.04f);

            if(index == text.Length){
                complete = true;
                audioSource.Stop();
            }
        }

        typing = null;
    }

    private void playAudio(){
        int index = Random.Range(0, maleSpeakingSounds.Count);
        if(currentConvo.GetLineByIndex(currentIndex).isElevator){
            if(currentConvo.GetLineByIndex(currentIndex).isMale){
                audioSource.PlayOneShot(elevatorIncorrect, 0.075f);
            } 
        } else {
            if(currentConvo.GetLineByIndex(currentIndex).isMale){
                audioSource.PlayOneShot(maleSpeakingSounds[index], 0.15f);
            } else {
                audioSource.PlayOneShot(femaleSpeakingSounds[index], 0.15f);
            }
            
        }
    }
}
