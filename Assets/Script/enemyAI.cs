using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    
    public float speed;
    //public bool isBoss = false;
    public List<Transform> points;
    public int nextId = 0;
    public int idChangeValue = 1;
    protected float val;

    
    public int idleTime = 2;

    
    void Start()
    {
        val = transform.localScale.x;
        
    }

    // Update is called once per frame
    void Update()
    {

        
        MoveToNext();
       

    }
    private void Reset()
    {
        init();
    }
    
    public void init()
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
