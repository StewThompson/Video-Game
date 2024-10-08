using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private GameObject player; 
    private Vector3 BossScale;
    // Start is called before the first frame update
    void Start()
    {
        BossScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private float NetPlayerPos;
    public float tolerance;

    // Update is called once per frame
    void Update()
    {
        NetPlayerPos = transform.localPosition.x - player.transform.position.x;
        if (Mathf.Abs(NetPlayerPos) > tolerance) { transform.localScale = new Vector3(BossScale.x * (Mathf.Sign(NetPlayerPos)), BossScale.y, BossScale.z); }

    }
}
