using System;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public Rigidbody bodyRigidBody;
    public Transform bodyTransform;
    public Transform headTransform;
    public float speed = 5f;
    public float rotationSpeed = 360f;
    public float jumpForce = 7f;
    public float mouseXSensitivity = 10f;
    public float mouseYSensitivity = 10f;
    public float pitchMax = 80f;
    public float pitchMin = -80f;

    private float zMovement = 0.0f;
    private float xMovement = 0.0f;
    private float yawRotation = 0.0f;
    private float pitchRotation = 0.0f;
    private bool wantToJump = false;

    private bool firstFrame = true;
    
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
        firstFrame = true;
    }

    public void Update()
    {
        if (firstFrame)
        {
            firstFrame = false;
            return;
        }

        HomeMadeController();
    }

    private void HomeMadeController()
    {

        yawRotation = Input.GetAxis("Mouse X") * mouseXSensitivity;
        pitchRotation = Input.GetAxis("Mouse Y") * mouseYSensitivity;

        if (Input.GetKey(KeyCode.W))
        {
            zMovement += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            zMovement -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            xMovement -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            xMovement += 1;
        }

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
                wantToJump = true;
            }
        }

        var rotation = bodyTransform.rotation;
        var eulerRotation = rotation.eulerAngles;
        rotation = Quaternion.Euler(
            eulerRotation.x,
            eulerRotation.y + yawRotation,
            eulerRotation.z
        );
        bodyTransform.rotation = rotation;
        
        rotation = headTransform.rotation;
        eulerRotation = rotation.eulerAngles;

        var originalPitch = eulerRotation.x;
        var rawChangedPitch = originalPitch - pitchRotation;
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
            xMovement,
            0f,
            zMovement
        ).normalized;

        bodyTransform.position += rotation * movementIntent * (speed * Time.deltaTime);

        if (wantToJump)
        {
            bodyRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            wantToJump = false;
        }

        zMovement = 0.0f;
        xMovement = 0.0f;
        yawRotation = 0.0f;
    }
}