using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : RoleControl 
{
	public GameObject skill_1;
	public GameObject skill_2;

	private EnemyControl enemyControl;
	private OperationControl operationControl; 

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		enemyControl = GameObject.FindGameObjectWithTag(Const.Enemy).GetComponent<EnemyControl>();
		operationControl = GameObject.Find("OperationPanel").GetComponent<OperationControl>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void CommonAttack()
	{
		base.CommonAttack();
		Debug.Log("Player attack");
		GameObject skillGo = Instantiate(skill_1, enemyControl.transform.position, Quaternion.identity);
		Destroy(skillGo, 0.5f);
		StartCoroutine("WaitTime");
		operationControl.isPlayerAction = false;
	}

	public override void SkillAttack()
	{
		base.SkillAttack();
		Debug.Log("Player attack");
		GameObject skillGo = Instantiate(skill_2, enemyControl.transform.position, Quaternion.identity);
		Destroy(skillGo, 0.5f);
		StartCoroutine("WaitTime");
		operationControl.isPlayerAction = false;
	}

	IEnumerator WaitTime()
	{
		yield return new WaitForSeconds(waitTime);
	}
}
