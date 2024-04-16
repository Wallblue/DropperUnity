using System;
using UnityEngine;

public enum CharacterControllerType
{
    DoomLike,
    DukeNukem3DLike, // 2nd Generation
    QuakeLike,
}


public class Movement : MonoBehaviour
{
    public Rigidbody bodyRigidBody;
    public Transform bodyTransform;
    public Transform headTransform;
    public float speed = 5f;
    public float rotationSpeed = 360f;
    public float jumpForce = 20f;
    public float mouseXSensitivity = 10f;
    public float mouseYSensitivity = 10f;
    public float pitchMax = 80f;
    public float pitchMin = -80f;
    public CharacterControllerType characterControllerType;

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
        
        switch (characterControllerType)
        {
            case CharacterControllerType.DoomLike:
                DoomLikeController();
                break;
            case CharacterControllerType.DukeNukem3DLike:
                DukeNukem3DLikeController();
                break;
            case CharacterControllerType.QuakeLike:
                QuakeLikeController();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DoomLikeController()
    {
        var mouseXDelta = Input.GetAxis("Mouse X");

        if (Input.GetKey(KeyCode.UpArrow))
        {
            zMovement += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            zMovement -= 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            yawRotation -= rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            yawRotation += rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.SphereCast(bodyTransform.position + Vector3.up * 0.49f,
                    0.48f,
                    Vector3.down,
                    out var _osef,
                    0.05f
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
    }

    private void DukeNukem3DLikeController()
    {
        var mouseXDelta = Input.GetAxis("Mouse X");

        if (Input.GetKey(KeyCode.W))
        {
            zMovement += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            zMovement -= 1;
        }

        yawRotation = mouseXDelta * mouseXSensitivity;

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
            if (Physics.SphereCast(bodyTransform.position + Vector3.up * 0.49f,
                    0.48f,
                    Vector3.down,
                    out var _osef,
                    0.05f
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
    }

    private void QuakeLikeController()
    {
        var mouseXDelta = Input.GetAxis("Mouse X");
        var mouseYDelta = Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.W))
        {
            zMovement += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            zMovement -= 1;
        }

        yawRotation = mouseXDelta * mouseXSensitivity;
        pitchRotation = mouseYDelta * mouseYSensitivity;

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
            if (Physics.SphereCast(bodyTransform.position + Vector3.up * 0.49f,
                    0.48f,
                    Vector3.down,
                    out var _osef,
                    0.05f
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
    }
}