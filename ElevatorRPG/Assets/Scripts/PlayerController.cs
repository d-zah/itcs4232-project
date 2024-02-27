using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float playerMaxSpeed;
    [SerializeField] private float acceleration;
    private float playerMaxSpeedConst;
    private float targetVelocityX;
    private float targetVelocityY;
    private float currentVelocityX;
    private float currentVelocityY;
    private bool pauseMovement;

    public float inputHorizontal;
    public float inputVertical;
    public bool isWalkingRight;
    public bool isWalkingLeft;
    public bool isWalkingUp;
    public bool isWalkingDown;
    

    public VectorValue startPosition;
    public GameObject wipePanel;


    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    public bool isFacingRight = true;
    [SerializeField] private Animator anim;

    public void Awake() {
        if(wipePanel != null) {
            GameObject panel = Instantiate(wipePanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    void Start() {
        pauseMovement = false;
        transform.position = startPosition.initialValue;
        playerMaxSpeedConst = playerMaxSpeed;
    }

    void Update()
    {
        if(pauseMovement){
            playerMaxSpeed = 0;
        } else {
            playerMaxSpeed = playerMaxSpeedConst;
        }

        
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if ((Mathf.Abs(inputHorizontal) == 0.0f && Mathf.Abs(inputVertical) == 0.0f))
        {
            isWalkingRight = false;
            isWalkingLeft = false;
            isWalkingUp = false;
            isWalkingDown = false;
        }
        else if (inputHorizontal > 0.0f)
        {
            isWalkingRight = inputHorizontal > 0.0f;
        }
        else if (inputHorizontal < 0.0f)
        {
            isWalkingLeft = inputHorizontal < 0.0f;
        }
        else if (inputVertical > 0.0f)
        {
            isWalkingUp = inputVertical > 0.0f;
        }
        else if (inputVertical < 0.0f)
        {
            isWalkingDown = inputVertical < 0.0f;
        }

        Flip();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    public void PlayerMovement() 
    {
        
            float targetVelocityX = inputHorizontal * playerMaxSpeed;
            float targetVelocityY = inputVertical * playerMaxSpeed;

            // Apply acceleration/deceleration to movement
            targetVelocityX = inputHorizontal * playerMaxSpeed;
            float tx = acceleration * Time.deltaTime;
            currentVelocityX = Mathf.Lerp(currentVelocityX, targetVelocityX, tx);
            rb.velocity = new Vector2(currentVelocityX, rb.velocity.y);

            targetVelocityY = inputVertical * playerMaxSpeed;
            float ty = acceleration * Time.deltaTime;
            currentVelocityY = Mathf.Lerp(currentVelocityY, targetVelocityY, ty);
            rb.velocity = new Vector2(rb.velocity.x, currentVelocityY);
        
            
        }


    private void Flip()
    {
        if((isFacingRight && inputHorizontal < 0f) || (!isFacingRight && inputHorizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void playerCanMove(bool setting){
        pauseMovement = setting;
    }

}
