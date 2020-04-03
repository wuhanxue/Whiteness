using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 战斗系统
/// </summary>
public class BattleTurnSystem : MonoBehaviour {

	public static BattleTurnSystem _instance;

	private BattleTurnSystem()
	{
		_instance = this;
	}

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

	// 当前行动单位的初始位置
	private Vector3 currentActUnitIniticalPostion;
	// 当前行动单位的初始朝向
	private Quaternion currentActUnitInitialRotation;
	// 当前行动单位的目标位置
	private Vector3 currentActUnitTargetPostion;
	// 玩家是否移动至目标位置
	public bool isUnitRunningToTarget = false;
	// 玩家是否移动回原来位置
	public bool isUnitRunningBack = false;
	// 当前行动单位与目标的距离
	private float distanceToTarget;
	// 当前行动单位距离初始位置的距离
	private float distanceToInitial;
	// 移动速度
	public float unitMoveSpeed = 1f;
	// 当前单位移动后停留位置
	private Vector3 currentUnitStopPosition;

	private Dictionary<object, List<object>> battleTempDict = new Dictionary<object, List<object>>();
	// 上回合玩家选择的目标
	private GameObject lastActUnitTarget;
	// 上回合玩家选择的技能
	private SkillStatus lastActUnitSkill;
	// 标识是否攻击过
	private bool isAttacked;

	private Transform playerActPos;
	private Transform enemyActPos;
	private Transform playerPos_1;
	private Transform playerPos_2;
	private Transform playerPos_3;
	private Transform enemyPos_1;
	private Transform enemyPos_2;
	private Transform enemyPos_3;
	// 初始位置
	private Vector3 currentActUnitInitPos;

	// 攻击技能名称
	public string attackTypeName;
	// 攻击伤害系数
	public float attackDamageMultiplier = 1f;
	// 伤害值
	public int attackValue;
	// 结束画面
	private GameObject endImg;
	// 胜利画面
	private GameObject winImg;
	// 操作画面
	private MenuPanel menuPanel;

	void Awake()
	{
		endImg = GameObject.Find("EndImg");
		endImg.SetActive(false);
		winImg = GameObject.Find("WinImg");
		winImg.SetActive(false);
		menuPanel = GameObject.Find(Const.MenuPanel).GetComponent<MenuPanel>();
		// 出战位置确定
		playerActPos = GameObject.Find(Const.PlayerActPos).transform;
		enemyActPos = GameObject.Find(Const.EnemyActPos).transform;
		// 玩家初始位置
		playerPos_1 = GameObject.Find(Const.PlayerPos_1).transform;
		playerPos_2 = GameObject.Find(Const.PlayerPos_2).transform;
		playerPos_3 = GameObject.Find(Const.PlayerPos_3).transform;
		// 敌人初始位置
		enemyPos_1 = GameObject.Find(Const.EnemyPos_1).transform;
		enemyPos_2 = GameObject.Find(Const.EnemyPos_2).transform;
		enemyPos_3 = GameObject.Find(Const.EnemyPos_3).transform;
	}

	void Start()
	{
		// 创建参战列表
		battleUnits = new List<GameObject>();
		// 添加玩家单位至参战列表
		playerUnits = GameObject.FindGameObjectsWithTag(Const.Player);
		playerUnits.ToList().ForEach(p => battleUnits.Add(p));
		// 添加敌方单位至参战列表
		enemyUnits = GameObject.FindGameObjectsWithTag(Const.Enemy);
		enemyUnits.ToList().ForEach(e => battleUnits.Add(e));
		// 速度排序
		ListSort();
		// 战斗
		ToBattle();
	}

	void Update()
	{
		// 操作面板
		OperatePanel();
		// 选择目标
		ChooseTarget();
		// 移动角色
		MoveUnit();
	}

