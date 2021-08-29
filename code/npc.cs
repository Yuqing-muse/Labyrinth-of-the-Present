using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{

    public int state = 1;

    public Animator animator;
    //这里加    public .....（触发交谈的代码的名字） mcc;  比方说像（public MC_Controller mcc;）
    public duihua say;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        say = this.GetComponent<duihua>();
        //找到触法交谈代码 GameObject.Find（“脚本所在物体名”）.GetComponent<脚本名>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 1://idle
                animator.SetBool("isTalk", false);
                break;
            case 2://talk
                animator.SetBool("isTalk", true);
                break;
        }
    }
}

// 在触法交谈的代码，前面声明加上  public NPCController animc;
// 触法交谈的地方加上 animc.state = 2；  交谈结束加上 animc.state = 1；
// start里面  GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCController>();
