/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：MonoController
 // 创建日期：2022/4/19 20:32:31
 // 功能描述：
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GF
{
    public class MonoController : MonoBehaviour
    {
        private UnityAction updateEvent;
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        public void AddUpdateListener(UnityAction fun)
        {
            updateEvent += fun;
        }

        public void RemoveUpdateListener(UnityAction fun)
        {
            updateEvent -= fun;
        }
    }
}

