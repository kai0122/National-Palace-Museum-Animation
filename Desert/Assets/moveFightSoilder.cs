using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFightSoilder : MonoBehaviour {
    //public Animator anim;
    private int start;
    private int stage1;
    private int stage2;
    private int stage3;
    private int count;
    public GameObject[] Players1;
    public int[] Players1Match;
    public GameObject[] Players2;
    public int[] Players2Match;
    // Use this for initialization
    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        start = -1;
        stage1 = -1;
        stage2 = -1;
        stage3 = -1;
        //count = 
        //anim = GetComponent<Animator>();
        InitialMatch();
    }

    // Update is called once per frame
    void Update () {
        
        for (int i = 0; i < Players1.Length; i++)
        {
            int ifChange = Random.Range(0, 100);
            if (ifChange < 1)
            {
                ChooseOther(Players1[i], 1, i);
            }
        }
        for (int i = 0; i < Players2.Length; i++)
        {
            int ifChange = Random.Range(0, 100);
            if (ifChange < 1)
            {
                ChooseOther(Players2[i], 2, i);
            }
        }
        
        /*
        if(start == -1)
        {
                Random.seed = (int)System.DateTime.Now.Ticks;
                start = Random.Range(1, 2);
                anim.SetInteger("Start", start);
        }
        else
        {
                Random.seed = (int)System.DateTime.Now.Ticks;
                stage1 = Random.Range(0, 8);
                anim.SetInteger("Stage1", stage1);
                //Random.seed = (int)System.DateTime.Now.Ticks;
                stage2 = Random.Range(0, 8);
                anim.SetInteger("Stage2", stage2);
                //Random.seed = (int)System.DateTime.Now.Ticks;
                stage3 = Random.Range(0, 2);
                anim.SetInteger("Stage3", stage3);
        }
        if (Input.GetKey("m"))
        {
            anim.SetBool("StartFight", true);
        }
        */
    }

    void InitialMatch()
    {
        for(int i = 0; i < Players1.Length; i++)
        {
            int temp = Random.Range(0, Players2.Length);
            Players1Match[i] = temp;
        }
        for (int i = 0; i < Players2.Length; i++)
        {
            int temp = Random.Range(0, Players1.Length);
            Players2Match[i] = temp;
        }
    }

    void ChooseOther(GameObject Me, int num, int i)
    {
        if(num == 1)
        {
            int temp = Random.Range(0, Players2.Length);
            Players1Match[i] = temp;
        }
        if (num == 2)
        {
            int temp = Random.Range(0, Players1.Length);
            Players2Match[i] = temp;
        }
    }
    
}
