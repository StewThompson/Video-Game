using System.Collections;
using UnityEngine;

public class SetInactiveIfOffScreen : MonoBehaviour
{
    private Transform player;
    private float checkInterval = 5f; // Time in seconds between checks

    void Start()
    {
        var playerObj = GameObject.FindWithTag("Player").transform.parent;
        if (playerObj != null)
        {
            player = playerObj.transform.parent;
        }
        StartCoroutine(CheckOffScreenStatus());
    }

    IEnumerator CheckOffScreenStatus()
    {
        // Loop indefinitely
        while (true)
        {
            // Wait for specified interval
            yield return new WaitForSeconds(checkInterval);

            // Perform the off-screen check
            if (player != null)
            {
                bool isOffScreen = OffScreenY() || OffScreenX();
                gameObject.SetActive(!isOffScreen);
            }
        }
    }

    private bool OffScreenY()
    {
        // Ensure player is assigned to avoid NullReferenceException
        return player != null && Mathf.Abs(player.position.y - transform.position.y) >= 50;
    }

    private bool OffScreenX()
    {
        // Ensure player is assigned to avoid NullReferenceException
        return player != null && Mathf.Abs(player.position.x - transform.position.x) >= 50;
    }
}
