using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowDamageObject : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<LifeCount>().LoseLife();
            if (FindObjectOfType<Crow>() != null)
                FindObjectOfType<Crow>().bited();
            if (FindObjectOfType<CrowOfBoss>() != null)
                FindObjectOfType<CrowOfBoss>().bited();
        }
    }
}
