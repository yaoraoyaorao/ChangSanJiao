/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：FsmManager
 // 创建日期：2022/5/1 3:46:42
 // 功能描述：
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GF.AI.FSM
{
    public class FsmManager<T>
    {
        public string currentStateName;
        private T _owner;
        private Dictionary<string, IFsmState<T>> _states;
        private IFsmState<T> currentState;
        private IFsmState<T> defaultState;
        private bool isInit = false;
        public FsmManager(T owner)
        {
            _owner = owner;
            _states = new Dictionary<string, IFsmState<T>>();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            if (!isInit)
            {
                currentState.OnEnter(_owner);
                isInit = true;
            }
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="state"></param>
        public void AddState(string stateName,IFsmState<T> state)
        {
            if (state == null || string.IsNullOrEmpty(stateName)) return;
            _states.Add(stateName, state);
        }

        /// <summary>
        /// 设置默认状态
        /// </summary>
        /// <param name="stateName"></param>
        public void SetDefalutState(string stateName)
        {
            if (string.IsNullOrEmpty(stateName)) return;

            if(_states.ContainsKey(stateName))
            {
                defaultState = _states[stateName];
                currentState = defaultState;
                currentStateName = stateName;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            Init();
            if (currentState == null) return;
            currentState.OnUpdate(_owner);
            if (currentState.CheckCondition(_owner,out string stateName))
            {
                ChangeState(stateName);
                currentStateName = stateName;
            }

        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="stateName"></param>
        private void ChangeState(string stateName)
        {
            if (_states.TryGetValue(stateName,out IFsmState<T> state))
            {
                currentState.OnExit(_owner);

                currentState = state;

                currentState.OnEnter(_owner);
            }
        }
    }
}

