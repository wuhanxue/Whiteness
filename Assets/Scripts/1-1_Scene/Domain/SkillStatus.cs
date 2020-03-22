using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStatus {
	// 编号
	public string skillId;
	// 名称
	public string skillName;
	// 伤害
	public int damage = 10;
	// 命中率
	public float rate = 1f;
	// 攻击回合数
	public int turnCount = 1;
	// 攻击类型
	public SkillType skillType = SkillType.Single;

}
