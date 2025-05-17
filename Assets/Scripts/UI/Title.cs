using UnityEngine;

public class Title : MonoBehaviour
{
    private RectTransform rectTransform;

    private Vector2 originPos;

    private float amplitude = 1.5f;
    private float frame = 0;
    private float rate = 0.007f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        originPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        frame += rate;
        Vector2 newPos = originPos + new Vector2(0f, Mathf.Sin(frame) * amplitude);
        rectTransform.anchoredPosition = newPos;
    }
}
