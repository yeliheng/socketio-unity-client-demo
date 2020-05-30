using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * 2020-05-26
 * 负责文本显示、滚动以及向服务器发送文本的逻辑
 * Written By Yeliheng
 */
public class TextScollController : MonoBehaviour
{

    public InputField chatInput;
    public Text chatText;
    public ScrollRect scrollRect;
    public Text userText;
    public Button connectButton;
    public SocketIOComponent socket;
    // Use this for initialization
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            sendMessageToServer();
        }

        if(userText.text == "")
        {
            connectButton.gameObject.SetActive(false);
        }
        else
        {
            connectButton.gameObject.SetActive(true);
        }
    }
    public void addText(string addText)
    {
        chatText.text += addText + "\n";
        chatInput.text = "";
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    public void sendMessageToServer()
    {
        string username = userText.text;
        if (username != "")
        {
            if (chatInput.text != "")
            {
                string addText = "\n  " + "<color=yellow>" + username + ": </color>" + chatInput.text;
                string msg = "<color=yellow>" + username + ": </color>" + chatInput.text;
                Dictionary<string, string> data = new Dictionary<string, string>();
                data["msg"] = msg;
                socket.Emit("ClientSendMessage", new JSONObject(data), ClientSendMessageCallback);
                //Debug.Log("发送量 ");
                chatText.text += addText;
                chatInput.text = "";
                chatInput.ActivateInputField();
                Canvas.ForceUpdateCanvases();
                scrollRect.verticalNormalizedPosition = 0f;
                Canvas.ForceUpdateCanvases();
            }
        }
        else
        {
            string addText = "\n <color=red>请先设置昵称!</color>";
            chatText.text += addText;
            chatInput.text = "";
            chatInput.ActivateInputField();
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
    }

    public void ClientSendMessageCallback(JSONObject json)
    {

    }

}