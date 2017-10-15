using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    // attributes
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int timer;

	// Use this for initialization
	void Start () {
        // components
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        // ints
        timer = 0;

        // lock the rotation of the sprite
        rb.freezeRotation = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(timer % 10 == 0)
            sr.color = Random.ColorHSV();

        timer++;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
            Destroy(gameObject);
    }
}
