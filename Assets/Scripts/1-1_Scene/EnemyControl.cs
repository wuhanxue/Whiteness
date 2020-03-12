using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : RoleControl
{

	public GameObject skill_1;
	public GameObject skill_2;

	private PlayerControl playerControl;
	private OperationControl operationControl;

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		playerControl = GameObject.FindGameObjectWithTag(Const.Player).GetComponent<PlayerControl>();
		operationControl = GameObject.Find("OperationPanel").GetComponent<OperationControl>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void CommonAttack()
	{
		base.CommonAttack();
		Debug.Log("Enemy attack");
		GameObject skillGo = Instantiate(skill_1, playerControl.transform.position, Quaternion.identity);
		Destroy(skillGo, 0.5f);
	}

	
}
