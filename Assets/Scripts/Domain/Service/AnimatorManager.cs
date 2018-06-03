using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager
{
    public void SetTrigger(string objName, string triggerName)
    {
        GameObject targetObj = GameObject.Find(objName);
        if (targetObj == null)
        {
            Debug.LogWarning("GameObject.name[" + objName + "] not found.");
            return;
        }
        Animator animator = targetObj.GetComponent<Animator>();
        animator.SetTrigger(triggerName);
    }
}
