using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GravityObject : MonoBehaviour
{
    public float ySize = 100.0f;
    public float forceGravity = 1.0f;
    
    void Start()
    {
        BoxCollider gravityBox = gameObject.AddComponent<BoxCollider>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Bounds bounds = meshFilter.mesh.bounds;

        gravityBox.size = new Vector3(bounds.size.x, ySize, bounds.size.z);
        gravityBox.center = bounds.center;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Transform otherTransform = other.transform;
            
            Vector3 antiGravity = -Physics.gravity * rb.mass;
            rb.AddForce(antiGravity, ForceMode.Force);
            Vector3 directionObjet = (transform.position - other.transform.position).normalized;
            Vector3 forceZone = directionObjet * (Physics.gravity.magnitude * forceGravity * rb.mass); 
            rb.AddForce(forceZone, ForceMode.Force);

            Quaternion targetRotation = Quaternion.FromToRotation(other.transform.up, -directionObjet) *
                                        other.transform.rotation;
            otherTransform.rotation = Quaternion.Slerp(otherTransform.rotation, targetRotation, 10 * Time.deltaTime);
        }
    }
}
