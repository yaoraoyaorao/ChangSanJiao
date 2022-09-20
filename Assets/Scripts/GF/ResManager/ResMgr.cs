/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：ResMgr
 // 创建日期：2022/4/19 20:28:22
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
    public class ResMgr : Singleton<ResMgr>
    {
        //同步加载资源
        public T Load<T>(string name) where T : Object
        {
            T res = Resources.Load<T>(name);
            //如果对象是一个GameObject类型的 我把他实例化后 再返回出去 外部 直接使用即可
            if (res is GameObject)
                return GameObject.Instantiate(res);
            else//TextAsset AudioClip
                return res;
        }


        //异步加载资源
        public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
            //开启异步加载的协程
            MonoMgr.Instance.StartCoroutine(ReallyLoadAsync(name, callback));
        }

        //真正的协同程序函数  用于 开启异步加载对应的资源
        private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
            ResourceRequest r = Resources.LoadAsync<T>(name);
            yield return r;

            if (r.asset is GameObject)
                callback(GameObject.Instantiate(r.asset) as T);
            else
                callback(r.asset as T);
        }
    }
}

