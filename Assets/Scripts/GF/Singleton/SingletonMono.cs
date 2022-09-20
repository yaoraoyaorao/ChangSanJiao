/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：SingletonMono
 // 创建日期：2022/4/11 21:17:56
 // 功能描述：
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GF
{
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance == null)
                _instance = this as T;

        }
    }
}
