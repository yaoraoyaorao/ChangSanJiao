/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：SceneManager
 // 创建日期：2022/4/18 14:40:21
 // 功能描述：场景管理
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GF
{
    public class SceneController
    {
        private IScene currentScene;
        private bool firstBeginScene;

        /// <summary>
        /// 设置场景
        /// </summary>
        /// <param name="scene"></param>
        public void SetScene(IScene scene, bool isLoad,UnityAction action = null)
        {
            if (scene == null) return;

            if (currentScene != null)
            {
                currentScene.End();
            }

            currentScene = scene;

            firstBeginScene = true;

            if (isLoad)
            {
                SceneMgr.Instance.LoadSceneAsyn(scene.sceneName, action);
            }
            else
            {
                currentScene.Begin();
                firstBeginScene = false;
            }
        }

        /// <summary>
        /// 场景更新
        /// </summary>
        public void UpdateScnene()
        {
            if (currentScene == null) return;
            if (firstBeginScene)
            {
                currentScene.Begin();
                firstBeginScene = false;
            }
            currentScene.Update();
        }
    }
}
