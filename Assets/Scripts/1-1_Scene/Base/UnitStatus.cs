using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色单位
/// </summary>
public class UnitStatus : MonoBehaviour {

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
	// 死亡与否
	private bool dead = false;
	// 标识是否死亡
	public bool IsDead
	{
		get { return dead; }
	}

    protected Animator animator;

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
		if (health <= 0)
		{
			dead = true;
			gameObject.tag = "DeadUnit";
		}
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// 受到攻击
	/// </summary>
	/// <param name="damage">伤害值</param>
	public virtual void ReceiveDamage(int damage)
	{
		health -= damage;
		healthPercent = health * 1f / initialHealth;
		Debug.Log(gameObject.name + "掉血" + damage + "点，剩余生命值" + health);
	}


	
}
