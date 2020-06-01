using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/*
* 2020-05-29
* 玩家移动与服务端实时同步
* Written By Yeliheng
*/
public class PlayerMoveSync : MonoBehaviour {
	private SocketIOComponent socket;
	private string username;
	
	// Use this for initialization
	void Start() {
		socket = FindObjectOfType<SocketIOComponent>();
		username = GetComponentInChildren<TextMesh>().text;
		//注册移动接收事件
		socket.On("MovementReceiver", MovementReceiver);
	}

	private void MovementReceiver(SocketIOEvent e)
	{
		string otherPlayerName = e.data["username"].ToString().Replace("\"","");
		float x = float.Parse(e.data["x"].ToString().Replace("\"", ""));
		float y = float.Parse(e.data["y"].ToString().Replace("\"", ""));
		GameObject playerObj = GameObject.Find(otherPlayerName);
		//playerObj.GetComponent<Rigidbody2D>().MovePosition(new Vector2(x,y));
		playerObj.transform.DOMove(new Vector2(x,y),3);
	}

	// Update is called once per frame
	void Update () {
		//socket.e
	}

	public void Sync(float x,float y)
	{
		//socket.Emit();
		Dictionary<string, string> data = new Dictionary<string, string>();
		data["username"] = username;
		data["x"] = x.ToString();
		data["y"] = y.ToString();
		//Debug.Log("username:" + username +"x: " + x + " Y: " + y);
		socket.Emit("MoveSync", new JSONObject(data), null);
	}


}
