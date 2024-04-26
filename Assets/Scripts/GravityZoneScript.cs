using UnityEngine;

public class ChangeGravityZone : MonoBehaviour
{
    public float xGravityForce;
    public float yGravityForce;
    public float zGravityForce;

    public bool isZone; // By "zone" we mean the gravity should return to normal when the player exists the game object

    
    private float _xPreviousGravityForce;
    private float _yPreviousGravityForce;
    private float _zPreviousGravityForce;

    private static bool _changeApplied;

    /*
     * We use OnTriggerStay because with OnTriggerEnter, if 2 zones are clinging to each other, the gravity change would have a conflict with OnTriggerExit's gravity change.
     * We use _changeApplies to avoid changing gravity every frame.
     */
    void OnTriggerStay(Collider other)
    {
        if(!_changeApplied)
        {
            Vector3 currentGravity = Physics.gravity;
            _xPreviousGravityForce = currentGravity.x;
            _yPreviousGravityForce = currentGravity.y;
            _zPreviousGravityForce = currentGravity.z;  

            Physics.gravity = new Vector3(
                xGravityForce,
                yGravityForce,
                zGravityForce
            );
            _changeApplied = isZone;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(isZone)
        {
            Physics.gravity = new Vector3(
                _xPreviousGravityForce,
                _yPreviousGravityForce,
                _zPreviousGravityForce
            );
            _changeApplied = false;
        }
    }
}
