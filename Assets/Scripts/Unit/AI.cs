using System.Collections;
using UnityEngine;

public class AI : MonoBehaviour
{
    private int pattern = 0;

    private Base myBase;

    [SerializeField]
    private UnitButton rsButton;
    [SerializeField]
    private UnitButton raButton;
    [SerializeField]
    private UnitButton rgButton;
    [SerializeField]
    private UnitButton rwButton;

    private UnitButton selectUpgradeBtn;

    void Start()
    {
        StartCoroutine(AIRoutine());
        myBase = UtilityMethods.TeamBase<GameObject>(gameObject.layer).GetComponent<Base>();
    }

    IEnumerator AIRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (GameManager.Instance.IsLive)
        {
            switch (pattern)
            {
                case 0:
                    yield return new WaitUntil(() => myBase.Cost >= 5);
                    Pattern1();
                    break;
                case 1:
                    yield return new WaitUntil(() => myBase.Cost >= 7);
                    Pattern2();
                    break;
                case 2:
                    yield return new WaitUntil(() => myBase.Cost >= 10);
                    Pattern3();
                    break;
            }

            int next = Random.Range(0, 3);
            pattern = next;
        }
    }

    private void Pattern1()
    {
        if (!GameManager.Instance.IsLive)
        {
            return;
        }

        int choice = Random.Range(0, 2);
        
        switch (choice)
        {
            case 0:
                rsButton.BuyUnit();
                break;
            case 1:
                raButton.BuyUnit();
                break;
        }
    }

    private void Pattern2()
    {
        if (!GameManager.Instance.IsLive)
        {
            return;
        }

        int choice = Random.Range(0, 2);

        switch (choice)
        {
            case 0:
                Pattern1();
                break;
            case 1:
                rgButton.BuyUnit();
                break;
        }
    }

    private void Pattern3()
    {
        if (!GameManager.Instance.IsLive)
        {
            return;
        }
        
        int choice = Random.Range(0, 2);

        switch (choice)
        {
            case 0:
                Pattern2();
                break;
            case 1:
                choice = Random.Range(0, 4);

                switch (choice)
                {
                    case 0:
                        selectUpgradeBtn = rsButton;
                        break;
                    case 1:
                        selectUpgradeBtn = raButton;
                        break;
                    case 2:
                        selectUpgradeBtn = rgButton;
                        break;
                    case 3:
                        selectUpgradeBtn = rwButton;
                        break;
                }

                // Upgrade가 활성화되어 있을 때만 Upgrade 가능. (즉, 현재 Cost가 10이고, Upgrade를 하지 않았을 때)
                if (selectUpgradeBtn.UpgradeButton.activeSelf)
                {
                    selectUpgradeBtn.UpgradeLevel();
                }
                else
                {
                    Pattern2();
                }

                break;
        }
    }
}
