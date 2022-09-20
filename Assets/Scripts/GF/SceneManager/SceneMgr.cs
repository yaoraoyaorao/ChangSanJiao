/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：SceneMgr
 // 创建日期：2022/4/19 22:55:44
 // 功能描述：
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
    public class SceneMgr : Singleton<SceneMgr>
    {
        /// <summary>
        /// 切换场景 同步
        /// </summary>
        /// <param name="name"></param>
        public void LoadScene(string name, UnityAction fun)
        {
            //场景同步加载
            SceneManager.LoadScene(name);
            //加载完成过后 才会去执行fun
            fun();
        }

        /// <summary>
        /// 提供给外部的 异步加载的接口方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fun"></param>
        public void LoadSceneAsyn(string name, UnityAction fun)
        {
            MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsyn(name, fun));
        }

        /// <summary>
        /// 协程异步加载场景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction fun)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            //可以得到场景加载的一个进度
            while (!ao.isDone)
            {
                //事件中心 向外分发 进度情况  外面想用就用
                EventManager.Instance.EventTrigger<float>("Loading", ao.progress);
                //这里面去更新进度条
                yield return ao.progress;
            }
            //加载完成过后 才会去执行fun
            if (fun!=null)
            {
                fun();
            }
        }
    }

}

