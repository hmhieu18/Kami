using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    
    public float speed;
    
    public List<Transform> points;
    public int nextId = 0;
    int idChangeValue = 1;
    private float val;
    

    void Start()
    {
        val = transform.localScale.x;
        
    }

    // Update is called once per frame
    void Update()
    {

        //float distance = Vector2.Distance(main.position, transform.position);
        MoveToNext();
        
        /*
        if (distance <= rangeSurveil)
        {
            chaseMain();
        }
        else
        {
            stopChase();
        }*/

    }
    private void Reset()
    {
        init();
    }
    
    void init()
    {
        
        GameObject root = new GameObject(name + "Root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);

        GameObject boundPoints = new GameObject("BoundaryPoints");
        boundPoints.transform.SetParent(root.transform);
        boundPoints.transform.position = root.transform.position;

        GameObject point1 = new GameObject("Bound1");
        point1.transform.SetParent(boundPoints.transform);
        point1.transform.position = root.transform.position;


        GameObject point2 = new GameObject("Bound2");
        point2.transform.SetParent(boundPoints.transform);
        point2.transform.position = root.transform.position;

        points = new List<Transform>();
        points.Add(point1.transform);
        points.Add(point2.transform);
    }
    /*
    private void chaseMain()
    {
        if (transform.position.x < main.position.x)
        {
            rg.velocity = new Vector2(speed, rg.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x > main.position.x)
        {
            rg.velocity = new Vector2(-speed, rg.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
    }

    private void stopChase()
    {
        rg.velocity = new Vector2(0, 0);
    }

    */
    private void MoveToNext()
    {
        Transform nextPoint = points[nextId];
        
        if (nextPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-val*1, val,val);
        }
        else
            transform.localScale = new Vector3(val, val,val);

        transform.position = Vector2.MoveTowards(transform.position, nextPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextPoint.position) < 0.2f)
        {
            if (nextId == points.Count - 1)
            {
                idChangeValue = -1;
            }
            if (nextId == 0)
                idChangeValue = 1;
            nextId += idChangeValue;
        }
    }
}
