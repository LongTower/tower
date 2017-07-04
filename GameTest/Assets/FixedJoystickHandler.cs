using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class FixedJoystickHandler : MonoBehaviour, IDragHandler , IEndDragHandler , IBeginDragHandler {
	[System.Serializable]
	public class VirtualJoystickEvent : UnityEvent<Vector3>{}
	// Use this for initialization
	public Transform content;
	public UnityEvent beginControl;
	public VirtualJoystickEvent controlling;
	public UnityEvent endControl;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBeginDrag(PointerEventData eventData){

		this.beginControl.Invoke();
	}

	public void OnDrag(PointerEventData eventData){

		if(this.content){
			this.controlling.Invoke(this.content.localPosition.normalized);
		}
	}

	public void OnEndDrag(PointerEventData eventData){

		this.endControl.Invoke();
	}
}
