using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCannon : MonoBehaviour
{
    public int health = 5;
    private bool inCoolDown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Bullet")
        {
            health--;
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.25f);
        inCoolDown = false;
    }
}