	private void MoveUnit()
	{
		// 向目标前进
		if (isUnitRunningToTarget)
		{
			distanceToTarget = Vector3.Distance(currentActUnitTargetPostion, currentActUnit.transform.position);
			Vector3 moveDir = currentActUnitTargetPostion - currentActUnitIniticalPostion;
			// 移动范围
			//if (distanceToTarget > 1)
			{
				//currentActUnit.GetComponent<Animator>().SetTrigger("Move");
				//currentActUnit.transform.Translate(moveDir * unitMoveSpeed * Time.deltaTime);
			}
			//else
			{
				//currentActUnit.GetComponent<Animator>().SetTrigger("Idle");
				// 停止移动
				isUnitRunningToTarget = false;
				// 发起进攻
				LanchAttack();
				
			}
		}
		// 返回
		if (isUnitRunningBack)
		{
			distanceToInitial = Vector3.Distance(currentActUnit.transform.position, currentActUnitIniticalPostion);
			Vector3 moveDir = currentActUnitIniticalPostion - currentActUnitTargetPostion;
			// 移动范围
			//if (distanceToInitial > 1)
			{
				//currentActUnit.GetComponent<Animator>().SetTrigger("Move");
				//currentActUnit.transform.Translate(moveDir * unitMoveSpeed * Time.deltaTime);
			}
			//else
			{
				//currentActUnit.GetComponent<Animator>().SetTrigger("Idle");
				isUnitRunningBack = false;
				//currentActUnit.transform.position = currentActUnitIniticalPostion;
				//currentActUnit.transform.rotation = currentActUnitInitialRotation;
				// 下一回合
				ToBattle();
			}
		}
	}

