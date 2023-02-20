using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlaster : MonoBehaviour
{
    public int health = 5;
    private bool inCoolDown = false;
    public GameObject bullet;
    public GameObject shootPoint;
    public int coolTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitToDrop(coolTime));
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if(gameObject.tag == "Bullet")
        {
            health--;
        }

        if (!inCoolDown)
        {
            inCoolDown = true;
            GameObject go = Instantiate(bullet);
            go.transform.position = shootPoint.transform.position;
            BulletMove b = go.GetComponent<BulletMove>();
            b.speed = 2f;
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.25f);
        inCoolDown = false;
    }

    IEnumerator waitToDrop(int downTime)
    {
        yield return new WaitForSeconds(downTime);
        StartCoroutine(CoolDown());
    }
}
