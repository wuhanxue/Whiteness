using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {

    public float smoothing = 20;  // 移动速度
	public float restTime = 0.15f;  // 休息时间
	public float restTimer = 0;


	private Vector2 targetPos = new Vector2(1,1);  // 目标位置
    private Rigidbody2D rigidbody;
	private Animator animator;
	// Use this for initialization

	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		restTimer += Time.deltaTime;
		if (restTimer < restTime) return;
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		animator.SetInteger("direction", -1);
		if (h > 0){
          v = 0;
		}
		if (h != 0 || v != 0)  // 发生了移动
		{
			targetPos = new Vector2(h, v);
			rigidbody.MovePosition(Vector2.Lerp(transform.position, targetPos, smoothing * Time.deltaTime));
			if (h < 0)
			{
			    animator.SetInteger("direction", 0);
			}
			if (h > 0)
			{
				animator.SetInteger("direction", 2);
			}
			if (v < 0)
			{
				animator.SetInteger("direction", 1);
			}
			if (v > 0)
			{
				animator.SetInteger("direction", 3);
			}
			restTimer = 0;

			

		}
	}
}
