using System.Collections;
using System.Collections.Generic;
//using UnityEngine.InputSystem;
using UnityEngine;

public class Tilter : MonoBehaviour
{
    public float speed = 0;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float smooth = 5.0f;
    float tiltAngle = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    */

    void Update()
    {
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);

    }
}
