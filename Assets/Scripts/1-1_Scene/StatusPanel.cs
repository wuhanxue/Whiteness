using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour {

	public GameObject owner;

	private UnitStatus ownerStatus;
	private GameObject uiRoot;
	private GameObject statusPanels;

	// HP滑动器
	private UISlider hpBar;
	// SP滑动器
	private UISlider spBar;

	// Use this for initialization
	void Start () {
		hpBar = transform.Find(Const.HPBar).GetComponent<UISlider>();
		spBar = transform.Find(Const.SPBar).GetComponent<UISlider>();
		ownerStatus = owner.GetComponent<UnitStatus>();
		// 定位
		uiRoot = GameObject.Find("UI Root");
		statusPanels = GameObject.Find("UI Root/StatusPanel");
		Transform[] panels = statusPanels.GetComponentsInChildren<Transform>();
		for (int i = 0; i < panels.Length; i++)
		{
			if (panels[i].name.Contains("StatusPanel") && panels[i].childCount == 0)
			{
				transform.SetParent(panels[i], false);
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		hpBar.value = ownerStatus.healthPercent;
		spBar.value = ownerStatus.energyPercent;
	}
}
