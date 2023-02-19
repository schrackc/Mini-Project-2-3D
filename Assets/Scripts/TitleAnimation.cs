using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    public GameObject asteroid1;
    private int numberOfActiveCollectibles = 0;
    private bool respawnCooldown = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(respawnRock());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (asteroid1.transform.position.z < -40)
            numberOfActiveCollectibles--;
        if(numberOfActiveCollectibles < 4)
            StartCoroutine(instantiateSmallRock());*/
        if(respawnCooldown != true)
        {
            StartCoroutine(respawnRock());
            respawnCooldown = true;
        }
            
    }

    IEnumerator instantiateSmallRock()
    {
        yield return new WaitForSeconds(Random.Range(1, 6));
        int x = Random.Range(-15, 15);
        int y = Random.Range(-8, 18);
        int z = 100;

        GameObject instance = Instantiate(asteroid1);
        instance.transform.position = new Vector3(x, y, z);
        Asteroid collectable = instance.GetComponent<Asteroid>();
        collectable.direction = new Vector3(Random.Range(0.5f, -0.5f), 0, -1);
        //collectable.manager = this;

        //numberOfActiveCollectibles++;
        Debug.Log(x + "," + y + "," + z);
    }

    IEnumerator respawnRock()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        respawnCooldown = false;
    }
}
