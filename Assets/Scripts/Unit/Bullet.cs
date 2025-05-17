using UnityEngine;

public class Bullet : MonoBehaviour, IScannable
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float scanDistance;
    public float ScanDistance => scanDistance;
    
    private int atk;

    private bool isHit = false;

    private void Update()
    {
        transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0f, 0f));
    }

    private void FixedUpdate()
    {
        if (!isHit)
        {
            GameObject scanObj = UtilityMethods.ScanEnemy(gameObject);

            if (scanObj != null)
            {
                isHit = true;
                scanObj.GetComponent<IHittable>().OnHit(atk);

                if (moveSpeed != 0f)
                {
                    SoundManager.Instance.PlaySfx(Type.Sound.RangeHit);
                }

                Destroy(gameObject, moveSpeed == 0f ? 3f : 0f);
            }
        }
    }

    public void SetAtk(int atk)
    {
        this.atk = atk;
    }
}
