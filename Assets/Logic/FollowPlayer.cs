using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject main;
    public float offset = -6;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Mathf.Approximately(transform.position.x, main.transform.position.x) || !Mathf.Approximately(transform.position.y, main.transform.position.y))
        {
            transform.position = new Vector3(main.transform.position.x, main.transform.position.y, offset);
        }
    }
}
