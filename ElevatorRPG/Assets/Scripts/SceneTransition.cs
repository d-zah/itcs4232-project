using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int sceneToLoad;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float duration = 1f;
    private float elapsedTime;
    private bool lerpActive;
    private bool triggered;
    private GameObject player;

    public void Start() {
        lerpActive = false;
        triggered = false;
    }

    public void Update() {
            if(lerpActive == false && triggered == true) {
                startPosition = player.transform.position;
                endPosition = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
                lerpActive = true;
            } else if(elapsedTime / duration > .99 && triggered == true) {
                lerpActive = false;
                SceneManager.LoadScene(sceneToLoad);
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