	/// <summary>
	/// 选择目标
	/// </summary>
	private void ChooseTarget()
	{
		// 如果等待选择敌方目标
		if (isWaitForPlayerToChooseTarget)
		{
			targetChooseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(targetChooseRay, out targetHit))
			{
				if (Input.GetMouseButtonDown(0) && targetHit.collider.gameObject.tag == Const.Enemy)
				{
					currentActUnitTarget = targetHit.collider.gameObject;
						
					Debug.Log("选择：" + currentActUnitTarget.name);
					// 关闭敌人的碰撞
					remainingEnemyUnits.ToList().ForEach(p =>
					{
						p.GetComponent<BoxCollider>().enabled = false;
					});
					RunToTarget();
				}
			}
		}
	}

	/// <summary>
	/// 按速度排序
	/// </summary>
	void ListSort()
	{
		// 攻击顺序升序
		battleUnits.Sort((x, y) => x.GetComponent<UnitStatus>().attackTurn.
			CompareTo(y.GetComponent<UnitStatus>().attackTurn));
		battleUnits.ForEach(p => Debug.Log(p.name));
	}

	/// <summary>
	/// 战斗
	/// </summary>
	public void ToBattle()
	{
		// 重新取出剩余单位
		remainingEnemyUnits = GameObject.FindGameObjectsWithTag(Const.Enemy);
		remainingPlayerUnits = GameObject.FindGameObjectsWithTag(Const.Player);

		// 检查存活敌人单位
		if (remainingEnemyUnits.Length == 0)
		{
			Debug.Log("敌人全灭，战斗胜利");
			winImg.SetActive(true);
			return;
		}
		else if (remainingPlayerUnits.Length == 0)
		{
			Debug.Log("我方全灭，战斗失败");
			endImg.SetActive(true);
			return;
		}
		else
		{
			// 战斗单位第一名出栈
			currentActUnit = battleUnits[0];
			battleUnits.RemoveAt(0);
			// 重新加入战斗单位序列
			battleUnits.Add(currentActUnit);
			// 获取角色组件
			UnitStatus attackOwner = currentActUnit.GetComponent<UnitStatus>();

			if (!attackOwner.IsDead)
			{
				currentActUnitInitPos = currentActUnit.transform.position;
				if (currentActUnit.tag == Const.Player)
				{
					currentActUnit.transform.position = playerActPos.position;
				}
				else if (currentActUnit.tag == Const.Enemy)
				{
					currentActUnit.transform.position = enemyActPos.position;
				}
				FindTarget();
			}
			else
			{
				ToBattle();
			}
		}
	}


	void FindTarget()
	{
		if (currentActUnit.tag == Const.Enemy)
		{
			int targetIndex = Random.Range(0, remainingPlayerUnits.Length);
			currentActUnitTarget = remainingPlayerUnits[targetIndex];
			currentActUnit.GetComponent<UnitStatus>().SetSkill("S_001_001");
			RunToTarget();
		}
		else if (currentActUnit.tag == Const.Player)
		{
			isWaitForPlayerToChooseSkill = true;
		}
	}

	/// <summary>
	/// 跑向目标
	/// </summary>
	void RunToTarget()
	{
		// 记录当前单位的初始位置与方向
		currentActUnitIniticalPostion = currentActUnit.transform.position;
		currentActUnitInitialRotation = currentActUnit.transform.rotation;
		// 目标位置
		currentActUnitTargetPostion = currentActUnitTarget.transform.position;
		// 开启向目标移动
		isUnitRunningToTarget = true;
	}

	/// <summary>
	/// 操作面板
	/// </summary>
	void OperatePanel()
	{
		// 检查是否轮到玩家攻击
		if (isWaitForPlayerToChooseSkill)
		{
			// 判断上次技能是否为多回合技能
			SkillStatus lastSkill = GetSkillFromDict();
			if (lastSkill != null && lastSkill.turnCount > 0)
			//if (lastActUnitSkill != null && lastActUnitSkill.turnCount > 0)
			{
				// 延续上回合的技能
				PlayerSkillChoose(null);
				//currentActUnitTarget = lastActUnitTarget;
				currentActUnitTarget = GetTargetFromDict();
				Debug.Log("选择：" + currentActUnitTarget.name);
				// 关闭敌人的碰撞
				remainingEnemyUnits.ToList().ForEach(p =>
				{
					p.GetComponent<BoxCollider>().enabled = false;
				});
				RunToTarget();
			}
			else
			{
				// 打开操作面板，进行玩家选择
				menuPanel.OpenTargetPanel(Const.Panel_000);
			}
			// 启用敌人的碰撞
			remainingEnemyUnits.ToList().ForEach(p =>
			{
				p.GetComponent<BoxCollider>().enabled = true;
			});
		}
		else
		{
			// 关闭面板
			menuPanel.CloseAllPanel();
			menuPanel.isChoosed = false;
		}
	}

	private GameObject GetTargetFromDict()
	{
		List<object> targetList = null;
		GameObject lastTarget = null;
		battleTempDict.TryGetValue(currentActUnit, out targetList);
		if (targetList != null)
		{
			lastTarget = targetList[0] as GameObject;
		}

		return lastTarget;
	}

	private SkillStatus GetSkillFromDict()
	{
		List<object> targetList = null;
		SkillStatus lastSkill = null;
		battleTempDict.TryGetValue(currentActUnit, out targetList);
		if (targetList != null)
		{
			lastSkill = targetList[1] as SkillStatus;
		}

		return lastSkill;
	}

	/// <summary>
	/// 玩家技能选择
	/// </summary>
	public void PlayerSkillChoose(string skillId)
	{
		UnitStatus currentActUnitStatus = currentActUnit.GetComponent<UnitStatus>();
		SkillStatus lastSkill = GetSkillFromDict();
		// 如果是回合技能
		//if (lastActUnitSkill != null && lastActUnitSkill.turnCount > 0)
		if (lastSkill != null && lastSkill.turnCount > 0)
		{
			//currentActUnitStatus.SetSkill(lastActUnitSkill);
			currentActUnitStatus.SetSkill(lastSkill);
		}
		else
		{
			// 赋值技能
			currentActUnitStatus.SetSkill(skillId);
		}
		isWaitForPlayerToChooseSkill = false;
		isWaitForPlayerToChooseTarget = true;
		// 选择目标
		Debug.Log("请选择目标...");
	}

	/// <summary>
	/// 当前行动单位执行攻击
	/// </summary>
	public void LanchAttack()
	{
		// 存储攻击者和受攻击者的属性组件
		UnitStatus attackOwner = currentActUnit.GetComponent<UnitStatus>();
		UnitStatus attackReceiver = currentActUnitTarget.GetComponent<UnitStatus>();
		// 计算伤害
		attackValue = (int) ((attackOwner.skillStatus.damage - attackReceiver.defence + Random.Range(-2, 2))
			* attackDamageMultiplier);
		if (attackValue < 0) attackValue = 0;
		// 攻击
		attackOwner.Attack();
		// 被攻击方受伤
		attackReceiver.Hurt(attackValue);
		if (currentActUnit.tag == Const.Player)
		{
			// 保存该次选择的目标
			//lastActUnitTarget = currentActUnitTarget;
			// 保存该次技能对象
			//lastActUnitSkill = attackOwner.GetSkill();

			object[] temArr = { currentActUnitTarget, attackOwner.GetSkill() };
			// 不存在则添加，存在则更新
			if (!battleTempDict.ContainsKey(currentActUnit))
			{
				battleTempDict.Add(currentActUnit, new List<object>(temArr));
			}
			else
			{
				battleTempDict[currentActUnit] = new List<object>(temArr);
			}
		}
		// 等待时间
		StartCoroutine("WaitForTargetAct");
	}

	IEnumerator WaitForTargetAct()
	{
		yield return new WaitForSeconds(1f);
		// 攻击结束后返回
		isUnitRunningBack = true;
		// 归位
		currentActUnit.transform.position = currentActUnitInitPos;
	}
}
