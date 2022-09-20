/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：IFsmState
 // 创建日期：2022/5/1 3:44:45
 // 功能描述：
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GF.AI.FSM
{
    public class IFsmState<T>
    {
        private Dictionary<ICondition<T>, string> _condtionDic;
        private Action<T> _enterHandle;
        private Action<T> _updateHandle;
        private Action<T> _exitHandle;

        public void BindEnterAction(Action<T> action)
        {
            _enterHandle = action;
        }
        public void BindUpdateAction(Action<T> action)
        {
            _updateHandle = action;
        }
        public void BindExitAction(Action<T> action)
        {
            _exitHandle = action;
        }

        /// <summary>
        /// 添加条件
        /// </summary>
        public void AddCondition(ICondition<T> condition,string stateName)
        {
            if (condition == null || string.IsNullOrEmpty(stateName)) 
                return;
            if (_condtionDic == null)
                _condtionDic = new Dictionary<ICondition<T>, string>();
            _condtionDic.Add(condition, stateName);
        }

        /// <summary>
        /// 移除条件
        /// </summary>
        /// <param name="condition"></param>
        public void RemoveCondition(ICondition<T> condition)
        {
            if (condition == null || _condtionDic == null) return;
            if (_condtionDic.ContainsKey(condition))
                _condtionDic.Remove(condition);
        }

        public bool CheckCondition(T owner,out string stateName)
        {
            if (_condtionDic == null)
            {
                stateName = string.Empty;
                return false;
            }
            foreach (var condition in _condtionDic.Keys)
            {
                if (condition.Trigger(owner))
                {
                    stateName = _condtionDic[condition];
                    return true;
                }
            }
            stateName = string.Empty;
            return false;
        }

        public virtual void OnEnter(T owner)
        {
            if (_enterHandle == null) return;
            _enterHandle(owner);
        }
        public virtual void OnUpdate(T owner)
        {
            if (_updateHandle == null) return;
            _updateHandle(owner);
        }
        public virtual void OnExit(T owner)
        {
            if (_exitHandle == null) return;
            _exitHandle(owner);
        }
    }
}

