using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalMovingPlatform : MonoBehaviour
{
    Animator anim;			//Reference to the Animator component
    bool isMoved;
    int openParameterID;	//The ID of the animator parameter that opens the door
    public List<Transform> points;
    public Transform platform;
    int goalPoint = 0;
    public float moveSpeed = 2;

    private void Update()
    {
        if (isMoved)
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
    }

    void Start()
    {
        isMoved = false;
    }

    public void Move()
    {
        isMoved = true;
    }
}
