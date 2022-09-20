/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：ICondition
 // 创建日期：2022/5/1 3:46:03
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
    public class ICondition<T>
    {
        private Func<T, bool> conditionHandle;
        public ICondition() { }
        public ICondition(Func<T,bool> handle)
        {
            BindCondition(handle);
        }

        /// <summary>
        /// 绑定条件
        /// </summary>
        /// <param name="handle"></param>
        public void BindCondition(Func<T,bool> handle)
        {
            conditionHandle = handle;
        }

        public virtual bool Trigger(T owner)
        {
            return conditionHandle != null && conditionHandle.Invoke(owner); 
        }

        public static ICondition<T> operator&(ICondition<T> c1,ICondition<T> c2)
        {
            return new AndCondition<T>(c1, c2);
        }
        public static ICondition<T> operator |(ICondition<T> c1, ICondition<T> c2)
        {
            return new OrCondition<T>(c1, c2);
        }
        public static ICondition<T> operator !(ICondition<T> c1)
        {
            return new NotCondition<T>(c1);
        }
    }

    public class ICondition<T1,T2>:ICondition<T1>
    {
        private Func<T1, T2, bool> conditionHandle;
        private T2 _value;
        public ICondition() { }
        public ICondition(Func<T1,T2, bool> handle,T2 value)
        {
            BindCondition(handle,value);
        }

        public void BindCondition(Func<T1, T2, bool> handle,T2 value)
        {
            conditionHandle = handle;
            _value = value;
        }
        public override bool Trigger(T1 owner)
        {
            return conditionHandle != null && conditionHandle(owner, _value);
        }

    }
}
