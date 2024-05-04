using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Scene currentScene;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    private GameObject player;
    [SerializeField] public Animator anim;
    public PlayerController playerController;
    private Animator trapdoorAnim;
    

    //lerp
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float duration = 1f;
    private float elapsedTime;
    private bool lerpActive;
    private bool triggered;
    

    public void Start() {
        trapdoorAnim = GetComponent<Animator>();
        playerPosition = new Vector2(transform.position.x, transform.position.y);
        lerpActive = false;
        triggered = false;
    }

    public void Update() {
            if(lerpActive == false && triggered == true) { //if trapdoor is hit
                //set up lerp
                startPosition = player.transform.position;
                endPosition = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
                
                //begin panel transition
                anim.SetBool("isSceneActive", false);
                trapdoorAnim.SetBool("trapdoorOpen", true);
                playerController.makePlayerFall();
                lerpActive = true;
            } else if(elapsedTime / duration > .99 && triggered == true) { //if lerp is done
                lerpActive = false;
                playerStorage.initialValue = playerPosition;
                //switch to new scene
                currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.buildIndex - 1);
            } else if(triggered == true) { //progress lerp
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / duration;
                player.transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
            }
           
            
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            player = other.gameObject;
            triggered = true;
        }
    }
}
