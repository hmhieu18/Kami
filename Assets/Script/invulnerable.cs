using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invulnerable : MonoBehaviour
{
    Color c;
    Renderer render;

    Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();

        c = render.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Traps"))
        {
            //Debug.Log("trigger");
            StartCoroutine("beInvulnerable");
            
        }

    }
    IEnumerator beInvulnerable()
    {
        Physics2D.IgnoreLayerCollision(11, 12, true);
        Physics2D.IgnoreLayerCollision(7, 12, true);
        c.a = 0.5f;
        render.material.color = c;
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(11, 12, false);
        Physics2D.IgnoreLayerCollision(7, 12, false);
        c.a = 1f;
        render.material.color = c;
    }
}
