using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationController : MonoBehaviour {

	RoleControl player;
	RoleControl enemy;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag(Const.Player).GetComponent<RoleControl>();
		enemy = GameObject.FindGameObjectWithTag(Const.Enemy).GetComponent<RoleControl>();
	}

	/// <summary>
	/// 攻击按钮
	/// </summary>
	public void OnAtkBtnClick()
	{
		player.CommonAttack();
		enemy.ReceiveDamage(player.attackDamage);
	}

	/// <summary>
	/// 技能按钮
	/// </summary>
	public void OnSklBtnClick()
	{
		player.SkillAttack();
		enemy.ReceiveDamage(player.skillDamage);
	}

	/// <summary>
	/// 防御按钮
	/// </summary>
	public void OnDefBtnClick()
	{
		player.Defend();
	}
}
