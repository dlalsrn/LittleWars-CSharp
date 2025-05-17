using System.Collections.Generic;
using UnityEngine;

public static class UtilityMethods
{
    private const int BlueLayer = 8;

    public static GameObject ScanEnemy(GameObject obj)
    {
        Vector3 pos = obj.transform.position;
        Vector3 dir = TeamValue(obj.layer, Vector3.right, Vector3.left); // 자신의 layer를 기반으로 Scan dir 결정
        IScannable scan = obj.GetComponent<IScannable>();
        float dis = scan.ScanDistance;
        string layerMask = TeamValue(obj.layer, "Red", "Blue");  // 자신의 layer를 기반으로 Scan 할 Layer 결정, Blue는 Red, Red는 Blue

        RaycastHit2D raycastHit2d = Physics2D.Raycast(pos, dir, dis, LayerMask.GetMask(layerMask));

        return raycastHit2d.collider != null ? raycastHit2d.collider.gameObject : null;
    }

    public static GameObject ScanAlly(GameObject obj)
    {
        Vector3 pos = obj.transform.position;
        Vector3 dir = TeamValue(obj.layer, Vector3.right, Vector3.left); // 자신의 layer를 기반으로 Scan dir 결정
        float dis = 0.3f;
        string layerMask = TeamValue(obj.layer, "Blue", "Red"); // 자신의 layer를 기반으로 Scan 할 Layer 결정, Blue는 Blue, Red는 Red

        RaycastHit2D raycastHit2d = Physics2D.Raycast(pos, dir, dis, LayerMask.GetMask(layerMask));

        return raycastHit2d.collider != null ? raycastHit2d.collider.gameObject : null;
    }

    public static T TeamValue<T>(int layer, T blue, T red)
    {
        return layer == BlueLayer ? blue : red;
    }

    public static T TeamBase<T>(int layer, string var = "")
    {
        // 현재 Unit의 Layer를 기반으로 자신의 Base Object를 가져옴
        GameObject myBase = TeamValue(layer, GameManager.Instance.BlueBase, GameManager.Instance.RedBase);

        // Var이 없을 경우는 Base를 Return
        if (string.IsNullOrEmpty(var))
        {
            return (T)(object)myBase;
        }

        var baseComponent = myBase.GetComponent<Base>();

        // Var에 존재하면 Base에서 해당 Object를 return
        return var switch
        {
            "Arrow" when typeof(T) == typeof(GameObject) => (T)(object)baseComponent.Arrow,
            "Magic" when typeof(T) == typeof(GameObject) => (T)(object)baseComponent.Magic,
            "Cost"  when typeof(T) == typeof(int) => (T)(object)baseComponent.Cost,
            "Hp" when typeof(T) == typeof(int) => (T)(object)baseComponent.Hp,
            "UnitList" when typeof(T) == typeof(List<GameObject>) => (T)(object)baseComponent.UnitList,
           _ => (T)(object)null,
        };
    }
}
