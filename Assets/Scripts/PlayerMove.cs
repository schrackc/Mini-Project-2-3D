using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float lerpConstant;
    public Manager manager;

    private Vector3 direction;
    private Quaternion newRotate;




    void Start()
    {
        direction = Vector3.zero;
        
    }

    private Vector3 GetInput()
    {
        Vector3 dir = Vector3.zero;
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (transform.position.y < 15))
        {
            //dir.z = -1;
            dir += transform.forward;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (transform.position.y > -10))
        {
            //dir.z = 1;
            dir -= transform.forward;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (transform.position.x < 30))
        {
            //dir.x = -1;
            dir += transform.right;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (transform.position.x > -30))
        {
            //dir.x = 1;
            dir -= transform.right;
        }
        
        return dir.normalized;
    }

    void Update()
    {
        //player controled movement
        if (!manager.bossDead)
        {
            Vector3 inputVelocity = GetInput();
            direction += inputVelocity;
            Vector3.ClampMagnitude(direction, 2);
            direction = Vector3.Lerp(direction, Vector2.zero, lerpConstant);
            transform.position += speed * Time.deltaTime * direction;
        }
        //game end movement
        else
        {
            Vector3 inputVelocity = new Vector3(0, 1, 1);
            inputVelocity = inputVelocity.normalized;
            direction += inputVelocity;
            Vector3.ClampMagnitude(direction, 2);
            direction = Vector3.Lerp(direction, Vector2.zero, lerpConstant);
            transform.position += speed * Time.deltaTime * direction;
            if (transform.rotation.x <= -135) {
                newRotate = Quaternion.Euler(-135, 0, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotate, speed * lerpConstant);
            }
        }
    }
}
