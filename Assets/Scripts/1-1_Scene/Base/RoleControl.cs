using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleControl : MonoBehaviour {

	public int hp = 100;
	public int attackDamage = 10;
	public int skillDamage = 20;
	public int defence = 2;
	public float waitTime = 1f;

	protected Animator animator;

	// Use this for initialization
	void Start()
	{
		
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
		Debug.Log("Receive damage " + damage);
		hp -= damage;
		DeadCheck();
	}



	/// <summary>
	/// 移动
	/// </summary>
	protected virtual void AttackMove()
	{
		if (animator != null)
		{
			animator.SetTrigger("Attack");
		}
	}

	protected virtual void DeadCheck()
	{
		if (hp <= 0)
		{
			hp = 0;
			if (animator != null)
			{
				animator.SetTrigger("Dead");
			}
		}
	}

	
}
