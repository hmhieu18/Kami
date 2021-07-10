using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loseHealth : MonoBehaviour
{
    public Image healthBar;
    public float current;
    float health = 100;
    float loss;
   
    // Start is called before the first frame update
    void Start()
    {
        loss = FindObjectOfType<movement>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            reduceBar();
    }

    void reduceBar()
    {
        if(health <= 0)
        {
            return;
        }

        health -= loss;
        healthBar.fillAmount = health / 100;

    }
}
