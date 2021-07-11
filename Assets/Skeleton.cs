using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public GameObject skeleton;
    Animator anim;			//Reference to the Animator component
    bool isRised;
    public bool isDead;
    int openParameterID;	//The ID of the animator parameter that opens the door
    public List<Transform> points;
    public Transform platform;
    int goalPoint = 0;
    public float moveSpeed = 2;
    public bool isFacingLeft = false;

    private void Update()
    {
        if (isRised && skeleton != null)
            if (!skeleton.GetComponent<Enemy>().isDead())
                MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        //change the position of the platform (move towards the goal point)
        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, Time.deltaTime * moveSpeed);
        //Check if we are in very close proximity of the next point
        if (Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f)
        {
            //If so change goal point to the next one
            //Check if we reached the last point, reset to first point
            if (goalPoint == points.Count - 1)
                goalPoint = 0;
            else
                goalPoint++;
        }

        if (platform.position.x < points[goalPoint].position.x && !isFacingLeft)
        {
            Flip();
        }
        else if (platform.position.x > points[goalPoint].position.x && isFacingLeft)
        {
            Flip();
        }
    }

    void Start()
    {
        //Get a reference to the Animator component
        anim = skeleton.GetComponent<Animator>();

        isRised = false;
        //Get the integer hash of the "Open" parameter. This is much more efficient
        //than passing strings into the animator
        openParameterID = Animator.StringToHash("rise");

        //Register this door with the Game Manager
        // GameManager.RegisterDoor(this);
    }

    public void Rise()
    {
        if (skeleton != null)
            if (!isRised || !skeleton.GetComponent<Enemy>().isDead())
            //Play the animation that opens the door
            {
                isRised = true;
                isDead = false;
                anim.SetTrigger(openParameterID);
            }
        // AudioManager.PlayDoorOpenAudio();
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingLeft = !isFacingLeft;

        // Multiply the player's x local scale by -1.
        Vector3 theScale =  platform.localScale;
        theScale.x *= -1;
        platform.localScale = theScale;
    }
}
