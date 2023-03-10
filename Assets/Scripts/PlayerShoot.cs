using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootPoint;

    private bool inCoolDown = false;

    private Vector3 screenPosition;
    private Vector3 worldPosition;

    private AudioSource PewPew;

    // Start is called before the first frame update
    void Start()
    {
        PewPew = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 1;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Input.GetKey(KeyCode.Space) && !inCoolDown)
        {
            if (!PewPew.isPlaying)
                PewPew.PlayOneShot(PewPew.clip, 0.6f);
            inCoolDown = true;
            GameObject go = Instantiate(bullet);
            go.transform.position = shootPoint.transform.position;
            go.transform.rotation = Quaternion.Euler(0,0,0);
            BulletMove b = go.GetComponent<BulletMove>();
            b.speed = 2f;
            //b.direction = new Vector3(worldPosition.x/10, worldPosition.y/200, 1);
            Debug.Log(b.direction);
            StartCoroutine(CoolDown());
        }

        
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.25f);
        inCoolDown = false;
    }
}
