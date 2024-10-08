using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawnScript : MonoBehaviour
{
    public GameObject Boulder;
    private GameObject FallingBoulder;
    public float MaxBoulderAliveTime;
    private float BoulderAliveTime;
    private void Start()
    {
        CreateBoulder();
    }
    private void Update()
    {
        BoulderLifetime();
    }
    private void CreateBoulder()
    {
        FallingBoulder = Instantiate(Boulder, transform);

    }
    private void BoulderLifetime()
    {
        BoulderAliveTime -= Time.deltaTime;
        if (BoulderAliveTime <= 0)
        {
            FallingBoulder.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            FallingBoulder.transform.position = transform.position;
            BoulderAliveTime = MaxBoulderAliveTime;
        }
    }


}
