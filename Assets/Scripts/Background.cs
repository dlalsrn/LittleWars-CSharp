using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float offset;

    CameraControl mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.gameObject.GetComponent<CameraControl>();
    }

    void Update()
    {
        if (mainCamera.IsMove)
        {
            transform.position += new Vector3(mainCamera.MoveSpeed * offset * Time.deltaTime, 0f, 0f);        
        }
    }
}
