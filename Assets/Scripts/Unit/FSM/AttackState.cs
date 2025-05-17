using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.XR;

public class AttackState : IState
{
    private Unit unit;

    public AttackState(Unit unit)
    {
        this.unit = unit;
    }

    public void Enter()
    {
        unit.GetComponent<Animator>().SetTrigger("doAttack");

        switch (unit.UnitType)
        {
            case Type.Unit.Sword:
                unit.ScanTarget.GetComponent<IHittable>().OnHit(unit.Atk);
                SoundManager.Instance.PlaySfx(Type.Sound.Sword);
                break;
            case Type.Unit.Gaurd:
                unit.ScanTarget.GetComponent<IHittable>().OnHit(unit.Atk);
                SoundManager.Instance.PlaySfx(Type.Sound.Guard);
                break;
            case Type.Unit.Range:
                unit.Shot("Arrow", unit.transform.position);
                SoundManager.Instance.PlaySfx(Type.Sound.Range);
                break;
            case Type.Unit.Magic:
                unit.Shot("Magic", unit.ScanTarget.transform.position);
                SoundManager.Instance.PlaySfx(Type.Sound.Magic);
                break;
        }

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
        unit.Delay(Random.Range(0.8f, 1.2f), () =>
        {
            if (unit.currentState == thisState)
            {
                unit.ChangeState(new MoveState(unit));
            }
        });
    }
}
