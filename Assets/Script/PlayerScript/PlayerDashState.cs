using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;//设置冲刺计时器
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);//y设置为0避免冲刺时下落

        if (stateTimer < 0)//计时器为0，返回到闲置状态
        {
            stateMachine.ChangState(player.idleState);
        }
    }
}
