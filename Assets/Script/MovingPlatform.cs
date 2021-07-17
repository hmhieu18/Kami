using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> points;
    public List<Transform> platforms;
    public bool moveX = true;
    public bool moveY = true;
    int goalPoint = 0;
    public float moveSpeed = 2;

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        foreach (Transform platform in platforms)
        {
            //change the position of the platform (move towards the goal point)
            if (moveX)
                points[goalPoint].position = new Vector2(points[goalPoint].position.x, platform.position.y);
            if (moveY)
                points[goalPoint].position = new Vector2(platform.position.x, points[goalPoint].position.y);
            platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, Time.deltaTime * moveSpeed);
            //Check if we are in very close proximity of the next point
            if (Vector2.Distance(platform.position, points[goalPoint].position) == 0)
            {
                //If so change goal point to the next one
                //Check if we reached the last point, reset to first point
                if (goalPoint == points.Count - 1)
                    goalPoint = 0;
                else
                    goalPoint++;
            }
        }

    }
}
