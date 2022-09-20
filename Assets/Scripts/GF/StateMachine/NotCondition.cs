/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：NotCondition
 // 创建日期：2022/5/1 3:58:30
 // 功能描述：
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GF.AI.FSM
{
    public class NotCondition<T> : ICondition<T>
    {
        private ICondition<T> c1;
        public NotCondition(ICondition<T> c1)
        {
            this.c1 = c1;
        }

        public override bool Trigger(T owner)
        {
            return !c1.Trigger(owner);
        }
    }
}
