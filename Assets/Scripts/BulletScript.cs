using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    //attributes
    public int bulletSpeed;

	// Use this for initialization
	void Start () {
        bulletSpeed = 5000;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.x > 300 || gameObject.transform.position.x < -300 || gameObject.transform.position.y > 300 || gameObject.transform.position.y < -300)
            Destroy(gameObject);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
