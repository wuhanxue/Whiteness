using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : RoleControl 
{
	public GameObject skill_1;
	public GameObject skill_2;

	private EnemyControl enemyControl;

	// Use this for initialization
	void Start () {
		enemyControl = GameObject.FindGameObjectWithTag(Const.Player).GetComponent<EnemyControl>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void CommonAttack()
	{
		base.CommonAttack();
		GameObject skillGo = Instantiate(skill_1, enemyControl.transform.position, Quaternion.identity);
		Destroy(skillGo, 0.5f);
	}

	public override void SkillAttack()
	{
		base.SkillAttack();
		GameObject skillGo = Instantiate(skill_2, enemyControl.transform.position, Quaternion.identity);
		Destroy(skillGo, 0.5f);
	}
}
