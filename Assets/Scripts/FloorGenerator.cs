using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

    // attributes
    public GameObject prefab;
    public Camera mainCam;
    private float prefabLength;
    private float camSize;

	// Use this for initialization
	void Start () {
        // get the sizes of the screen and flooring
        prefabLength = prefab.GetComponent<BoxCollider2D>().size.x;
        camSize = (Camera.main.orthographicSize * Screen.width / Screen.height) * 2;

        // generate the flooring
        for (float i = 0; i < camSize; i += prefabLength)
            Instantiate(prefab, new Vector3(i - 16f, -9), gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
