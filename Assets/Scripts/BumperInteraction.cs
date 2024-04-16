using UnityEngine;

public class BumperInteracion : MonoBehaviour
{
    public Rigidbody bodyRigidBody;
    public float bumpForce = 200f;

    private void OnCollisionEnter(Collision other)
    {
        bodyRigidBody.AddForce(transform.rotation * Vector3.forward * bumpForce, ForceMode.Impulse);
    }
}