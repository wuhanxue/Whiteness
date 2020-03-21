using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色单位
/// </summary>
public class UnitStatus : MonoBehaviour {

	public string unitId = "U001";
    // 血量
    public int health = 100;
	// 能量
	public int energy = 100;
	// 攻击力
	public int attack = 10;
	// 技能攻击力
	public int skill = 20;
	// 防御力
	public int defence = 2;
	// 速度
	public int speed = 1;
	// 初始血量
	public int initialHealth = 100;
	// 初始能量
	public int initialEnergy = 100;
	// 血量比
	public float healthPercent;
	// 能量比
	public float energyPercent;
	// 出手回合
	public float attackTurn;
	// 技能号
	public SkillStatus skillStatus;
	public GameObject damageInfo;
	// 死亡与否
	private bool dead = false;
	private GameObject uiRoot;
	// 标识是否死亡
	public bool IsDead
	{
		get { return dead; }
	}

    private Animator animator;

	// Use this for initialization
	void Start()
	{
		// 血量设置
		health = initialHealth;
		healthPercent = health * 1f / initialHealth;
		// 能量设置
		energy = initialEnergy;
		energyPercent = energy * 1f / initialEnergy;
		// 先后手
		attackTurn = speed * 1f / 100;

		animator = GetComponent<Animator>();
		uiRoot = GameObject.Find("UI Root");
	}

	// Update is called once per frame
	void Update()
	{
		if (health <= 0)
		{
			dead = true;
			gameObject.tag = "DeadUnit";
		}
	}

	public void SetSkill(string skillId)
	{
		skillStatus.skillId = skillId;
	}
	

	public void Attack()
	{
		StartCoroutine("WaitForAttack" + skillStatus.skillId);
	}

	IEnumerator WaitForAttack1()
	{
		// 技能1：火焰dot伤害3回合
		Debug.Log("选择技能1：火焰dot伤害3回合");
		// 播放动画
		animator.SetTrigger("Attack1");
		yield return new WaitForSeconds(0.75f);
		animator.ResetTrigger("Attack1");
		animator.SetTrigger("Idle");
	}

	IEnumerator WaitForAttack2()
	{
		// 技能2：即死技能
		Debug.Log("选择技能2：即死技能");
		// 播放动画
		animator.SetTrigger("Attack2");
		yield return new WaitForSeconds(0.75f);
		animator.ResetTrigger("Attack2");
		animator.SetTrigger("Idle");
	}

	IEnumerator WaitForAttack3()
	{
		// 技能3：魔法平A
		Debug.Log("选择技能3：魔法平A");
		// 播放动画
		animator.SetTrigger("Attack3");
		yield return new WaitForSeconds(0.75f);
		animator.ResetTrigger("Attack3");
		animator.SetTrigger("Idle");
	}

	IEnumerator WaitForAttack4()
	{
		// 技能4：防御魔法
		Debug.Log("选择技能4：防御魔法");
		// 播放动画
		animator.SetTrigger("Defend");
		yield return new WaitForSeconds(0.75f);
		animator.ResetTrigger("Defend");
		animator.SetTrigger("Idle");
	}

	public void Hurt(int attackValue)
	{
		StartCoroutine("WaitForTakeDamage", attackValue);
	}

	IEnumerator WaitForTakeDamage(int attackValue)
	{
		// 被攻击者受伤
		ReceiveDamage(attackValue);
		if (!IsDead)
		{
			animator.SetTrigger("Hurt");
			yield return new WaitForSeconds(0.3f);
			animator.ResetTrigger("Hurt");
			animator.SetTrigger("Idle");
		}
		else
		{
			animator.SetTrigger("Dead");
		}
		// 停顿一秒
		yield return new WaitForSeconds(1f);
		
	}

	/// <summary>
	/// 受到攻击
	/// </summary>
	/// <param name="damage">伤害值</param>
	void ReceiveDamage(int damage)
	{
		health -= damage;
		healthPercent = health * 1f / initialHealth;
		Debug.Log(gameObject.name + "掉血" + damage + "点，剩余生命值" + health);
		// 世界坐标转屏幕坐标
		Vector3 unitPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));
		unitPos.z = 0f;
		Vector3 unitScreenPos = UICamera.currentCamera.ScreenToWorldPoint(unitPos);
		GameObject info = Instantiate(damageInfo);
		info.GetComponent<UILabel>().text = "-" + damage;
		info.transform.SetParent(uiRoot.transform, false);
		info.transform.position = unitScreenPos;
		// 销毁
		Destroy(info, 0.3f);
	}

}
