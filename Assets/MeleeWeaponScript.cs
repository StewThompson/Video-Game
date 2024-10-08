using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MeleeWeaponScript : MonoBehaviour
{
    public float finalAngle;
    public float swingSpeed;
    public bool RandomUpSwingSpeed;
    public float UpSwingSpeed;
    public float UpSwingSpeedLower;
    public float UpSwingSpeedHigher;

    private void Start()
    {
        CanAttack = true;
    }
    private float direction;
    private void Update()
    {
        direction=Mathf.Sign(transform.parent.parent.localScale.x);
    }

    public void StartSwing()
    {
        StartCoroutine(Swing());
    }
    public bool CanAttack { get; private set; }
    public bool IsSwingingDown { get; private set; }
    private IEnumerator Swing()
    {
        CanAttack = false;
        IsSwingingDown = true;
        float timeElapsed = 0f;
        float startAngle = transform.eulerAngles.z;
        //float targetAngle = direction > 0 ? finalAngle : -finalAngle;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * swingSpeed;
            float angle = Mathf.Lerp(startAngle, finalAngle, timeElapsed);
            transform.rotation = Quaternion.Euler(0, 0, angle* direction);
            yield return null;
        }

        // Ensure the final angle is set
        transform.rotation = Quaternion.Euler(0, 0, finalAngle*direction);// * direction);

        // Start the upswing after the swing completes
        IsSwingingDown = false; 
        StartCoroutine(UpSwing());
    }

    private IEnumerator UpSwing()
    {
        float actualUpSwingSpeed = UpSwingSpeed;
        if (RandomUpSwingSpeed)
        {
            actualUpSwingSpeed = Random.Range(UpSwingSpeedLower, UpSwingSpeedHigher);
        }

        float timeElapsed = 0f;
        float startAngle = transform.eulerAngles.z;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * actualUpSwingSpeed;
            float angle = Mathf.Lerp(startAngle, 0, timeElapsed);
            transform.rotation = Quaternion.Euler(0, 0, angle * direction);
            yield return null;
        }

        // Ensure the final rotation is exactly set to 0
        transform.rotation = Quaternion.Euler(0, 0, 0);
        CanAttack = true;
    }
}
