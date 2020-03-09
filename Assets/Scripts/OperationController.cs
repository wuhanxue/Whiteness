using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationController : MonoBehaviour {

	public GameObject skill_1;
	public GameObject skill_2;
	private GameObject player;
	private GameObject enemy;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		enemy = GameObject.FindGameObjectWithTag("Enemy");
	}

	public void OnAtkBtnClick()
	{
		player.GetComponent<Animation>().Play("player_move");
		GameObject skillGo = Instantiate(skill_1, enemy.transform.position, Quaternion.identity);
		Destroy(skillGo, 0.5f);
	}

	public void OnSklBtnClick()
	{
		player.GetComponent<Animation>().Play("player_move");
		GameObject skillGo = Instantiate(skill_2, enemy.transform.position, Quaternion.identity);
		Destroy(skillGo, 0.5f);
	}

	public void OnDefBtnClick()
	{

	}
}
