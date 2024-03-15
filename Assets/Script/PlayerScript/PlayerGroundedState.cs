using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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



        if (!player.IsGroundDetected())//不在地面时转换为空中状态
        {
            stateMachine.ChangState(player.airState);
        }
        if (Input.GetKeyDown(KeyCode.Space)  && player.IsGroundDetected()) //在地面时按下空格进入跳跃状态
        {
            stateMachine.ChangState(player.jumpState);
        }
    }
}
