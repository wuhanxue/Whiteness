using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class    PlayerMove : MonoBehaviour {

    public float speed = 2;  // 移动速度
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
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0)
        {
            v = 0;
        }

        transform.position += new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);
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
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetInteger("direction", -1);
        }




    
	}
}
