using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal.Input;

/*
 * 2020-05-27
 * 整合了一些socket事件
 * Written By Yeliheng
 */
namespace Tool
{

    public class SocketManager : MonoBehaviour
    {

        SocketIOComponent m_socket;
        public TextScollController textScollController;
        public Button connectButton;
        public Text userText;
        public GameObject playerPrefab;
        private TextMesh playerName;
        void Start()
        {  
            m_socket = GetComponent<SocketIOComponent>();
            connectButton.GetComponent<Button>().onClick.AddListener(OnPlayerJoin);
            if (m_socket != null)
            {

                //系统的事件
              //  m_socket.On("open", OnSocketOpen);
              //  m_socket.On("");
                m_socket.On("error", OnSocketError);
                m_socket.On("close", OnSocketClose);
                //自定义的事件
                m_socket.On("PlayerJoinBroadcast", PlayerJoinBroadcast);
                m_socket.On("PlayerLeftBroadcast", PlayerLeftBroadcast);
                m_socket.On("ServerMessageReceiver", ServerMessageReceiver);
                m_socket.On("OnlinePlayers", OnlinePlayers);
                //Invoke("OnPlayerJoin", 0.5f);

            }
        }

        public void OnPlayerJoin()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
             data["name"] = userText.text;
            m_socket.Emit("PlayerJoin", new JSONObject(data), PlayerJoin);

            //断开连接,会触发close事件
            //socket.Close();
        }
        #region 注册的事件

        public void PlayerJoinBroadcast(SocketIOEvent e)
        {
            string message = ("\n" + e.data["msg"].ToString()).Replace("\"", "");
            Debug.Log(e.data.ToString()) ;
            textScollController.addText(message);
            string name = e.data["playerName"].ToString().Replace("\"", "");
            GameObject player = Instantiate(playerPrefab);
            player.name = name;
            //创建一个新玩家
            //云玩家
            if (e.data["playerName"].ToString().Replace("\"", "") != userText.text) { 
                
                if (player != null)
                {
                    Renderer renderer = player.GetComponent<Renderer>();
                    renderer.material.color = Color.blue;
                    playerName = player.GetComponentInChildren<TextMesh>();
                    playerName.text = name;
                }
            }
            //本地玩家
            else
            {        
                if (player != null)
                {
                    Renderer renderer = player.GetComponent<Renderer>();
                    renderer.material.color = Color.green;
                    playerName = player.GetComponentInChildren<TextMesh>();
                    playerName.text = "";
                    playerName.text = userText.text;
                }
            }

            //这里还需要考虑到之前加入的玩家没有被添加
            //见addPlayer()函数
            
            
        }

        public void PlayerLeftBroadcast(SocketIOEvent e)
        {
            string name = e.data["playerName"].ToString().Replace("\"", "");
            GameObject leftPlayer = GameObject.Find(name);
            if (leftPlayer != null)
            {
                Destroy(leftPlayer);
            }
        }

        #endregion

        public void PlayerJoin(JSONObject json)
        {
            // Debug.Log(string.Format("OnServerListenerCallback data: {0}", json));
            Debug.Log(json["msg"].ToString());
           // textScollController.addText(json["msg"].ToString());
        }

        public void ServerMessageReceiver(SocketIOEvent e)
        {
            string message = "\n" + e.data["msg"].ToString().Replace("\"", "");
            Debug.Log(message);
            textScollController.addText(message);
        }

        public void OnlinePlayers(SocketIOEvent e)
        {
           // Debug.Log(e.data);
            //此处将遍历出除自己外的所有玩家
            for(int i = 0; i < e.data["list"].Count; i++)
            {
                //Debug.Log(e.data["list"][i]);
                string name = e.data["list"][i].ToString().Replace("\"", "");
                GameObject player = Instantiate(playerPrefab);
                player.name = name;
                Renderer renderer = player.GetComponent<Renderer>();
                renderer.material.color = Color.blue;
                playerName = player.GetComponentInChildren<TextMesh>();
                playerName.text = name;
            }
        }


        public void OnSocketError(SocketIOEvent e)
        {
            textScollController.addText("<color=red>服务器连接失败!正在重试...</color>");
            Debug.Log("<color=red>服务器连接失败!正在重试...</color>");
        }

        public void OnSocketClose(SocketIOEvent e)
        {
            Debug.Log("OnSocketClose: " + e.name + " " + e.data);

        }


        public void OnSocketOpen(SocketIOEvent ev)
        {

        }

    }
}