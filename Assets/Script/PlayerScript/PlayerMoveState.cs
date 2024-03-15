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

        //��������ı��ɫ���ٶ�
        player.SetVelocity(xInput*player.moveSpeed, rb.velocity.y);

        /*
         * ����ƶ����������������ֹͣ�ƶ����й���
         * �������û�л���ƶ�ֵ�����������״̬
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
