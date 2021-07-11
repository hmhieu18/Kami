using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject explosionVFXPrefab;   //The visual effects for orb collection

    int playerLayer;                        //The layer the player game object is on

    public float moveSpeed = 10;
    private RectTransform UICoin;
    private bool isCollected = false;
    void Start()
    {
        //Get the integer representation of the "Player" layer
        playerLayer = LayerMask.NameToLayer("Player");

        //Register this orb with the game manager
        GameManager.RegisterCoin(this);
        UICoin = GameManager.getUICoin();
    }

    // private void Update()
    // {
    //     if (isCollected)
    //         FlyToDest();
    // }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the collided object isn't on the Player layer, exit. This is more 
        //efficient than string comparisons using Tags
        isCollected = true;
        if (collision.gameObject.layer != playerLayer)
            return;

        //The orb has been touched by the Player, so instantiate an explosion prefab
        //at this location and rotation
        GameObject effect = Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);

        //Tell audio manager to play orb collection audio
        AudioManager.PlayCoinCollectionAudio();

        //Tell the game manager that this orb was collected
        GameManager.PlayerGrabbedCoin(this);
        gameObject.SetActive(false);

        //Deactivate this orb to hide it and prevent further collection
    }
    // public void FlyToDest()
    // {
    //     transform.position = Vector2.MoveTowards(transform.position, UICoin.anchoredPosition, Time.deltaTime * moveSpeed);
    //     if (UICoin.anchoredPosition == transform.position)
    //     {
    //         gameObject.SetActive(false);
    //         Destroy(gameObject);
    //     }
    // }
}
