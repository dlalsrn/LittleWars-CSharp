using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IHittable
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject arrow;
    public GameObject Arrow => arrow;

    [SerializeField]
    private GameObject magic;
    public GameObject Magic => magic;

    [SerializeField]
    private int cost;
    public int Cost => cost;
    private int maxCost = 10;
    private float costInterval;

    [SerializeField]
    private int hp;
    public int Hp => hp;
    [SerializeField]
    private Sprite hitSprite;
    [SerializeField]
    private Sprite normalSprite;
    [SerializeField]
    private Sprite destroySprite;
    [SerializeField]
    private GameObject destroyFx;

    [SerializeField]
    private List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> UnitList => unitList;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        costInterval = UtilityMethods.TeamValue(gameObject.layer, 1.5f, CheckLevel());
        StartCoroutine(CostRoutine());
    }

    IEnumerator CostRoutine()
    {
        yield return null;

        while (GameManager.Instance.IsLive)
        {
            yield return new WaitForSeconds(costInterval);

            if (cost < maxCost)
            {
                cost++;
            }
        }
    }

    private float CheckLevel()
    {
        string level = GameManager.Instance.Level;

        return level switch
        {
            "Easy" => 2f,
            "Normal" => 1.5f,
            "Hard" => 1f,
            _ => 0f,
        };
    }

    public void AddUnit(GameObject unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(GameObject unit)
    {
        unitList.Remove(unit);
    }

    public void DecreaseCost(int cost)
    {
        this.cost -= cost;
    }

    public void OnHit(int damage)
    {
        hp -= damage;

        if (hp > 0)
        {
            StartCoroutine(HitRoutine());
        }
        else
        {
            hp = 0;
            spriteRenderer.sprite = destroySprite;
            destroyFx.SetActive(true);

            bool result = UtilityMethods.TeamValue(gameObject.layer, false, true);
            GameManager.Instance.GameOver(result);
        }
    }

    IEnumerator HitRoutine()
    {
        spriteRenderer.sprite = hitSprite;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = normalSprite;
    }
}
