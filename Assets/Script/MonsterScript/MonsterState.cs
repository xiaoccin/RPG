using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState
{
    protected MonsterStateMachine stateMachine;
    protected Monster monster;
    protected Rigidbody2D rb;

    protected float stateTimer;//��ʱ��
    private string animBoolName;
    protected bool triggerCalled;//�������������Ʊ�־

    public MonsterState(MonsterStateMachine stateMachine, Monster monster, string _animBoolName)
    {
        this.stateMachine = stateMachine;
        this.monster = monster;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        this.rb = monster.rb;
        monster.animator.SetBool(animBoolName, true);
        triggerCalled = false;
    }


    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }



    public virtual void Exit()
    {
        monster.animator.SetBool(animBoolName, false);
    }
}
