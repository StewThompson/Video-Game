using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player; 
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TeleportPlayer()
    {
        player.transform.parent.position = new Vector2(0, 1);
    }
}
