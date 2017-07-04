using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class base_menu : MonoBehaviour {
	public bool game_start = false;
	public bool game_exit = false;
	public GameObject player = null;
	public Transform target = null;
	public Image stick; 
	public Camera top_camera;
	//盛放场景ID的表单数组
	// Use this for initialization
	void Start () {
		print ("SDFADF");

		player = GameObject.Find ("player");
//		Debug.Log("rotat"+Vector2.Angle (new Vector2 (10, 10),new Vector2(10, 100)));
	}
	
	// Update is called once per frame
	void Update () {
		if (game_start)
		{
			print ("开始游戏");
			SceneManager.LoadScene ("MainScene");
			//Application.LoadLevel("MainScene");	

		}
		if (game_exit)
		{
			print ("退出游戏");
			Application.Quit();
		}

		if (player) {
//			return;
			//print ("have a player");

//			transform.LookAt (player.transform.position);
			if(stick){
//				float angle = Vector2.Angle (new Vector2 (100, 100), new Vector2 (stick.transform.position.x, stick.transform.position.y));
				float posx = stick.transform.localPosition.x;
				float posy = stick.transform.localPosition.y;
				float add_value = 90;
				if (posx != 0 || posy != 0) {
					if (posx == 0) {
						posx = 0.001f;
					}
					if(posx < 0){
						add_value = 90 + 180;
					}
					float angle = Mathf.Atan(stick.transform.localPosition.y/posx);
					float final_degree = angle * 180 / 3.14159f;
//					Debug.Log ("angel:" + final_degree);
					player.transform.rotation = Quaternion.Euler(0, add_value - final_degree, 0);
				}

				player.transform.position = new Vector3 (player.transform.position.x + Time.deltaTime * stick.transform.localPosition.x / 30, player.transform.position.y, player.transform.position.z + Time.deltaTime * stick.transform.localPosition.y / 30);
				if (top_camera) {
					top_camera.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 15, player.transform.position.z);
				}
			}

		}
	}

	public void onClickStartGame(){
		game_start = true;
	}

	public void onClickQuitGame(){
		game_exit = true;
	}

	public void onClickToFirstScene(){
		SceneManager.LoadScene ("first_scene", LoadSceneMode.Single);
		print (SceneManager.sceneCount);
	}

	public void onClickGoToShop(){
		SceneManager.LoadScene ("shop_scene", LoadSceneMode.Single);
		print (SceneManager.sceneCount);
	}

	public void onClickGoToMainScene(){
		SceneManager.LoadScene ("MainScene", LoadSceneMode.Single);
		print (SceneManager.sceneCount);
	}
    public void onClickStartFight()
    {
        GameObject objPrefab = (GameObject)Resources.Load("monsters/monster");
        Instantiate(objPrefab);
    }
}
