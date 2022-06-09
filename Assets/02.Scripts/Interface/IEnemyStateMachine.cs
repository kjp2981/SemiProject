using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStateMachine
{
    public EnemyState state { get; set; }

    public void ChangeState(EnemyState state);

    public void CheckState();

    public void MosnterAction();
}