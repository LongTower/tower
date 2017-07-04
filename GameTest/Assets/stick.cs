using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stick : MonoBehaviour {

	// Use this for initialization
	public int speed = 1000;
	public float half_width;
	public float half_height;
    public RectTransform bg;
	public GameObject player = null;
	private Animator animator;
	public string now_anim_state = "to_free";
	private float attack_time = 0.1f;
	private Vector3 touch_begin;
	private int touch_stick_id = -1;
    public int hp = 1000;
    public int atk = 10;
    public Camera main_camera;

    public Monster monster;
	void Start () {

        half_width = bg.rect.width / 2;
        half_height = bg.rect.height / 2;
		player = GameObject.Find ("player");
		animator = player.GetComponent<Animator>();

		for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; ++i) {
			if (animator.runtimeAnimatorController.animationClips [i].name == "attack") {

				AnimationEvent auidoEvent = new AnimationEvent();  
				auidoEvent.time = animator.runtimeAnimatorController.animationClips [i].length * 0.75f;  
				auidoEvent.functionName = "commonAttack";
				animator.runtimeAnimatorController.animationClips [i].AddEvent(auidoEvent);
			}
		}

		GameObject btnObj = GameObject.FindGameObjectWithTag("btn_1");
		Button btn = btnObj.GetComponent<Button>();
		btn.onClick.AddListener(delegate() {
			this.OnClick(btnObj); 
		});
			
    }
	
	// Update is called once per frame
	void Update () {

		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			touchOnPhone ();
		}else if(Application.platform == RuntimePlatform.WindowsEditor){
			touchOnPC ();
		}
	}
	
	void freeStickMove(){
		float step = speed * Time.deltaTime;
		gameObject.transform.localPosition = Vector3.MoveTowards (transform.localPosition, new Vector3 (0, 0, 0), step);
		if (attack_time > 0) {
			attack_time = attack_time - Time.deltaTime;
			return;
		}
		changeAnimationState ("to_free");
	}
	//攻击动作
	void OnClick (GameObject btnObj) {
		for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; ++i) {
			if (animator.runtimeAnimatorController.animationClips [i].name == "attack") {
				attack_time = animator.runtimeAnimatorController.animationClips [i].length; 
			}
		}
		changeAnimationState("to_attack");
	}

	public void changeAnimationState(string name){
		if (name == null) {
			name = "to_free";
		} else if (now_anim_state == name) {
			return;
		}
		animator.SetBool (now_anim_state, false);
		animator.SetBool (name, true);

		now_anim_state = name;
	}

    public void beHit(int hurt)
    {
        changeAnimationState("to_hit");
    }
    void touchOnPC(){
        //main_camera.transform.position = new Vector3(player.transform.position.x, 8, player.transform.position.z - 5);
        //main_camera.transform.LookAt(player.transform.position);
        if (Input.GetMouseButtonDown (0)) {
			touch_begin = Input.mousePosition;
		} else if (Input.GetMouseButton (0)) {
			if (Vector3.Distance (bg.position, touch_begin) < half_width) {
				changeAnimationState ("to_walk");
				if (Vector3.Distance(bg.position, Input.mousePosition) < half_width) {
					//范围内，可以触摸移动
					Vector3 pos = new Vector3 (Input.mousePosition.x - bg.position.x, Input.mousePosition.y - bg.position.y, 0);
                    moveStick(pos);
                } else {
					Vector3 target = Input.mousePosition - bg.position;
					float distance = Vector3.Distance (Input.mousePosition, bg.position);
					if (distance == 0) {
						distance = 0.01f;
					}

					float temp_x = (half_width) / distance * target.x;
					float temp_y = (half_height) / distance * target.y;
                    moveStick(new Vector3(temp_x, temp_y, 0));
                }
			} else {
				freeStickMove ();
                Vector2 off_pos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                moveCamera(off_pos);
            }
		}else {
			freeStickMove ();
		}
	}

	void touchOnPhone(){
        if(Input.touchCount <=0)
        {
            freeStickMove();
            return;
        }
        for (int i = 0; i < Input.touchCount; i++)
        {
            if(i >= 2)
            {
                break;
            }
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                if (Vector2.Distance(new Vector2(bg.position.x, bg.position.y), Input.GetTouch(i).position) < half_width)
                {
                    touch_stick_id = i;
                }
            }
            else if (Input.touches[i].phase == TouchPhase.Moved)
            {
                if(touch_stick_id == i)
                {
                    changeAnimationState("to_walk");
                    if (Vector2.Distance(Input.GetTouch(i).position, new Vector2(bg.position.x, bg.position.y)) < half_width)
                    {
                        //范围内，可以触摸移动
                        Vector3 pos = new Vector3(Input.GetTouch(i).position.x - bg.position.x, Input.GetTouch(i).position.y - bg.position.y, 0);
                        moveStick(pos);
                    }
                    else
                    {
                        Vector2 target = Input.GetTouch(i).position - new Vector2(bg.position.x, bg.position.y);
                        float distance = Vector2.Distance(Input.GetTouch(i).position, new Vector2(bg.position.x, bg.position.y));
                        if (distance == 0)
                        {
                            distance = 0.01f;
                        }

                        float temp_x = (half_width) / distance * target.x;
                        float temp_y = (half_height) / distance * target.y;
                        moveStick(new Vector3(temp_x, temp_y, 0));
                    }
                }
                else
                {
                    Vector2 off_pos = Input.GetTouch(i).deltaPosition;
                    moveCamera(off_pos);
                }
            }
            else if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                freeStickMove();
                if(touch_stick_id == i)
                {
                    touch_stick_id = -1;
                }
            }
        }
	}

    void moveStick(Vector3 pos)
    {
        transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
    }

    void moveCamera(Vector2 off_pos)
    {
        main_camera.transform.position = new Vector3(main_camera.transform.position.x - off_pos.x / 5, 10, main_camera.transform.position.z - off_pos.y / 5);
    }
}