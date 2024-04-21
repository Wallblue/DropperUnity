using UnityEngine;

public class ChangeGravityZone : MonoBehaviour
{
    public float xGravityForce;
    public float yGravityForce;
    public float zGravityForce;

    
    private float _xPreviousGravityForce;
    private float _yPreviousGravityForce;
    private float _zPreviousGravityForce;
    private static bool _test;

    private void Start()
    {
        Vector3 currentGravity = Physics.gravity;
        _xPreviousGravityForce = currentGravity.x;
        _yPreviousGravityForce = currentGravity.y;
        _zPreviousGravityForce = currentGravity.z;
    }

    void OnTriggerStay(Collider other)
    {
        if(!_test)
        {
            Physics.gravity = new Vector3(
                xGravityForce,
                yGravityForce,
                zGravityForce
            );
            _test = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.gravity = new Vector3(
            _xPreviousGravityForce,
            _yPreviousGravityForce,
            _zPreviousGravityForce
        );
        _test = false;
    }
}
