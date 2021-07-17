// This script handles inputs for the player. It serves two main purposes: 1) wrap up
// inputs so swapping between mobile and standalone is simpler and 2) keeping inputs
// from Update() in sync with FixedUpdate()

using UnityEngine;

//We first ensure this script runs before all other player scripts to prevent laggy
//inputs
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public bool testTouchControlsInEditor = false;  //Should touch controls be tested?
    public float verticalDPadThreshold = .5f;       //Threshold touch pad inputs
    public Thumbstick thumbstick;                   //Reference to Thumbstick
    public TouchButton jumpButton;                  //Reference to jump TouchButton
    public TouchButton attackButton;                  //Reference to jump TouchButton
    public TouchButton fireButton;                  //Reference to jump TouchButton

    [HideInInspector] public float horizontal;      //Float that stores horizontal input
    [HideInInspector] public bool jumpHeld;         //Bool that stores jump pressed
    [HideInInspector] public bool jumpPressed;      //Bool that stores jump held
    [HideInInspector] public bool crouchHeld;       //Bool that stores crouch pressed
    [HideInInspector] public bool crouchPressed;    //Bool that stores crouch held

    [HideInInspector] public bool attackHeld;       //Bool that stores crouch pressed
    [HideInInspector] public bool attackPressed;    //Bool that stores crouch held	
	[HideInInspector] public bool fireHeld;		//Bool that stores crouch pressed
    [HideInInspector] public bool firePressed;    //Bool that stores crouch held
    bool dPadCrouchPrev;                            //Previous values of touch Thumbstick
    bool readyToClear;                              //Bool used to keep input in sync


    void Update()
    {
        //Clear out existing input values
        ClearInput();

        //If the Game Manager says the game is over, exit
        if (GameManager.IsGameOver())
            return;

        //Process keyboard, mouse, gamepad (etc) inputs
        ProcessInputs();
        //Process mobile (touch) inputs
        ProcessTouchInputs();

        //Clamp the horizontal input to be between -1 and 1
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    }

    void FixedUpdate()
    {
        //In FixedUpdate() we set a flag that lets inputs to be cleared out during the 
        //next Update(). This ensures that all code gets to use the current inputs
        readyToClear = true;
    }

    public void ClearInput()
    {
        //If we're not ready to clear input, exit
        if (!readyToClear)
            return;

        //Reset all inputs
        horizontal = 0f;
        jumpPressed = false;
        jumpHeld = false;
        crouchPressed = false;
        crouchHeld = false;
		fireHeld=false;
		firePressed=false;
		attackPressed=false;
		attackHeld=false;

        readyToClear = false;
    }

    void ProcessInputs()
    {
        //Accumulate horizontal axis input
        horizontal += Input.GetAxis("Horizontal");

        //Accumulate button inputs
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        jumpHeld = jumpHeld || Input.GetButton("Jump");

        crouchPressed = crouchPressed || Input.GetButtonDown("Crouch");
        crouchHeld = crouchHeld || Input.GetButton("Crouch");

		attackPressed = attackPressed || Input.GetButtonDown("Attack");
        attackHeld = attackHeld || Input.GetButton("Attack");

		firePressed = firePressed || Input.GetButtonDown("Fire");
        fireHeld = fireHeld || Input.GetButton("Fire");
    }

    void ProcessTouchInputs()
    {
        //If this isn't a mobile platform AND we aren't testing in editor, exit
        if (!Application.isMobilePlatform && !testTouchControlsInEditor)
            return;

        //Record inputs from screen thumbstick
        Vector2 thumbstickInput = thumbstick.GetDirection();

        //Accumulate horizontal input
        horizontal += thumbstickInput.x;

        //Accumulate jump button input
        jumpPressed = jumpPressed || jumpButton.GetButtonDown();
        jumpHeld = jumpHeld || jumpButton.GetButton();

		attackPressed = attackPressed || attackButton.GetButtonDown();
        attackHeld = attackHeld || attackButton.GetButton();

		firePressed = firePressed || fireButton.GetButtonDown();
        fireHeld = fireHeld || fireButton.GetButton();

        //Using thumbstick, accumulate crouch input
        bool dPadCrouch = thumbstickInput.y <= -verticalDPadThreshold;
        crouchPressed = crouchPressed || (dPadCrouch && !dPadCrouchPrev);
        crouchHeld = crouchHeld || dPadCrouch;

        //Record whether or not playing is crouching this frame (used for determining
        //if button is pressed for first time or held
        dPadCrouchPrev = dPadCrouch;
    }
}
