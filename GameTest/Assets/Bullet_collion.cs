using UnityEngine;
using System.Collections;

public class Bullet_collion : MonoBehaviour {
    public GameObject player = null;
    
    // Use this for initialization
    void Start () {
        if (player == null)
            player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("啊，碰撞了"+ other.GetComponent<Collider>().tag);
        if(other.GetComponent<Collider>().tag == "anemy")
        {
            //Debug.Log("啊，消失了");
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.GetComponent<Collider>().tag == "wall")
        {
            
            Destroy(this.gameObject);
        }
        else if (other.GetComponent<Collider>().tag == "target")
        {
            
            //gameObject.SetActive(false);
            //这里的逻辑，碰撞后应该调用柱子自己的移动函数，明天添加一个脚本运行柱子移动
            Vector3 move_distance = other.transform.position + (other.transform.position - player.transform.position) / 3;
            move_distance.y = other.transform.position.y;
            other.gameObject.GetComponent<target_cylinder>().HitMove(move_distance);
            Destroy(this.gameObject);
        }
    }
}
