using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        other.rigidbody.gameObject.transform.parent = gameObject.transform;
    }

    private void OnCollisionExit(Collision other)
    {
        other.rigidbody.gameObject.transform.parent = null;
    }
}