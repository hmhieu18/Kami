using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Skeleton skeleton;
    void OnTriggerEnter2D(Collider2D collision)
	{
		skeleton.Rise();
	}
}
