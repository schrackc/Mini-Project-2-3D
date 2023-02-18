using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{
    public float speed;
    public Manager manager;

    private bool introMoveEnd = false;
    private float glideSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 150, 125);
    }

    // Update is called once per frame
    void Update()
    {
        //AI Fighting
        if (introMoveEnd)
        {

        }
        //AI Intro movement down
        else
        {
            introMovement();
            if (transform.position.y <= 0)
                introMoveEnd = true;
        }
    }

    private void introMovement()
    {
        Vector3 direction = new Vector3(0, -1, 0);
        Vector3.ClampMagnitude(direction, 2);
        direction = Vector3.Lerp(direction, Vector2.zero, 0.2f);
        transform.position += glideSpeed * Time.deltaTime * direction;
    }
}
