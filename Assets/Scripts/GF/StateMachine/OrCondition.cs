/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：OrCondition
 // 创建日期：2022/5/1 3:57:10
 // 功能描述：
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GF.AI.FSM
{
    public class OrCondition<T> : ICondition<T>
    {

        private ICondition<T> c1;
        private ICondition<T> c2;
        public OrCondition(ICondition<T> c1, ICondition<T> c2)
        {
            this.c1 = c1;
            this.c2 = c2;
        }

        public override bool Trigger(T owner)
        {
            return c1.Trigger(owner) || c2.Trigger(owner);
        }
    }
}
