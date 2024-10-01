using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public GameObject[] backgrounds; // An array of the 2 backgrounds
    public float speed = 0.1f; // Speed at which the background moves
    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in backgrounds)
        {
            loadChildObjects(obj, true); // For horizontal repeating
            loadChildObjects(obj, false); // For vertical repeating
        }
    }

    void loadChildObjects(GameObject obj, bool horizontal)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        float objectWidth = spriteRenderer.bounds.size.x;
        float objectHeight = spriteRenderer.bounds.size.y;
        Vector3 startPosition = obj.transform.position;
        int childsNeeded;

        if (horizontal)
        {
            childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        }
        else
        {
            childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / objectHeight);
            startPosition.x = 0; // Assuming you want to center it horizontally
        }

        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            if (horizontal)
            {
                c.transform.position = new Vector3(startPosition.x + (objectWidth * i), startPosition.y, startPosition.z);
            }
            else
            {
                c.transform.position = new Vector3(startPosition.x, startPosition.y + (objectHeight * i), startPosition.z);
            }
            c.name = obj.name + (horizontal ? "H" : "V") + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>()); // You might not want to destroy the original renderer if using the object for other purposes.
    }

    void LateUpdate()
    {
        foreach (GameObject obj in backgrounds)
        {
            repositionChildObjects(obj, true); // For horizontal repeating
            repositionChildObjects(obj, false); // For vertical repeating
        }
    }

    void repositionChildObjects(GameObject obj, bool horizontal)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>(true);
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            if (horizontal)
            {
                float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
                if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
                {
                    firstChild.transform.SetAsLastSibling();
                    firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
                }
                else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
                {
                    lastChild.transform.SetAsFirstSibling();
                    lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
                }
            }
            else
            {
                float halfObjectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y;
                if (transform.position.y + screenBounds.y > lastChild.transform.position.y + halfObjectHeight)
                {
                    firstChild.transform.SetAsLastSibling();
                    firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y + halfObjectHeight * 2, lastChild.transform.position.z);
                }
                else if (transform.position.y - screenBounds.y < firstChild.transform.position.y - halfObjectHeight)
                {
                    lastChild.transform.SetAsFirstSibling();
                    lastChild.transform.position = new Vector3(firstChild.transform.position.x, firstChild.transform.position.y - halfObjectHeight * 2, firstChild.transform.position.z);
                }
            }
        }
    }
}
