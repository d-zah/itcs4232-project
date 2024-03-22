using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterMovement : MonoBehaviour
{
    public Vector2 emitterPosition;

    //lerp
    private Vector3 startPosition;
    private Vector3 endPosition;

    [SerializeField] private float start_X, start_Y, end_X, end_Y;

    private float duration = 4f;
    private float elapsedTime;

    

    public void Start() {
        emitterPosition = new Vector2(transform.position.x, transform.position.y);
        startPosition = new Vector3(start_X, start_Y, transform.position.z);
        endPosition = new Vector3(end_X, end_Y, transform.position.z);
    }

    public void Update() {      
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;
        transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
        if(transform.position == endPosition){
            //flip destinations
            float temp;
            temp = start_X;
            start_X = end_X;
            end_X = temp; 

            temp = start_Y;
            start_Y = end_Y;
            end_Y = temp;

            startPosition = new Vector3(start_X, start_Y, transform.position.z);
            endPosition = new Vector3(end_X, end_Y, transform.position.z);

            elapsedTime = 0f;

        }
           
            
    }
}
