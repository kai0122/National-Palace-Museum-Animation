using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFightSoilderSelf : MonoBehaviour {
    public Animator anim;
    private int stage1;
    private int stage2;
    private int stage3;
    private moveFightSoilder m_moveFightSoilder;
    public int TargetIndex;
    public GameObject Target;
    public int MyIndex;
    public bool isDead;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        Random.seed = (int)System.DateTime.Now.Ticks;
        m_moveFightSoilder = gameObject.transform.parent.transform.parent.GetComponent<moveFightSoilder>();
        TargetIndex = -1;
        anim.SetInteger("Start", 1);
        anim.SetBool("StartFight", false);
        isDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(TargetIndex != m_moveFightSoilder.Players1Match[MyIndex])
        {
            anim.SetBool("StartFight", false);
            TargetIndex = m_moveFightSoilder.Players1Match[MyIndex];
            Target = m_moveFightSoilder.Players2[TargetIndex];
            if (!isDead)
                RunToOther();
        }
        else
        {
            
            stage1 = Random.Range(0, 8);
            anim.SetInteger("Stage1", stage1);
            //Random.seed = (int)System.DateTime.Now.Ticks;
            if (!isDead)
            {
                gameObject.transform.LookAt(Target.transform);
                stage2 = Random.Range(0, 500);
                if (stage2 >= 7) stage2 = 7;
                anim.SetInteger("Stage2", stage2);
                //Random.seed = (int)System.DateTime.Now.Ticks;
                if (stage2 == 0)
                    isDead = true;
            }
            stage3 = Random.Range(0, 2);
            anim.SetInteger("Stage3", stage3);
        }
        if (Vector3.Distance(gameObject.transform.position, Target.transform.position) < 8)
        {
            anim.SetBool("StartFight", true);
        }
        else
        {
            gameObject.transform.LookAt(Target.transform);
        }

    }

    void RunToOther()
    {
        gameObject.transform.LookAt(Target.transform);
        anim.SetInteger("Stage2", 8);
    }
}
