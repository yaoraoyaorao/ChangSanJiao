/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：EventManager
 // 创建日期：2022/4/13 9:30:59
 // 功能描述：事件中心
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GF
{
    public class IEventInfo
    { }

    public class EventAction<T>:IEventInfo
    {
        public EventAction(UnityAction<T> a)
        {
            action += a;
        }
        public UnityAction<T> action;
    }

    public class EventAction:IEventInfo
    {
        public EventAction(UnityAction a)
        {
            action += a;
        }
        public UnityAction action;
    }

    public class EventManager : Singleton<EventManager>
    {
        private Dictionary<string, IEventInfo> eventPool = new Dictionary<string, IEventInfo>();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        public void RegisterEvent<T>(string eventName, UnityAction<T> action)
        {
            if (eventPool.ContainsKey(eventName))
            {
                (eventPool[eventName] as EventAction<T>).action += action;
            }
            else
            {
                eventPool.Add(eventName, new EventAction<T>(action));
            }
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        public void RegisterEvent(string eventName, UnityAction action)
        {
            if (eventPool.ContainsKey(eventName))
            {
                (eventPool[eventName] as EventAction).action += action;
            }
            else
            {
                eventPool.Add(eventName, new EventAction(action));
            }
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <param name="eventName"></param>
        public void UnRegisterEvent<T>(string eventName,UnityAction<T> action)
        {
            if (eventPool.ContainsKey(eventName))
            {
                (eventPool[eventName] as EventAction<T>).action -= action;
            }
            else
            {
                Debug.LogError("注销失败，事件不存在 eventName:" + eventName);
                return;
            }
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <param name="eventName"></param>
        public void UnRegisterEvent(string eventName, UnityAction action)
        {
            if (eventPool.ContainsKey(eventName))
            {
                (eventPool[eventName] as EventAction).action -= action;
            }
            else
            {
                Debug.LogError("注销失败，事件不存在 eventName:" + eventName);
                return;
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="obj"></param>
        public void EventTrigger<T>(string eventName,T obj)
        {

            if (eventPool.ContainsKey(eventName))
            {
                //eventDic[name]();
                if ((eventPool[eventName] as EventAction<T>).action != null)
                    (eventPool[eventName] as EventAction<T>).action.Invoke(obj);
                //eventDic[name].Invoke(info);
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="obj"></param>
        public void EventTrigger(string eventName)
        {

            if (eventPool.ContainsKey(eventName))
            {
                //eventDic[name]();
                if ((eventPool[eventName] as EventAction).action != null)
                    (eventPool[eventName] as EventAction).action.Invoke();
                //eventDic[name].Invoke(info);
            }
        }

        /// <summary>
        /// 清空事件
        /// </summary>
        public void Clear()
        {
            eventPool.Clear();
        }
    }

}
