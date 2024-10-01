using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Image HealthBar;
    public GameObject[] hearts;
    private int lastHeart = 0;
    private bool halfHeart = true;
    private bool shouldStartNew;


    private void Start()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        MainCharacter mainCharacter = obj.GetComponent<MainCharacter>();
        mainCharacter.HealthEvent += MainCharacter_HealthEvent;
        lastHeart = hearts.Length - 1;
    }

    private void MainCharacter_HealthEvent(object sender, MainCharacter.HealthStatus e)
    {
        HealthBar.fillAmount = (float)e.CurrentHealth/e.TotalHealth;


        /*if (e.HealthCount < 0)
        {
            for(int i = 0; i > e.HealthCount;i--) { 
             if (halfHeart) { hearts[lastHeart].GetComponent<Image>().fillAmount = 0.5f; halfHeart = false; shouldStartNew = false; }
             else { hearts[lastHeart].SetActive(false); lastHeart--; halfHeart = true; shouldStartNew = true; }
            }
           
        }
        else
        {
            for (int i = 0; i < e.HealthCount; i++)
            {
                if (!shouldStartNew)
                {
                    hearts[lastHeart].GetComponent<Image>().fillAmount = 1f;
                    halfHeart = true; shouldStartNew = true;
                }
                else if (lastHeart + 1 < hearts.Length)
                {
                    lastHeart++; (hearts[lastHeart]).SetActive(true);
                    (hearts[lastHeart]).GetComponent<Image>().fillAmount = 0.5f;
                    halfHeart = true; shouldStartNew = false;
                }
            }
        }
        */

    }
}
