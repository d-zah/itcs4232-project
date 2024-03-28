using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    public LayerMask layersToHit;

    public PlayerController playerController;


    //lerp
    [SerializeField] public Animator anim;
    public Scene currentScene;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float duration = 1f;
    private float elapsedTime;
    private bool lerpActive;
    private bool triggered;
    public bool facingLeft;

    // Update is called once per frame
    void Update()
    {
        Vector2 direction;

        if(facingLeft == true){
            direction = Vector2.left;
        } else {
            direction = Vector2.right;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 50f, layersToHit);
        if(hit.collider == null){
            transform.localScale = new Vector3(50f, .5f, 1);
            return;
        }
        transform.localScale = new Vector3(hit.distance + 0.25f, .5f, 1);
        //Debug.Log(hit.collider.gameObject.name);
        if(hit.collider.tag == "Player"){
            //kill player n reset round
            playerController.playerCanMove(true);
            playerController.makePlayerFall();
            triggered = true;
        } 
        if(triggered == true){
            LerpIsTriggered();
        }
    }

    void LerpIsTriggered() {
        if(lerpActive == false && triggered == true) {
                //begin panel transition
                anim.SetBool("isSceneActive", false);
                lerpActive = true;
            } else if(elapsedTime / duration > .99 && triggered == true) {
                lerpActive = false;
                //reload scene
                currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.buildIndex);
            } else if(triggered == true) {
                elapsedTime += Time.deltaTime;
            }
    }
}
