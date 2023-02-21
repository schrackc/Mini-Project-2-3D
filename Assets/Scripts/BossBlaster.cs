using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlaster : MonoBehaviour
{
    public int health = 5;
    private bool inCoolDown = false;
    public GameObject bullet;
    public GameObject shootPoint;
    public ParticleSystem smallExplosion;
    public float coolTime = 5;
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
            smallExplosion = Instantiate(smallExplosion);
            smallExplosion.transform.position = transform.position;
            smallExplosion.Play();
            Debug.Log("Part Destroyed");
            Destroy(this.gameObject);
        }

        if (!inCoolDown)
        {
            inCoolDown = true;
            GameObject go = Instantiate(bullet);
            go.transform.position = shootPoint.transform.position;
            BossBulletMove b = go.GetComponent<BossBulletMove>();
            b.speed = 8f;
            //b.direction = new Vector3(0, 0, -1);
            StartCoroutine(CoolDown(coolTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if(gameObject.tag == "Bullet")
        {
            health--;
        }

        
    }

    IEnumerator CoolDown(float downTime)
    {
        yield return new WaitForSeconds(downTime);
        inCoolDown = false;
    }

    IEnumerator waitToDrop(float downTime)
    {
        yield return new WaitForSeconds(10);
        Debug.Log("Play Time's Over!");
        StartCoroutine(CoolDown(downTime));
    }
}
