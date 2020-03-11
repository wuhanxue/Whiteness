using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationControl : MonoBehaviour {

	RoleControl player;
	RoleControl enemy;
	GameObject operationPanel;

	public GameState currentState = GameState.Menu;

	public bool isPlayerAction = true;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag(Const.Player).GetComponent<RoleControl>();
		enemy = GameObject.FindGameObjectWithTag(Const.Enemy).GetComponent<RoleControl>();
		operationPanel = GameObject.Find("OperationPanel");
	}

	void Update()
	{
		if (isPlayerAction)
		{
			operationPanel.SetActive(true);
		}
		else
		{
			operationPanel.SetActive(false);
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
		enemy.CommonAttack();
		player.ReceiveDamage(enemy.attackDamage);
	}

	/// <summary>
	/// 技能按钮
	/// </summary>
	public void OnSklBtnClick()
	{
		player.SkillAttack();
		enemy.ReceiveDamage(player.skillDamage);
		enemy.CommonAttack();
		player.ReceiveDamage(enemy.attackDamage);
	}

	/// <summary>
	/// 防御按钮
	/// </summary>
	public void OnDefBtnClick()
	{
		player.Defend();
		enemy.CommonAttack();
		player.ReceiveDamage(enemy.attackDamage - player.defence);
	}
}
