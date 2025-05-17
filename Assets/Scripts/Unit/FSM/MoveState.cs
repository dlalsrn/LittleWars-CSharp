using UnityEngine;

public class MoveState : IState
{
    private Unit unit;

    public MoveState(Unit unit)
    {
        this.unit = unit;
        unit.GetComponent<Animator>().SetTrigger("doMove");
    }

    public void Enter()
    {
        if (unit.Dust != null)
        {
            unit.Dust.Play();
        }
    }

    public void Update()
    {
        unit.transform.Translate(unit.MoveSpeed * Time.deltaTime, 0f, 0f);
    }

    public void ScanObj()
    {
        GameObject scanObj = UtilityMethods.ScanEnemy(unit.gameObject);

        if (scanObj != null)
        {
            unit.SetScanTarget(scanObj);
            unit.ChangeState(new AttackState(unit));
        }
        else
        {
            scanObj = UtilityMethods.ScanAlly(unit.gameObject);

            if (scanObj != null)
            {
                unit.ChangeState(new StopState(unit));
            }
        }
    }
    
    public void Exit()
    {
        if (unit.Dust != null)
        {
            unit.Dust.Stop();
        }
    }
}
