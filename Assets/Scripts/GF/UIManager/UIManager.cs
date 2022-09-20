/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：UIManager
 // 创建日期：2022/4/11 21:09:27
 // 功能描述：ui管理器，是ui框架的核心，主要负责ui窗体的加载，缓存，以及UI窗体基类的各种声明周期的操作
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GF.UI
{
    /// <summary>
    /// UI层级
    /// </summary>
    public enum E_UI_Layer
    {
        Bot,
        Mid,
        Top,
        System,
    }
    public class UIManager : Singleton<UIManager>
    {
        public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

        private Transform bot;
        private Transform mid;
        private Transform top;
        private Transform system;

        //记录我们UI的Canvas父对象 方便以后外部可能会使用它
        public RectTransform canvas;

        public UIManager()
        {
            //创建Canvas 让其过场景的时候 不被移除
            GameObject obj = ResMgr.Instance.Load<GameObject>(SysDefine.SYS_PATH_CANVAS);
            canvas = obj.transform as RectTransform;
            GameObject.DontDestroyOnLoad(obj);

            //找到各层
            bot = canvas.Find("Bot");
            mid = canvas.Find("Mid");
            top = canvas.Find("Top");
            system = canvas.Find("System");
        }

        /// <summary>
        /// 通过层级枚举 得到对应层级的父对象
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Transform GetLayerFather(E_UI_Layer layer)
        {
            switch (layer)
            {
                case E_UI_Layer.Bot:
                    return this.bot;
                case E_UI_Layer.Mid:
                    return this.mid;
                case E_UI_Layer.Top:
                    return this.top;
                case E_UI_Layer.System:
                    return this.system;
            }
            return null;
        }

        /// <summary>
        /// 显示面板
        /// </summary>
        /// <typeparam name="T">面板脚本类型</typeparam>
        /// <param name="panelName">面板名</param>
        /// <param name="layer">显示在哪一层</param>
        /// <param name="callBack">当面板预设体创建成功后 你想做的事</param>
        public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callBack = null) where T : BasePanel
        {
            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].ShowMe();
                // 处理面板创建完成后的逻辑
                if (callBack != null)
                    callBack(panelDic[panelName] as T);
                //避免面板重复加载 如果存在该面板 即直接显示 调用回调函数后  直接return 不再处理后面的异步加载逻辑
                return;
            }

            ResMgr.Instance.LoadAsync<GameObject>(SysDefine.SYS_PATH_UIPREFAB + panelName, (obj) =>
            {
                //把他作为 Canvas的子对象
                //并且 要设置它的相对位置
                //找到父对象 你到底显示在哪一层
                Transform father = bot;
                switch (layer)
                {
                    case E_UI_Layer.Mid:
                        father = mid;
                        break;
                    case E_UI_Layer.Top:
                        father = top;
                        break;
                    case E_UI_Layer.System:
                        father = system;
                        break;
                }
                //设置父对象  设置相对位置和大小
                obj.transform.SetParent(father);

                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                (obj.transform as RectTransform).offsetMax = Vector2.zero;
                (obj.transform as RectTransform).offsetMin = Vector2.zero;

                //得到预设体身上的面板脚本
                T panel = obj.GetComponent<T>();
                // 处理面板创建完成后的逻辑
                if (callBack != null)
                    callBack(panel);

                panel.ShowMe();

                //把面板存起来
                panelDic.Add(panelName, panel);
            });
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="panelName"></param>
        public void HidePanel(string panelName)
        {
            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].HideMe();
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }

        /// <summary>
        /// 得到某一个已经显示的面板 方便外部使用
        /// </summary>
        public T GetPanel<T>(string name) where T : BasePanel
        {
            if (panelDic.ContainsKey(name))
                return panelDic[name] as T;
            return null;
        }
    }




}