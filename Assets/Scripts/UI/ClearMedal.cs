using UnityEngine;
using UnityEngine.UI;

public class ClearMedal : MonoBehaviour
{
    [SerializeField]
    private string level;

    private void Start()
    {
        GetComponent<Image>().enabled = PlayerPrefs.GetInt("IsClear" + level, 0) == 1 ? true : false;
    }
}
