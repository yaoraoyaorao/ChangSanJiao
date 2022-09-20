/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：Singleton
 // 创建日期：2022/4/11 21:11:23
 // 功能描述：单例模式
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GF
{
    public class Singleton<T> where T : new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();

                return _instance;
            }
        }

    }
}
