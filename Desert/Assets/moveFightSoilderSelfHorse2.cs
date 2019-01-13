using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFightSoilderSelfHorse2 : MonoBehaviour {
    public Animator anim;
    private int stage1;
    private int stage2;
    private int stage3;
    private moveFightSoilderHorse m_moveFightSoilder;
    public int TargetIndex;
    public GameObject Target;
    public int MyIndex;
    public bool running;
    public bool death;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        Random.seed = (int)System.DateTime.Now.Ticks;
        m_moveFightSoilder = gameObject.transform.parent.transform.parent.GetComponent<moveFightSoilderHorse>();
        TargetIndex = -1;
        anim.SetInteger("Start", 1);
        anim.SetBool("StartFight", false);
        running = true;
        death = false;
        stage2 = 0;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (TargetIndex != m_moveFightSoilder.Players2Match[MyIndex])
        {
            anim.SetBool("StartFight", false);
            TargetIndex = m_moveFightSoilder.Players2Match[MyIndex];
            Target = m_moveFightSoilder.Players1[TargetIndex];
            RunToOther();
        }
        else
        {
            stage1 = Random.Range(0, 7);
            anim.SetInteger("Stage1", stage1);
            //Random.seed = (int)System.DateTime.Now.Ticks;
            if (Vector3.Distance(gameObject.transform.position, Target.transform.position) < 8)
            {
                
                if (stage2 == 1)
                    death = true;
                else
                {
                    stage2 = Random.Range(1, 100);
                    anim.SetInteger("Stage2", stage2);
                }
                    
            }
            //Random.seed = (int)System.DateTime.Now.Ticks;

        }
        if (Vector3.Distance(gameObject.transform.position, Target.transform.position) < 8)
        {
            running = false;
            if (running == false)
                anim.SetBool("StartFight", true);
        }
        else
        {
            gameObject.transform.LookAt(Target.transform);
            running = true;
        }
    }

    void RunToOther()
    {
        gameObject.transform.LookAt(Target.transform);
        anim.SetInteger("Stage2", 2);
    }
}
