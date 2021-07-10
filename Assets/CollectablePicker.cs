using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePicker : MonoBehaviour
{
    private float fireflies=0;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag=="Collectable")
        {
            fireflies++;
            print(fireflies);
            Destroy(other.gameObject);
        }
    }
}
