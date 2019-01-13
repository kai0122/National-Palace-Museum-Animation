using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFightSoilderSelfHorseMan : MonoBehaviour {
    public Animator anim;
    public moveFightSoilderSelfHorse m_moveFightSoilderSelfHorse;
    public int fight;
    // Use this for initialization
    void Start () {
        fight = -1;
        Random.seed = (int)System.DateTime.Now.Ticks;
        anim = GetComponent<Animator>();
        m_moveFightSoilderSelfHorse = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<moveFightSoilderSelfHorse>();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_moveFightSoilderSelfHorse.running == true)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        fight = Random.Range(0, 7);
        anim.SetInteger("Fight", fight);
        if (m_moveFightSoilderSelfHorse.death == true)
        {
            anim.SetBool("Death", true);
        }

    }
}
