using UnityEngine;
using System.Collections;

public class MenuPanel : MonoBehaviour
{
	private Animator animator;
	private CanvasGroup group;

	
	public bool IsOpen {
		get {return animator.GetBool("IsOpen");}
		set {animator.SetBool("IsOpen", value);}
	}
	
	// Use this for initialization
	void Awake ()
	{
		animator = GetComponent<Animator> ();
		group = GetComponent<CanvasGroup> ();
		
		var rect = GetComponent<RectTransform> ();
		rect.offsetMax = rect.offsetMin = new Vector2 (0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Open")) {
			group.interactable = group.blocksRaycasts = false;
		} else {
			group.interactable = group.blocksRaycasts = true;
		}
	}
}

