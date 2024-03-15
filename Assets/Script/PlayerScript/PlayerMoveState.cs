using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //根据输入改变角色的速度
        player.SetVelocity(xInput*player.moveSpeed, rb.velocity.y);

        /*
         * 如果移动过程中鼠标点击，则停止移动进行攻击
         * 否则如果没有获得移动值，则进入闲置状态
         */
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangState(player.primaryAttackState);
        }else if (xInput == 0)
        {
            stateMachine.ChangState(player.idleState);
        }
        
    }
}
