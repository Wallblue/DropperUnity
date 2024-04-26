using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GravityObject : MonoBehaviour
{
    public float ySize = 100.0f;
    public float forceGravity = 0.0f;
    private Quaternion originalRotation;
    
    void Start()
    {
        BoxCollider gravityBox = gameObject.AddComponent<BoxCollider>();
        gravityBox.isTrigger = true;
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Bounds bounds = meshFilter.mesh.bounds;

        gravityBox.size = new Vector3(bounds.size.x, ySize, bounds.size.z);
        gravityBox.center = bounds.center;
    }

    private void OnTriggerEnter(Collider other)
    {
        originalRotation = other.transform.rotation;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Oui");
        Rigidbody rb = other.GetComponent<Rigidbody>();

            Transform otherTransform = other.transform;
            Vector3 position = transform.position;
            
            /*Vector3 antiGravity = -Physics.gravity;
            rb.AddForce(antiGravity, ForceMode.Force);*/
            
            
            Vector3 directionObjet = (transform.position - other.transform.position).normalized;
            float distance = Vector3.Distance(position, otherTransform.position);
            Vector3 forceZone = directionObjet * (Physics.gravity.magnitude * forceGravity); 
            rb.AddForce(forceZone, ForceMode.Force);

            // Fais une rotation à 180°, je ne l'ai pas mis dans la version finale cela entraine quelques bugs 
            
            Quaternion targetRotation = Quaternion.FromToRotation(other.transform.up, -directionObjet) *
                                        other.transform.rotation;
            otherTransform.rotation = Quaternion.Slerp(otherTransform.rotation, targetRotation, 10 * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.rotation = originalRotation;
    }
}
