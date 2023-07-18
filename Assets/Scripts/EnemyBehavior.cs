using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private GameObject enemy;
    public float enemySpawnRate;
   //private float enemySpawning;
    public bool isSpawing = false;
    public float enemySpeed;
    //public Transform enemySpawnPoint;
    public Rigidbody2D rigidBody;
    public float timeToLive = 3f;
    public float positionNegativeX = -5.0f;
    public float positionPositiveX = 5.0f;
    [SerializeField]
    private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isSpawing)
        {
            StartCoroutine(enemySpawning());
        }
        Destroy(enemy, timeToLive);
    }

    IEnumerator enemySpawning()
    {
        isSpawing= true;
        enemySpawn();
        yield return new WaitForSeconds(enemySpawnRate);
        isSpawing= false;
    }

    void enemySpawn()
    {
        float randomRange = Random.Range(positionNegativeX, positionPositiveX);
        //Vector2 (x,y) = randomRange between -10 to 10
        Vector2 randomPosition = new Vector2(randomRange, -1);

        //enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
       // rb.velocity = (-transform.up) * enemySpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        Debug.Log("Collision test");
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            Debug.Log("Enemy destroyed!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
            }
            Debug.Log("Collided with " + collision.gameObject.name);
        }
    }

    //void randomSpawn()
    //{
    //    float randomRange = Random.Range(-10.0f, 10.0f);
    //    //Vector2 (x,y) = randomRange between -10 to 10
    //    Vector2 randomPosition = new Vector2(randomRange, randomRange);
    //}
}
