using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
* 2020-05-30
* 简易摇杆
* Written By Yeliheng
*/
public class Joystick : ScrollRect {

	// Use this for initialization
	float radius = 0f;
	public Vector2 stickPos;

	protected override void Start()
	{
		radius = viewport.rect.width / 2;
		//stickPos = new Vector2(0, 0);
	}

	private void Update()
	{
		if(content.localPosition.magnitude > radius)
		{
			content.localPosition = content.localPosition.normalized * radius;
		}
		stickPos = content.localPosition.normalized;
	}
}
