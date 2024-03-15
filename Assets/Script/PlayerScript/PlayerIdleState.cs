using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    public override void Update()
    {
        base.Update();
        
        /*
         * 闲置过程中如果鼠标点击，则进入攻击状态
         * 否则如果获得了移动值，则进入移动状态
         */
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangState(player.primaryAttackState);
        }
        else if (xInput != 0 && !player.isBusy)
        {
            stateMachine.ChangState(player.moveState);
        }
    }

}
