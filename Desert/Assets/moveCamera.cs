using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {
    public GameObject TargetToLook;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("i"))
        {
            gameObject.transform.position += gameObject.transform.forward * 0.05f;
        }
        if (Input.GetKey("l"))
        {
            gameObject.transform.position += Quaternion.Euler(0,  90, 0) * gameObject.transform.forward * 0.05f;
        }
        if (Input.GetKey("j"))
        {
            gameObject.transform.position += Quaternion.Euler(0, -90, 0) * gameObject.transform.forward * 0.05f;
        }
        if (Input.GetKey("k"))
        {
            gameObject.transform.position += -gameObject.transform.forward * 0.05f;
        }
        gameObject.transform.LookAt(TargetToLook.transform.position);
    }
}
