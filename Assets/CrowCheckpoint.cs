using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowCheckpoint : MonoBehaviour
{
    public Crow crow;
    void OnTriggerEnter2D(Collider2D collision)
	{
		crow.mainReached();
	}
}
