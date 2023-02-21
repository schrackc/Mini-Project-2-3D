using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed;
    public float spinSpeed;
    public int pointValue = 5;
    public Manager manager;
    public ParticleSystem exp;

    public Vector3 direction;
    private Vector3 rotation;

    public enum EnemyType { small, medium, large, none };
    public EnemyType enemyType = EnemyType.none;
    // Start is called before the first frame update
    void Start()
    {
        //direction = new Vector3(0, 0, -1);
        rotation = new Vector3(spinSpeed, spinSpeed, spinSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t = speed * Time.deltaTime * direction;
        transform.position += speed * Time.deltaTime * direction;

        //Rotate Astroid
        transform.Rotate(rotation);

        if (transform.position.z <= -20)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Bullet")
        {
            manager.incrementScore(pointValue);
            switch (enemyType)
            {
                case EnemyType.none:
                    break;
                case EnemyType.small:
                    //manager.splitMediumRock(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    break;
                case EnemyType.medium:
                    manager.splitMediumRock(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    break;
                case EnemyType.large:
                    manager.splitLargeRock(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    break;
                default:
                    break;
            }
            StartCoroutine(destroy());
        }
    }
    private IEnumerator destroy()
    {
        exp = Instantiate(exp);
        exp.transform.position = transform.position;
        exp.Play();
        transform.position = new Vector3(0, -500, 500);
        yield return new WaitForSeconds(3f);
        //ParticleSystemStopAction particleSystemStopAction = ParticleSystemStopAction.Destroy;
      
        Destroy(exp);
        Destroy(this.gameObject);
    }
}
