using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour {

	public GameObject owner;

	private UnitStatus ownerStatus;

	// HP滑动器
	private UISlider hpBar;
	// SP滑动器
	private UISlider spBar;

	// Use this for initialization
	void Start () {
		hpBar = owner.transform.Find(Const.HPBar).GetComponent<UISlider>();
		spBar = owner.transform.Find(Const.SPBar).GetComponent<UISlider>();
		ownerStatus = owner.GetComponent<UnitStatus>();
	}
	
	// Update is called once per frame
	void Update () {
		hpBar.value = ownerStatus.bloodPercent;
	}
}
