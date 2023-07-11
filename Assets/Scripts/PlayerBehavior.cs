using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rigidBody;
    private Vector2 moveInput;
    public Animator anim;
    private SpriteRenderer spriteRenderer;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    public float fireRate;
    float firecooldown;

    // Start is called before the first frame update
    void Start()
    {

        rigidBody= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        if(spriteRenderer == null)
        {
            Debug.Log("Sprite missing a renderer");
        }

        firecooldown = fireRate;
        fireRate= 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            spriteRenderer.flipX = false;
            //Debug.Log("Sprite flip false");
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
           // Debug.Log("Sprite flip true");
        }
        anim.SetFloat("Horizontal", moveInput.x);
        anim.SetFloat("Vertical", moveInput.y);
        anim.SetFloat("Speed", moveInput.sqrMagnitude);
        //Debug.Log("Test");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * bulletSpeed;
        }
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
