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
	//private string username;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		moveSync = GetComponent<PlayerMoveSync>();
		//username = GetComponentInChildren<TextMesh>().text;
		player = GetComponent<Renderer>();
		if(player.material.color == Color.green)
			isLocalPlayer = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer)
		{
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			Vector2 movement = new Vector2(moveHorizontal, moveVertical);
			rb2d.AddForce(movement * speed);
			x = transform.position.x;
			y = transform.position.y;

		}
		if (rb2d.velocity.x != 0 || rb2d.velocity.y != 0)
		{
			moveSync.Sync(x, y);
		}


	}
}
