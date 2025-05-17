using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DieState : IState
{
    private Unit unit;

    public DieState(Unit unit)
    {
        this.unit = unit;
    }

    public void Enter()
    {
        UtilityMethods.TeamBase<GameObject>(unit.gameObject.layer).GetComponent<Base>().RemoveUnit(unit.gameObject);
        unit.GetComponent<Animator>().SetTrigger("doDie");
        unit.GetComponent<BoxCollider2D>().enabled = false;
        unit.GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Destroy(unit.gameObject, 3f);
    }
    
    public void Update()
    {

    }

    public void Exit()
    {

    }
}
