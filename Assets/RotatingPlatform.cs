using UnityEngine;
using MoreMountains.CorgiEngine;


public class RotatingPlatform : MonoBehaviour
{
    public float RotationSpeed = 50f;
    private void Update()
    {
        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            collision.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            Transform characterTransform = collision.transform;
            
            Vector3 localOffset = transform.InverseTransformPoint(characterTransform.position);

            Vector3 newPosition = transform.TransformPoint(localOffset);
            characterTransform.position = newPosition;

            characterTransform.rotation = transform.rotation;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            collision.transform.SetParent(null);

            collision.transform.rotation = Quaternion.identity;
        }
    }
}