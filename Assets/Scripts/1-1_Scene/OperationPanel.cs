using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationPanel : MonoBehaviour {

	UIButton atkBtn;
	UIButton sklBtn;
	UIButton defBtn;


	void Awake()
	{
		atkBtn = GameObject.Find("AtkBtn").GetComponent<UIButton>();
		sklBtn = GameObject.Find("SklBtn").GetComponent<UIButton>();
		defBtn = GameObject.Find("DefBtn").GetComponent<UIButton>();
		atkBtn.onClick.Add(new EventDelegate(() => { PlayerSkillChoose(1); }));
		sklBtn.onClick.Add(new EventDelegate(() => { PlayerSkillChoose(2); }));
		defBtn.onClick.Add(new EventDelegate(() => { PlayerSkillChoose(3); }));
	}

	void PlayerSkillChoose(int skillId)
	{
		// 玩家操作
		BattleTurnSystem._instance.PlayerSkillChoose(skillId);
	}
}
