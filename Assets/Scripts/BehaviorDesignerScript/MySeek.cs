using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;

public class MySeek : NavMeshMovement
{
    public SharedVector3 targetPosition;

    public override void OnAwake()
    {
        base.OnAwake();
        targetPosition = transform.position;
    }
    public override void OnStart()
    {
        base.OnStart();
        SetDestination(targetPosition.Value);
    }
    public override TaskStatus OnUpdate()
    {
        if (HasArrived())
        {
            return TaskStatus.Success;
        }

        SetDestination(targetPosition.Value);

        return TaskStatus.Running;
    }
}
