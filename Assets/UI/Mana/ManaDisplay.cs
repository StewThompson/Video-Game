using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ManaDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] manadiamonds;
    private int lastMana = 0;
    private bool halfStar = true;
    private bool shouldStartNew;
    void Start()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        MainCharacter mainCharacter = obj.GetComponent<MainCharacter>();
        mainCharacter.ManaEvent += MainCharacter_ManaEvent;
        
    }

    private void MainCharacter_ManaEvent(object sender, MainCharacter.ManaStatus e)
    {
        Debug.Log("mana = " + e.ManaCount);
        if(e.ManaCount < 0)
        {
            if (halfStar) { manadiamonds[lastMana].GetComponent<Image>().fillAmount = 0.5f; halfStar = false; shouldStartNew = false; }
            else { manadiamonds[lastMana].SetActive(false); lastMana++;halfStar = true; shouldStartNew = true; }
        }
        else
        {
            if (!shouldStartNew) { manadiamonds[lastMana].GetComponent<Image>().fillAmount = 1f; 
                halfStar = true; shouldStartNew = true; }
            else if (lastMana-1>=0) { lastMana--; (manadiamonds[lastMana]).SetActive(true); 
                (manadiamonds[lastMana]).GetComponent<Image>().fillAmount = 0.5f; 
                halfStar = true; shouldStartNew = false; }
        }
        
        
    }
        

    // Update is called once per frame
    
}
