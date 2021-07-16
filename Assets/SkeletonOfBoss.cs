using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonOfBoss : MonoBehaviour
{

    public GameObject skeleton;
    Animator anim;			//Reference to the Animator component
    public bool isDead;
    int openParameterID;	//The ID of the animator parameter that opens the door
    public List<Transform> points;
    public Transform platform;
    int goalPoint = 0;
    public float moveSpeed = 2;
    public bool isFacingLeft = false;
    void Start()
    {
        anim = skeleton.GetComponent<Animator>();

        openParameterID = Animator.StringToHash("rise");

        anim.SetTrigger(openParameterID);

    }
    private void Update()
    {
        if (skeleton != null)
            if (!skeleton.GetComponent<Enemy>().isDead())
                MoveToNextPoint();
        
        if (FindObjectOfType<projectile>().isDead())
        {
            GetComponent<Enemy>().Die();
        }
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
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingLeft = !isFacingLeft;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = platform.localScale;
        theScale.x *= -1;
        platform.localScale = theScale;
    }
    public void SetPoints(List<Transform> _points)
    {
        points = _points;
        goalPoint = 0;
    }
}
