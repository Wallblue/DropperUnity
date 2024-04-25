using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GrappinHarponScript : MonoBehaviour
{
    public float xdistance;
    public float ydistance;
    public float zdistance;
    
    private float _zMovement;
    private float _yMovement;
    private float _xMovement;
    
    public Transform bodyTransform;
    public Rigidbody bodyRigidBody;
    public Rigidbody bodyRigidTarget;
    
    private int boucle;
    private bool grappinON;

    private void FixedUpdate()
    {
        while (grappinON)
        {
            xdistance = bodyRigidBody.transform.position.x - bodyRigidTarget.transform.position.x;
            ydistance = bodyRigidBody.transform.position.y - bodyRigidTarget.transform.position.y;
            zdistance = bodyRigidBody.transform.position.z - bodyRigidTarget.transform.position.z;
            
            boucle = 10;
            
            //while (boucle != 0)
            //while (xdistance > -10 && xdistance < 10 && ydistance > -10 && ydistance < 10 && zdistance > -10 && zdistance < 10) 
            if (true)
            {
                boucle -= 1;
                if (xdistance < -1 || xdistance > 1) 
                {
                    if (xdistance < 1) 
                    { 
                        _xMovement += 1;
                    }

                    if (xdistance > -1)
                    {
                        _xMovement -= 1;
                    }
                } 
                if (ydistance < -1 || ydistance > 1) 
                { 
                    if (ydistance < 1) 
                    { 
                        _yMovement += 1;
                    }

                    if (ydistance > -1)
                    { 
                        _yMovement -= 1;
                    }
                } 
                if (zdistance < -1 || zdistance > 1) 
                { 
                    if (zdistance < 1) 
                    {
                        _zMovement += 1;
                    }

                    if (zdistance > -1)
                    { 
                        _zMovement -= 1; 
                    }
                } 
                var movementIntent = new Vector3(
                    _xMovement, 
                    _yMovement, 
                    _zMovement
                    ).normalized;
                bodyTransform.position += movementIntent * (50 * Time.deltaTime);
                _zMovement = 0.0f;
                _yMovement = 0.0f;
                _xMovement = 0.0f;
                
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("false");
                    grappinON = false;
                }
            }
            
            Debug.Log(xdistance + " " + ydistance + " " + zdistance);
            Debug.Log(_xMovement + " " + _yMovement + " " + _zMovement);
        }
    }

    public void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Click over GameObject.");
            
            grappinON = true;
        }
    }
}
