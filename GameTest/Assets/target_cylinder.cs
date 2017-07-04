using UnityEngine;
using System.Collections;

public class target_cylinder : MonoBehaviour {
    private Vector3 move_distance;
    // Use this for initialization
    void Start () {
        move_distance = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position == move_distance)
            return;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(move_distance.x, transform.position.y, move_distance.z), Time.deltaTime * 10);
    }

    public void HitMove(Vector3 distance)
    {
        move_distance = distance;
    }
}
