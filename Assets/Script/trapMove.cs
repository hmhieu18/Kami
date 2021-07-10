using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapMove : MonoBehaviour
{
    public List<Transform> destPoint;
    public float speed;
    int nextPoint = 0;
    Vector2 origin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveToNext();
    }
    void moveToNext()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            destPoint[nextPoint].position,
            Time.deltaTime * speed
            );
        if(Vector2.Distance(transform.position, destPoint[nextPoint].position) < 0.1f)
        {
            if (nextPoint == destPoint.Count - 1)
            {
                nextPoint = 0;
            }
            else
                nextPoint++;
        }
        
    }
}
