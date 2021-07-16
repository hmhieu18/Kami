using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boneMonster : MonoBehaviour
{
    Transform main;
    Vector2 targetPos;
    public float speed;
    float val;
    public List<Transform> points;
    Transform point;
    Transform startPoint;
    public boneMonster(Transform point)
    {
        this.point = point;
    }
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("Player").transform;
        targetPos = new Vector2(main.position.x, 0);
        val = transform.localScale.x;
        startPoint = transform;
    
    }

    // Update is called once per frame
    void Update()
    {
        chaseMain();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }
    private void chaseMain()
    {


        Transform dest = point;

        if (Vector2.Distance(transform.position, dest.position) < 0.2f)
        {
            dest = startPoint;
        }
        
        if (dest.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-val * 1, val, val);
        }
        else
            transform.localScale = new Vector3(val, val, val);

        transform.position = Vector2.MoveTowards(transform.position, dest.position, speed * Time.deltaTime);

    }
    
}
