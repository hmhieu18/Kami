using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	Animator anim;			//Reference to the Animator component
	int openParameterID;	//The ID of the animator parameter that opens the door


	void Start()
	{
		//Get a reference to the Animator component
		anim = GetComponent<Animator>();

		//Get the integer hash of the "Open" parameter. This is much more efficient
		//than passing strings into the animator
		openParameterID = Animator.StringToHash("Open");

		//Register this door with the Game Manager
		GameManager.RegisterDoor(this);
	}

	public void Open()
	{
		//Play the animation that opens the door
		anim.SetTrigger(openParameterID);
		AudioManager.PlayDoorOpenAudio();
	}
}
