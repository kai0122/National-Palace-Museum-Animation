using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSoilder : MonoBehaviour {
    public Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        
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
        if (Input.GetKeyDown("v"))
        {
            anim.SetBool("V", true);
        }
        else
        {
            anim.SetBool("V", false);
        }
    }
}
