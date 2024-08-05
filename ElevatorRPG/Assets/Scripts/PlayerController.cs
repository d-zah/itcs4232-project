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
    [SerializeField] private Animator anim;
    private Animator animator;

    public float inputHorizontal;
    public float inputVertical;
    

    public VectorValue startPosition;


    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] PauseMenu pauseMenu;


    public void Awake() {
        anim.SetBool("isSceneActive", true);
    }

    void Start() {
        pauseMovement = false;
        transform.position = startPosition.initialValue;
        animator = GetComponent<Animator>();
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

        // if not moving
        if (!(Mathf.Abs(inputHorizontal) == 0.0f && Mathf.Abs(inputVertical) == 0.0f))
        {
            animator.SetFloat("moveX", inputHorizontal);
            animator.SetFloat("moveY", inputVertical);
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }

        checkForPause();
    }

    void checkForPause(){
        if(!Input.GetKeyDown(KeyCode.Escape)) return;

        pauseMenu.pause();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    public void PlayerMovement() 
    {
        
            float targetVelocityX = inputHorizontal;
            float targetVelocityY = inputVertical;

            // Apply acceleration/deceleration to movement
            targetVelocityX = inputHorizontal;
            float tx = acceleration * Time.deltaTime;
            currentVelocityX = Mathf.Lerp(currentVelocityX, targetVelocityX, tx);
            //rb.velocity = new Vector2(currentVelocityX, rb.velocity.y);

            targetVelocityY = inputVertical;
            float ty = acceleration * Time.deltaTime;
            currentVelocityY = Mathf.Lerp(currentVelocityY, targetVelocityY, ty);

            //assign velocity
            rb.velocity = new Vector2(currentVelocityX, currentVelocityY);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, 1.0f) * playerMaxSpeed;
        
            
        }

    public void playerCanMove(bool setting){
        pauseMovement = setting;
    }

    public void makePlayerFall(){
        animator.SetBool("falling", true);
    }

    public int getPlayerLevel(){
        return SceneManager.GetActiveScene().buildIndex;
    }

    public int getElevatorProgress(){
        return startPosition.elevatorProgress;
    }

    public void SavePlayer(){
        SaveSystem.SavePlayer(this);
    }

}
