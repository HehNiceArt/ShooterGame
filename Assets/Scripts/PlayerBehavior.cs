using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;
    private float defaultMoveSpeed = 4;
    public Rigidbody2D rigidBody;
    private Vector2 moveInput;
    public Animator anim;
    //private SpriteRenderer spriteRenderer;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    
    public float fireRate;

    private GameObject bullet;

    [SerializeField]
    private GameObject bulletPrefab;

    private bool canShoot = false;

    public float timeToLive = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        anim.SetFloat("Horizontal", moveInput.x);
        anim.SetFloat("Vertical", moveInput.y);
        anim.SetFloat("Speed", moveInput.sqrMagnitude);
        //Debug.Log("Test");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Reduce speed");
            moveSpeed -= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (moveSpeed < defaultMoveSpeed)
            {
                Debug.Log("Return default move speed");
                moveSpeed = defaultMoveSpeed;
            }
        }
        

        if (Input.GetButton("Fire1") &&(!canShoot))
        {   
            StartCoroutine(FireShot());        
        }

        Destroy(bullet, timeToLive);
       
    }


    IEnumerator FireShot()
    {
        canShoot = true;
        Fire();
        yield return new WaitForSeconds(fireRate);
        canShoot = false;
    }



    void Fire()
    {
        bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    

    private void FixedUpdate()
    {
        rigidBody.velocity = moveInput * moveSpeed;
    }

    private void OnMove(InputValue inputValue)
    {
        //if press WASD =  to Vector 2 Values
        moveInput = inputValue.Get<Vector2>();
    }
}
