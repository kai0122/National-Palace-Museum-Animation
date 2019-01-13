using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manAUController : MonoBehaviour {
    public Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        //bool fire = Input.GetButtonDown("Fire1");

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
        if (Input.GetKeyDown("z"))
        {
            anim.SetBool("Z", true);
        }
        else
        {
            anim.SetBool("Z", false);
        }
        if (Input.GetKeyDown("x"))
        {
            anim.SetBool("X", true);
        }
        else
        {
            anim.SetBool("X", false);
        }
        if (Input.GetKeyDown("c"))
        {
            anim.SetBool("C", true);
        }
        else
        {
            anim.SetBool("C", false);
        }
        if (Input.GetKeyDown("f"))
        {
            anim.SetBool("F", true);
        }
        else
        {
            anim.SetBool("F", false);
        }
        if (Input.GetKeyDown("s"))
        {
            anim.SetBool("S", true);
        }
        else
        {
            anim.SetBool("S", false);
        }
        //anim.SetFloat("inputH", inputH);
        //anim.SetFloat("inputV", inputV);
    }
}
