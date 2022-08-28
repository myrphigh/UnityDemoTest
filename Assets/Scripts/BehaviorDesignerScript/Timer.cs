using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using BehaviorDesigner;

public class Timer : Action
{
    public SharedFloat TimerTarget;
    public override void OnStart()
    {
        TimerTarget.Value = 0f;
    }
    public override TaskStatus OnUpdate()
    {
        TimerTarget.Value += Time.deltaTime;
        return TaskStatus.Running;
    }
}
