using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public Rigidbody bodyRigidBody;
    public Transform bodyTransform;
    public Transform headTransform;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float mouseXSensitivity = 10f;
    public float mouseYSensitivity = 10f;
    public float pitchMax = 80f;
    public float pitchMin = -80f;

    private float _zMovement;
    private float _xMovement;
    private float _yawRotation;
    private float _pitchRotation;
    private bool _wantToJump;
    private bool _wantToRun;

    private bool _firstFrame = true;
    
    private void Start()
    {
        LockCursor();
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            LockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        _firstFrame = true;
    }

    public void Update()
    {
        if (_firstFrame)
        {
            _firstFrame = false;
            return;
        }

        HomeMadeController();
    }

    private void HomeMadeController()
    {

        _yawRotation = Input.GetAxis("Mouse X") * mouseXSensitivity;
        _pitchRotation = Input.GetAxis("Mouse Y") * mouseYSensitivity;

        if (Input.GetKey(KeyCode.W))
        {
            _zMovement += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _zMovement -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _xMovement -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _xMovement += 1;
        }

        _wantToRun = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //bool sphereCast = Physics.SphereCast(bodyTransform.position + Vector3.up * 0.49f, 0.48f, Vector3.down, out var )
            if (Physics.SphereCast(bodyTransform.position + Vector3.up * 0.49f,
                    0.48f,
                    Vector3.down,
                    out var _,
                    0.1f
                ))
            {
                _wantToJump = true;
            }
        }

        var rotation = bodyTransform.rotation;
        var eulerRotation = rotation.eulerAngles;
        rotation = Quaternion.Euler(
            eulerRotation.x,
            eulerRotation.y + _yawRotation,
            eulerRotation.z
        );
        bodyTransform.rotation = rotation;
        
        rotation = headTransform.rotation;
        eulerRotation = rotation.eulerAngles;

        var originalPitch = eulerRotation.x;
        var rawChangedPitch = originalPitch - _pitchRotation;
        var firstStepPitch = rawChangedPitch % 360;

        if (firstStepPitch > 180)
        {
            firstStepPitch -= 360.0f;
        }

        var finalPitch = Mathf.Clamp(firstStepPitch, pitchMin, pitchMax);
        
        rotation = Quaternion.Euler(
            finalPitch,
            eulerRotation.y,
            eulerRotation.z
        );
        headTransform.rotation = rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(bodyTransform.position - bodyTransform.up * 0.05f, 0.48f);
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        var rotation = bodyTransform.rotation;

        var movementIntent = new Vector3(
            _xMovement,
            0f,
            _zMovement
        ).normalized;

        float speed = _wantToRun ? this.runSpeed : this.walkSpeed; 
        bodyTransform.position += rotation * movementIntent * (speed * Time.deltaTime);

        if (_wantToJump)
        {
            bodyRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _wantToJump = false;
        }

        _zMovement = 0.0f;
        _xMovement = 0.0f;
        _yawRotation = 0.0f;
    }
}