using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField]
    private GameObject[] unitPrefabs;
    [SerializeField]
    private int cost;
    [SerializeField]
    private GameObject upgradeButton;
    public GameObject UpgradeButton => upgradeButton;
    [SerializeField]
    private bool isWizard;
    private int level = 0;

    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private TextMeshProUGUI atkText;
    [SerializeField]
    private TextMeshProUGUI costText;
    [SerializeField]
    private Image portraitImage;
    [SerializeField]
    private Sprite[] portraitSprites;

    private Base myBase;

    private void Awake()
    {
    }

    private void Start()
    {
        SetUnitInfo();
        myBase = UtilityMethods.TeamBase<GameObject>(gameObject.layer).GetComponent<Base>();
    }

    private void LateUpdate()
    {
        int curLevel = level - (isWizard ? 1 : 0);

        if (myBase.Cost == 10 && curLevel < 1)
        {
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
        }
    }

    public void UpgradeLevel()
    {
        if (CheckBaseCost(10))
        {
            level++;

            if (isWizard)
            {
                if (unitPrefabs[level - 1] != null)
                {
                    unitPrefabs[level - 1].SetActive(false);
                }

                unitPrefabs[level].SetActive(true);
            }

            SetUnitInfo();
            SoundManager.Instance.PlaySfx(Type.Sound.Upgrade);
        }

    }

    public void BuyUnit()
    {
        if (CheckBaseCost(cost))
        {
            myBase.AddUnit(Instantiate(unitPrefabs[level]));
            SoundManager.Instance.PlaySfx(Type.Sound.Buy);
        }
    }

    private bool CheckBaseCost(int cost)
    {
        if (cost <= myBase.Cost)
        {
            myBase.DecreaseCost(cost);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetUnitInfo()
    {
        if (isWizard)
        {
            if (unitPrefabs[level] == null)
            {
                atkText.SetText("0");
            }
            else
            {
                Unit unit = unitPrefabs[level].GetComponent<Unit>();
                atkText.SetText($"{unit.Atk}");
            }
            portraitImage.sprite = portraitSprites[level];
        }
        else
        {
            Unit unit = unitPrefabs[level].GetComponent<Unit>();
            hpText.SetText($"{unit.Hp}");
            atkText.SetText($"{unit.Atk}");
            costText.SetText($"{cost}");
            portraitImage.sprite = portraitSprites[level];
        }
    }
}
