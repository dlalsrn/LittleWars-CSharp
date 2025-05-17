using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, IScannable, IHittable
{
    private Animator animator;

    public IState currentState;

    [SerializeField]
    private Type.Unit unitType;
    public Type.Unit UnitType => unitType;

    [SerializeField]
    private ParticleSystem dust;
    public ParticleSystem Dust => dust;

    [SerializeField]
    private int hp;
    public int Hp => hp;
    [SerializeField]
    private int atk;
    public int Atk => atk;
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    [SerializeField]
    private float scanDistance;
    public float ScanDistance => scanDistance;
    private GameObject scanTarget;
    public GameObject ScanTarget => scanTarget;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeState(new MoveState(this));
    }

    private void Update()
    {
        currentState?.Update();
    }

    private void FixedUpdate()
    {
        if (currentState is MoveState moveState)
        {
            moveState.ScanObj();
        }
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void SetScanTarget(GameObject scanObj)
    {
        scanTarget = scanObj;
    }

    public void Delay(float time, System.Action callback)
    {
        StartCoroutine(DelayRoutine(time, callback));
    }

    IEnumerator DelayRoutine(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    public void OnHit(int damage)
    {
        hp -= damage;

        if (hp > 0)
        {
            animator.SetTrigger("doHit");
        }
        else
        {
            ChangeState(new DieState(this));
        }
    }

    public void Shot(string var, Vector3 pos)
    {
        GameObject bulletObj = Instantiate<GameObject>(UtilityMethods.TeamBase<GameObject>(gameObject.layer, var), pos, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetAtk(atk);
    }
}
