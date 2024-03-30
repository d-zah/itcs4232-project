using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ladder : MonoBehaviour
{
    public Scene currentScene;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    private GameObject player;
    [SerializeField] public Animator anim;
    //public PlayerController playerController;
    public int destinationBuildIndex;
    

    //lerp
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float duration = 1f;
    private float elapsedTime;
    private bool lerpActive;
    private bool triggered;
    

    public void Start() {
        playerPosition = new Vector2(transform.position.x, transform.position.y);
        lerpActive = false;
        triggered = false;
    }

    public void Update() {
            if(lerpActive == false && triggered == true) {
                //set up lerp
                startPosition = player.transform.position;
                endPosition = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
                
                //begin panel transition
                anim.SetBool("isSceneActive", false);
                //playerController.makePlayerFall();
                lerpActive = true;
            } else if(elapsedTime / duration > .99 && triggered == true) {
                lerpActive = false;
                playerStorage.initialValue = playerPosition;
                //switch to new scene
                currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(destinationBuildIndex);
            } else if(triggered == true) {
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
