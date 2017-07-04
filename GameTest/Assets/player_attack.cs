using UnityEngine;
using System.Collections;

public class player_attack : MonoBehaviour {
	public GameObject bullet = null;
	public GameObject player = null;
	public float destroy_time = 3;
	public Vector3 temp_direction = Vector3.forward;
    
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		if (bullet != null&&destroy_time > 0) {
			bullet.transform.Translate (temp_direction * Time.deltaTime);
			destroy_time = destroy_time - Time.deltaTime;
		}else if(bullet != null && destroy_time <= 0)
        {
            bullet.SetActive(false);
        }
	}

	public void commonAttack(){
		//Debug.Log ("commonAttack");
		if (bullet == null) {
            bullet = Instantiate (Resources.Load ("bullet")) as GameObject;
        }
        else
        {
            bullet.SetActive(true);
        }
		bullet.transform.position = player.transform.position;
		destroy_time = 3;
		temp_direction = player.transform.forward * 5;
		//Debug.Log ("direction:" + temp_direction);
	}

    
}
