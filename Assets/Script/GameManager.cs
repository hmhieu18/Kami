using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 	//This class holds a static reference to itself to ensure that there will only be
	//one in existence. This is often referred to as a "singleton" design pattern. Other
	static GameManager current;

	public RectTransform UICoin;
	public float deathSequenceDuration = 1.5f;	//How long player death takes before restarting
	// public Transform UICoin;

	List<Coin> coins;								//The collection of scene coins
	Door lockedDoor;							//The scene door
	SceneFader sceneFader;						//The scene fader

	int numberOfDeaths;							//Number of times player has died
	int numberOfCollectedCoins=0;
	int numberRemainingLives;							//Number of times player has died
	float totalGameTime;						//Length of the total game time
	bool isGameOver;							//Is the game currently over?


	void Awake()
	{
		//If a Game Manager exists and this isn't it...
		if (current != null && current != this)
		{
			//...destroy this and exit. There can only be one Game Manager
			Destroy(gameObject);
			return;
		}

		//Set this as the current game manager
		current = this;

		//Create out collection to hold the coins
		coins = new List<Coin>();
		numberOfCollectedCoins=0;
		UIManager.UpdateCoinUI(numberOfCollectedCoins);

		//Persis this object between scene reloads
		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		//If the game is over, exit
		if (isGameOver)
			return;

		//Update the total game time and tell the UI Manager to update
		totalGameTime += Time.deltaTime;
		UIManager.UpdateTimeUI(totalGameTime);
	}

	public static bool IsGameOver()
	{
		//If there is no current Game Manager, return false
		if (current == null)
			return false;

		//Return the state of the game
		return current.isGameOver;
	}

	public static void RegisterSceneFader(SceneFader fader)
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//Record the scene fader reference
		current.sceneFader = fader;
	}

	public static void RegisterDoor(Door door)
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//Record the door reference
		current.lockedDoor = door;
	}

	public static void RegisterCoin(Coin coin)
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//If the coin collection doesn't already contain this coin, add it
		if (!current.coins.Contains(coin))
			current.coins.Add(coin);

		//Tell the UIManager to update the coin text
		// UIManager.UpdateCoinUI(current.coins.Count);
	}

	public static void PlayerGrabbedCoin(Coin coin)
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

        // coin.FlyToDest(current.UICoin);
		// coin.FlyToDest(UICoin);
		current.numberOfCollectedCoins++;
		//If the coins collection doesn't have this coin, exit
		if (!current.coins.Contains(coin))
			return;

		//Remove the collected coin
		current.coins.Remove(coin);

		//If there are no more coins, tell the door to open
		if (current.coins.Count == 0)
			current.lockedDoor.Open();
		
		//Tell the UIManager to update the coin text
		UIManager.UpdateCoinUI(current.numberOfCollectedCoins);
	}

	public static void PlayerDied()
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//Increment the number of player deaths and tell the UIManager
		current.numberOfDeaths++;
		// current.numberOfLivesLeft--
		//If we have a scene fader, tell it to fade the scene out
		if(current.sceneFader != null)
			current.sceneFader.FadeSceneOut();
		current.numberOfCollectedCoins=0;
        UIManager.UpdateCoinUI(current.numberOfCollectedCoins);
		//Invoke the RestartScene() method after a delay
		current.Invoke("RestartScene", current.deathSequenceDuration);
	}
	public static RectTransform getUICoin()
	{
		return current.UICoin;
	}

	public static void PlayerWon()
	{
		Debug.Log("PLAYER WON");
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//The game is now over
		current.isGameOver = true;

		//Tell UI Manager to show the game over text and tell the Audio Manager to play
		//game over audio
		UIManager.DisplayGameOverText();
		AudioManager.PlayWonAudio();
	}

	void RestartScene()
	{
		//Clear the current list of coins
		coins.Clear();

		numberOfCollectedCoins=0;

		//Play the scene restart audio
		AudioManager.PlaySceneRestartAudio();

		//Reload the current scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}
}
