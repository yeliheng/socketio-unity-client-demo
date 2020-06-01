using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 2020-05-29
 * 2D玩家控制器
 * Written By Yeliheng
 */

public class PlayerController : MonoBehaviour {
	public float speed;
	private Rigidbody2D rb2d;
	private float x, y;
	private PlayerMoveSync moveSync;
	private bool isLocalPlayer = false;
	private Renderer player;
	private bl_Joystick joystick;
	private float moveHorizontal;
	private float moveVertical;
	//private string username;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		joystick = FindObjectOfType<bl_Joystick>();
		moveSync = GetComponent<PlayerMoveSync>();
		//username = GetComponentInChildren<TextMesh>().text;
		player = GetComponent<Renderer>();
		if(player.material.color == Color.green)
			isLocalPlayer = true;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isLocalPlayer)
		{
			//float moveX;
			 moveHorizontal = joystick.Horizontal;
			 moveVertical = joystick.Vertical;
			//Debug.Log("x: " + moveHorizontal + " y: " + moveVertical);
			//Vector2 movement = new Vector2(moveHorizontal, moveVertical);
			//rb2d.AddForce(movement * speed);
			//float moveX += moveHorizontal * speed;

			x = transform.position.x;
			y = transform.position.y;
			rb2d.DOMove(new Vector2(x + moveHorizontal * speed, y + moveVertical * speed),3);

		}
		if (moveHorizontal != 0 || moveVertical != 0)
		{
			moveSync.Sync(x, y);
		}


	}
}
