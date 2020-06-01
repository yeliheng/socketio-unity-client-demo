# socketio-unity-client-demo
基于socketio的unity客户端demo

## 已完成功能 ##

1.连接服务器

2.玩家文字聊天

3.玩家语音聊天(目前使用Agora,将改进为自主RTC)

4.玩家出生/移动

## 如何使用? ##

1. Clone项目，用unity2017打开

2.前往Agora申请App Id

3.在Scripts文件夹下 配置Key.cs 内容如下:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Key {

    public static string GetKey()
    {
        return "Agora的App Id";
    }
}

```
