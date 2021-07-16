using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundon;
    [SerializeField] Image soundoff;
    private bool muted = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause=muted;
    }

    public void OnButtonPress()
    {
        if(muted == false)
        {
            muted = true;
            AudioListener.pause = true;
            Debug.Log("1");
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
            Debug.Log("2");
        }
        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon(){
        if(muted == false)
        {
            soundon.enabled = true;
            soundoff.enabled = false;
        }
        else
        {
            soundon.enabled = false;
            soundoff.enabled = true;
        }
    }
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted?1:0);
    }
}
