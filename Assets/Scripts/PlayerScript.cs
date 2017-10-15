using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // attributes
    private KeyCode left, right, jump, fire,lastKey;
    private Vector3 playerPosition, shotLocation;
    private float accelerationUnitX, accelerationUnitY, maxSpeedX, maxSpeedY, velocityX, accelerationX, velocityY, accelerationY;
    private bool midair;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public GameObject bullet;
    private int bulletDirection;

	// Use this for initialization
	void Start ()
    {
        // keys
        left = KeyCode.A;
        right = KeyCode.D;
        jump = KeyCode.W;
        fire = KeyCode.Space;

        // floats
        accelerationUnitX = 0.01f;
        accelerationUnitY = 0.005f;
        maxSpeedX = .15f;
        maxSpeedY = .25f;
        velocityX = 0;
        velocityY = 0;
        accelerationX = 0;
        accelerationY = 0;

        // vector 3s
        playerPosition = gameObject.transform.position;

        // bools
        midair = false;

        // Components
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        // lock the rotation
        rb.freezeRotation = true;

        // ints
        bulletDirection = 1;
    }
	
	// Update is called once per frame
	void Update () {
        // move the object
        gameObject.transform.position += new Vector3(MovementX(), MovementY());

        // shooting location
        if(sr.flipX)
            shotLocation = new Vector2(gameObject.transform.position.x - 1f, gameObject.transform.position.y);

        else if (!sr.flipX)
            shotLocation = new Vector2(gameObject.transform.position.x + 1f, gameObject.transform.position.y);

        // shooting
        if (Input.GetKeyDown(fire))
            Fire();
    }

    float MovementX()
    {
        // Increase or decrease the speed
        if (Input.GetKey(left))
        {
            accelerationX = -accelerationUnitX;
            lastKey = left;
            sr.flipX = true;
            bulletDirection = -1;
        }

        else if (Input.GetKey(right))
        {
            accelerationX = accelerationUnitX;
            lastKey = right;
            sr.flipX = false;
            bulletDirection = 1;
        }

        // if neither left nor right, slow down
        else
        {
            if (velocityX != 0)
            {
                if (velocityX < 0)
                    accelerationX = accelerationUnitX;

                else if (velocityX > 0)
                    accelerationX = -accelerationUnitX;
            }

            else
                accelerationX = 0;
        }

        // increase the velocity
        velocityX += accelerationX;

        // This needs fixing, stops the slow stop
        if (lastKey == left && velocityX > 0)
            velocityX = 0;

        if (lastKey == right && velocityX < 0)
            velocityX = 0;

        // clamp the velocity and make sure it cannot go backwards
        if (velocityX > maxSpeedX)
            velocityX = maxSpeedX;

        if (velocityX < -maxSpeedX)
            velocityX = -maxSpeedX;

        return velocityX;
    }

    float MovementY()
    {
        // check the key
        if (Input.GetKey(jump) && !midair)
        {
            midair = true;
            velocityY = .1f;
            accelerationY = accelerationUnitY;
        }

        if (Input.GetKeyUp(jump))
            accelerationY = 0;

        if (midair)
            velocityY += accelerationY;

        else
            velocityY = 0;

        // clamp the velocity and make sure it cannot go backwards
        if (velocityY > maxSpeedY)
            accelerationY = -accelerationY;

        return velocityY;
    }

    // Collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (midair && col.gameObject.tag == "Ground")
        {
            midair = false;
            velocityY = 0;
            accelerationY = 0;
        }

        // if it is an enemy, remove a life in the future
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit by enemy");
        }
            
    }

    // shooting
    void Fire()
    {
        GameObject shot;
        shot = Instantiate(bullet, shotLocation, bullet.transform.rotation);
        shot.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletDirection * 5000f, 0));
    }
}
