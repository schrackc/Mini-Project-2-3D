using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSplit : MonoBehaviour
{
    public Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Bullet")
        {
            manager.instantiateMediumRock(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            Destroy(this.gameObject);
        }
        if (gameObject.tag == "Player")
        {
            //Need Player Damage with Player Health
            Destroy(this.gameObject);
        }
    }
}
