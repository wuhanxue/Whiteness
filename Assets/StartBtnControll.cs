﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtnControll : MonoBehaviour {

    /// <summary>
    /// 开始游戏按钮触发事件
    /// </summary>
    public void OnStartBtnClick()
    {
        SceneManager.LoadScene(1);
    }

}
