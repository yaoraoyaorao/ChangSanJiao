/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：IScene
 // 创建日期：2022/4/18 14:40:02
 // 功能描述：场景接口类
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GF
{

    public class IScene
    {
        public string sceneName;
        public SceneController sceneController;
        public IScene(SceneController s)
        {
            sceneController = s;
        }

        public virtual void Begin() { }
        public virtual void End() { }
        public virtual void Update() { }
    }
}