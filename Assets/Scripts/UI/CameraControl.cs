using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float moveSpeed;
    public float MoveSpeed => moveSpeed;
    private float minPos = -2f;
    private float maxPos = 2f;

    private bool isLeft = false;
    private bool isRight = false;
    private bool isMove = false;
    public bool IsMove => isMove;

    private void Update()
    {
        if ((isLeft && transform.position.x > minPos) || (isRight && transform.position.x < maxPos))
        {
            isMove = true;
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        else
        {
            isMove = false;
        }
    }

    public void OnButtonUp()
    {
        isLeft = false;
        isRight = false;
        moveSpeed = 0f;
    }

    public void OnLeftButtonDown()
    {
        isLeft = true;
        moveSpeed = -2f;
    }

    public void OnRightButtonDown()
    {
        isRight = true;
        moveSpeed = 2f;
    }
}
