using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoLog : MonoBehaviour {

	UITextList textList;

	// Use this for initialization
	void Start () {
		textList = GetComponent<UITextList>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddText(string info)
	{
		textList.Add(info);
		textList.scrollValue = 1f;
	}
}
