using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLogic : MonoBehaviour
{
    private Animator anime;

    private bool SwordSelfDestruct = false;
    private float selfDestructTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    private void Update()
    {
        if(SwordSelfDestruct)
        {
            selfDestructTimer-=Time.deltaTime;
        }
        if(selfDestructTimer < 0) { Destroy(gameObject); }
    }
    public void Swing()
    {
        anime = GetComponent<Animator>();
        if (anime != null)
        {
            anime.SetBool("Swinging", false);
            transform.localPosition.Set(6.12f, -2.36f, 0f);
            transform.localRotation = new Quaternion(0f, 90, 0f, 0f);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Debug.LogError("Animator component not found.");
        }
        SwordSelfDestruct = true;
    }
}
    

