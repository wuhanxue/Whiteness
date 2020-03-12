using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationControl : MonoBehaviour {

	RoleControl player;
	RoleControl enemy;
	UIPanel operationPanel;

	public GameState currentState = GameState.Menu;

	public bool isPlayerAction = true;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag(Const.Player).GetComponent<RoleControl>();
		enemy = GameObject.FindGameObjectWithTag(Const.Enemy).GetComponent<RoleControl>();
		operationPanel = GameObject.Find("OperationPanel").GetComponent<UIPanel>();
	}

	void Update()
	{
		if (isPlayerAction)
		{
			operationPanel.enabled = true;
		}
		else
		{
			operationPanel.enabled = false;
		}

		if (currentState == GameState.Game)
		{
			if (player.hp <= 0)
			{
				Debug.Log("Failed");
				currentState = GameState.Over;
			}
			else if (enemy.hp <= 0)
			{
				Debug.Log("Success");
				currentState = GameState.Over;
			}
		}
	}

	/// <summary>
	/// 攻击按钮
	/// </summary>
	public void OnAtkBtnClick()
	{
		player.CommonAttack();  
		enemy.ReceiveDamage(player.attackDamage);
		StartCoroutine("WaitTime");
		
	}

	/// <summary>
	/// 技能按钮
	/// </summary>
	public void OnSklBtnClick()
	{
		player.SkillAttack();
		enemy.ReceiveDamage(player.skillDamage);
		StartCoroutine("WaitTime");
	}

	/// <summary>
	/// 防御按钮
	/// </summary>
	public void OnDefBtnClick()
	{
		player.Defend();
		StartCoroutine("WaitTime");
	}

	IEnumerator WaitTime()
	{
		// 玩家回合结束
		isPlayerAction = false;
		// 等待敌人攻击
		yield return new WaitForSeconds(1f);
		enemy.CommonAttack();
		player.ReceiveDamage(enemy.attackDamage);
		// 玩家回合开始
		isPlayerAction = true;
	}
}
