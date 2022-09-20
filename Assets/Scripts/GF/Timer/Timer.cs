using System.Collections;
using UnityEngine.Events;
using UnityEngine;
namespace GF
{
    public class Timer : Singleton<Timer>
    {
        public void Start(float time,UnityAction action)
        {
            MonoMgr.Instance.StartCoroutine(Hide(time, action));
        }


        IEnumerator Hide(float time, UnityAction action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }

    }
}

