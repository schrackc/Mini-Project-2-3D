using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCannon : MonoBehaviour
{
    public int health = 5;
    private bool inCoolDown = false;
    public GameObject rock1;
    //public GameObject rock2;
    public GameObject shootPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitToDrop());
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (!inCoolDown)
        {
            inCoolDown = true;
            GameObject go = Instantiate(rock1);
            go.transform.position = shootPoint.transform.position;
            Asteroid rock = go.GetComponent<Asteroid>();
            rock.direction = new Vector3(0, -0.5f, -1);
            StartCoroutine(CoolDown());
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
        yield return new WaitForSeconds(5f);
        inCoolDown = false;
    }

    IEnumerator waitToDrop()
    {
        yield return new WaitForSeconds(20f);
        StartCoroutine(CoolDown());
    }
}
