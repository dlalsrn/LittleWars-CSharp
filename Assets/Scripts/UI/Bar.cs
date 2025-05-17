using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField]
    private RectTransform barRectTransform;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private TextMeshProUGUI shadowText;

    private float width;
    private float height;

    [SerializeField]
    private int max = 10;

    [SerializeField]
    private string var;

    private void Start()
    {
        width = barRectTransform.sizeDelta.x;
        height = barRectTransform.sizeDelta.y;
    }

    private void LateUpdate()
    {
        int value = UtilityMethods.TeamBase<int>(gameObject.layer, var);
        float newWidth = width * (value / (float)max);
        barRectTransform.sizeDelta = new Vector2(newWidth, height);

        text.SetText(value.ToString());
        shadowText.SetText(value.ToString());
    }
}
