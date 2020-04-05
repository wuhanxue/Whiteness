using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsPanel : MonoBehaviour {

	UIButton sklBtn1;
	UIButton sklBtn2;


	void Awake()
	{
		sklBtn1 = GameObject.Find("TacticsPanel_3/SklBtn_1").GetComponent<UIButton>();
		sklBtn2 = GameObject.Find("TacticsPanel_3/SklBtn_2").GetComponent<UIButton>();
		sklBtn1.onClick.Add(new EventDelegate(() => { PlayerSkillChoose("S_002_001"); }));
		sklBtn2.onClick.Add(new EventDelegate(() => { PlayerSkillChoose("S_002_002"); }));
	}

	void PlayerSkillChoose(string skillId)
	{
		// 玩家操作
		BattleTurnSystem._instance.PlayerSkillChoose(skillId);
	}
}
