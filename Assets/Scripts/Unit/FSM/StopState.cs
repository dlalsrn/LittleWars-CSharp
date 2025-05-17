using System.Collections;
using UnityEngine;

public class StopState : IState
{
    private Unit unit;

    public StopState(Unit unit)
    {
        this.unit = unit;
    }

    public void Enter()
    {
        unit.GetComponent<Animator>().SetTrigger("doStop");
        ChangeMoveState();
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }

    private void ChangeMoveState()
    {
        IState thisState = this;
        unit.Delay(0.5f, () =>
        {
            if (unit.currentState == thisState)
            {
                unit.ChangeState(new MoveState(unit));
            }
        });
    }
}
