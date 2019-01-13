using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horsemanAUController : MonoBehaviour {
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    
	
	// Update is called once per frame
	void Update () {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        //bool fire = Input.GetButtonDown("Fire1");

        if (Input.GetKeyDown("w"))
        {
            anim.SetBool("Run_IDLE", true);
        }
        else
        {
            anim.SetBool("Run_IDLE", false);
        }
        if (Input.GetKeyDown("r"))
        {
            anim.SetBool("RoarUp", true);
        }
        else
        {
            anim.SetBool("RoarUp", false);
        }
        if (Input.GetKeyDown("q"))
        {
            anim.SetBool("Q", true);
        }
        else
        {
            anim.SetBool("Q", false);
        }
        if (Input.GetKeyDown("e"))
        {
            anim.SetBool("E", true);
        }
        else
        {
            anim.SetBool("E", false);
        }
        if (Input.GetKeyDown("c"))
        {
            anim.SetBool("C", true);
        }
        else
        {
            anim.SetBool("C", false);
        }
        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);


    }
}
