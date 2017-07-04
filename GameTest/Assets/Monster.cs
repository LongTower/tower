using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    enum anim_model
    {
        free,
    }
    public GameObject begin_pos = null;
    public GameObject end_pos = null;
    public GameObject player = null;


    private Vector3 end_position;
    private float walk_speed = 0.02f;
    private int walk_interval_time = 60;
    private float attack_range = 3;
    // Use this for initialization
    void Start () {
        Debug.Log("进入了prefab");

        //AnimationClip clip1 = GetComponent<Animation>().GetClip("free");
        //AnimationClip clip2 = GetComponent<Animation>().GetClip("free2");
        //Debug.Log("clip1" + clip1);
        //Debug.Log("clip2" + clip2);
        //playanimation("free");
        PlayAnimation("idle");
        //初始化，设置坐标位置
        if(begin_pos == null)
        {
            begin_pos = GameObject.Find("monster_begin");
        }
        transform.position = begin_pos.transform.position;

        if (end_pos == null)
        {
            end_pos = GameObject.Find("target_cylinder");
        }
        end_position = end_pos.transform.position;
        if (player == null)
            player = GameObject.Find("player");
        
        GetComponent<Animation>()["walk"].speed = 0.3f;
    }
	
	// Update is called once per frame
	void Update () {
        //if (walk_interval_time < 0)
        //{
            
            //计算和改变方向,判断是否接近player
            
            float target_distance = Vector3.Distance(transform.position, end_pos.transform.position);
            float player_distance = Vector3.Distance(transform.position, player.transform.position);
            if(target_distance < attack_range)
            {
                transform.LookAt(end_pos.transform.position);
                PlayAnimation("attack");
                walk_interval_time = 100;
            }
            else if (player_distance < attack_range)
            {
                transform.LookAt(player.transform.position);
                PlayAnimation("attack");
                walk_interval_time = 100;
            }
            else if(!GetComponent<Animation>().IsPlaying("attack"))
            {
                float step = walk_speed;
                transform.position = Vector3.MoveTowards(transform.position, end_pos.transform.position, step);
                transform.LookAt(end_pos.transform.position);
                PlayAnimation("walk");
            }



            
        //    return;
        //}
        //else
        //{
        //    walk_interval_time = walk_interval_time - 1;
        //}
    }

    /*主要逻辑是
        1.让怪物在一定角度内向end_pos推箱子，
            先向柱子走，
            然后判断柱子是否在范围内，
            然后攻击
                如果玩家在范围内，并且攻击自己，则自身选择：
                    1.攻击玩家
                    2.忽视玩家，攻击柱子
                    3.其他状态
        2.碰到玩家，攻击玩家
            在走向柱子的时候，如果更早遇到玩家，则攻击玩家，直到玩家离开攻击判断范围，再走向柱子
    */

    public void PlayAnimation(string name)
    {
        if (GetComponent<Animation>().GetClip(name))
        {
            GetComponent<Animation>().Play(name);
        }
        else
        {
            Debug.Log("不存在动画" + name);
        }
    }
}
