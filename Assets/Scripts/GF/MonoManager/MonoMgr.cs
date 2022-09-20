/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：MonoMgr
 // 创建日期：2022/4/19 20:30:32
 // 功能描述：
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace GF
{
    public class MonoMgr : Singleton<MonoMgr>
    {

        private MonoController controller;

        public MonoMgr()
        {
            //保证了MonoController对象的唯一性
            GameObject obj = new GameObject("MonoController");
            controller = obj.AddComponent<MonoController>();
        }

        /// <summary>
        /// 给外部提供的 添加帧更新事件的函数
        /// </summary>
        /// <param name="fun"></param>
        public void AddUpdateListener(UnityAction fun)
        {
            controller.AddUpdateListener(fun);
        }

        /// <summary>
        /// 提供给外部 用于移除帧更新事件函数
        /// </summary>
        /// <param name="fun"></param>
        public void RemoveUpdateListener(UnityAction fun)
        {
            controller.RemoveUpdateListener(fun);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return controller.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return controller.StartCoroutine(methodName, value);
        }

        public Coroutine StartCoroutine(string methodName)
        {
            return controller.StartCoroutine(methodName);
        }
    }
}

