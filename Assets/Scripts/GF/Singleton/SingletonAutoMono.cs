/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：SingletonAutoMono
 // 创建日期：2022/4/11 21:20:25
 // 功能描述：
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GF
{
    public class SingletonAutoMono<T> : MonoBehaviour where T:MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).ToString();
                    DontDestroyOnLoad(obj);
                    _instance = obj.AddComponent<T>();
                }

                return _instance;
            }
        }

    }
}
