using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCheckpoint : MonoBehaviour
{
    public ConditionalMovingPlatform platform;
    
    void OnTriggerEnter2D(Collider2D collision)
	{
		platform.Move();
	}
}
