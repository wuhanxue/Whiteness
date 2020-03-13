using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OperationControl : MonoBehaviour {

	// 所有参战对象列表
	private List<GameObject> battleUnits;
	// 所有参战玩家列表
	private GameObject[] playerUnits;
	// 所有参战敌人列表
	private GameObject[] enemyUnits;
	// 剩余敌人列表
	private GameObject[] remainingEnemyUnits;
	// 剩余玩家列表
	private GameObject[] remainingPlayerUnits;

	// 当前行动单位
	private GameObject currentActUnit;
	// 当前行动单位的目标
	private GameObject currentActUnitTarget;
	// 玩家选择技能UI的开关
	public bool isWaitForPlayerToChooseSkill = false;
	// 是否等待玩家选择目标，控制射线的开关
	public bool isWaitForPlayerToChooseTarget = false;
	// 玩家选择攻击对象的射线
	private Ray targetChooseRay;
	// 射线目标
	private RaycastHit targetHit;
	// 攻击技能名称
	public string attackTypeName;
	// 攻击伤害系数
	public float attackDamageMultiplier;
	// 伤害值
	public float attackData;


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

		// 创建参战列表
		battleUnits = new List<GameObject>();
		// 添加玩家单位至参战列表
		playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
		playerUnits.ToList<GameObject>().ForEach(p => battleUnits.Add(p));
		// 添加敌方单位至参战列表
		enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
		enemyUnits.ToList<GameObject>().ForEach(e => battleUnits.Add(e));
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
			if (player.health <= 0)
			{
				Debug.Log("Failed");
				currentState = GameState.Over;
			}
			else if (enemy.health <= 0)
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
		enemy.ReceiveDamage(player.attack);
		StartCoroutine("WaitTime");
		
	}

	/// <summary>
	/// 技能按钮
	/// </summary>
	public void OnSklBtnClick()
	{
		player.SkillAttack();
		enemy.ReceiveDamage(player.skill);
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
		player.ReceiveDamage(enemy.attack);
		// 玩家回合开始
		isPlayerAction = true;
	}
}
