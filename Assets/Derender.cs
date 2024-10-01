using UnityEngine;

public class OffscreenCulling : MonoBehaviour
{
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (objectRenderer.isVisible)
        {
            // Object is visible
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            // Object is not visible
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
