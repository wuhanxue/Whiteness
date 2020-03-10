using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleControl : MonoBehaviour {

	public int hp = 100;
	public int attackDamage = 10;
	public int skillDamage = 20;
	public int defence = 2;

	private Animator animator;

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// 普通攻击
	/// </summary>
	public virtual void CommonAttack()
	{
		AttackMove();
	}

	/// <summary>
	/// 技能攻击
	/// </summary>
	public virtual void SkillAttack()
	{
		AttackMove();
	}

	/// <summary>
	/// 防御
	/// </summary>
	public virtual void Defend()
	{
		//TODO
	}

	/// <summary>
	/// 受到攻击
	/// </summary>
	/// <param name="damage">伤害值</param>
	public virtual void ReceiveDamage(int damage)
	{
		hp -= damage;
		DeadCheck();
	}

	/// <summary>
	/// 移动
	/// </summary>
	protected virtual void AttackMove()
	{
		animator.SetTrigger("Attack");
	}

	protected virtual void DeadCheck()
	{
		if (hp <= 0)
		{
			hp = 0;
			animator.SetTrigger("Dead");
		}
	}
}
