using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
    // Start is called before the first frame update
    Transform main;
    Vector2 targetPos;
    public float speed;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("Player").transform;
        targetPos = new Vector2(main.position.x, main.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //moveToTarget(3);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (transform.position.x == targetPos.x && transform.position.y == targetPos.y)
            DestroyProjectile();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            DestroyProjectile();
    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    
}
