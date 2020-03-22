using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationPanel : MonoBehaviour {

	UIButton sklBtn1;
	UIButton sklBtn2;
	UIButton sklBtn3;
	UIButton sklBtn4;


	void Awake()
	{
		sklBtn1 = GameObject.Find("SklBtn_1").GetComponent<UIButton>();
		sklBtn2 = GameObject.Find("SklBtn_2").GetComponent<UIButton>();
		sklBtn3 = GameObject.Find("SklBtn_3").GetComponent<UIButton>();
		sklBtn4 = GameObject.Find("SklBtn_4").GetComponent<UIButton>();
		sklBtn1.onClick.Add(new EventDelegate(() => { PlayerSkillChoose(1); }));
		sklBtn2.onClick.Add(new EventDelegate(() => { PlayerSkillChoose(2); }));
		sklBtn3.onClick.Add(new EventDelegate(() => { PlayerSkillChoose(3); }));
		sklBtn4.onClick.Add(new EventDelegate(() => { PlayerSkillChoose(4); }));
	}

	void PlayerSkillChoose(int skillId)
	{
		// 玩家操作
		BattleTurnSystem._instance.PlayerSkillChoose(skillId);
	}
}
