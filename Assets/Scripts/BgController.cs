using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 2020-05-28
 * 背景铺满屏幕
 * Written By Yeliheng
 */
public class BgController : MonoBehaviour {

    public SpriteRenderer m_AnimatorSprite;
    public GameObject _objanimator;
    // Use this for initialization
    void Start () {
        MatchingAnimatorToCamera(_objanimator);
	}
    /// <summary>
    /// 使sprite铺满整个屏幕
    /// </summary>
    private void MatchingAnimatorToCamera(GameObject _objanimator)
    {
        if (_objanimator != null)
        {
            m_AnimatorSprite = _objanimator.GetComponent<SpriteRenderer>();
        }

        Vector3 scale = _objanimator.transform.localScale;
        float cameraheight = Camera.main.orthographicSize * 2;
        float camerawidth = cameraheight * Camera.main.aspect;

        if (cameraheight >= camerawidth)
        {
            scale *= cameraheight / m_AnimatorSprite.bounds.size.y;
        }
        else
        {
            scale *= camerawidth / m_AnimatorSprite.bounds.size.x;
        }
        _objanimator.transform.localScale = scale;
        _objanimator.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, _objanimator.transform.position.z);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
