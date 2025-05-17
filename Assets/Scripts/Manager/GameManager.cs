using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject blueBase;
    public GameObject BlueBase => blueBase;

    [SerializeField]
    private GameObject redBase;
    public GameObject RedBase => redBase;

    [SerializeField]
    private GameObject victoryImage;
    [SerializeField]
    private GameObject defeatImage;
    [SerializeField]
    private GameObject resetButton;
    [SerializeField]
    private GameObject menuGroup;
    [SerializeField]
    private GameObject gameGroup;
    [SerializeField]
    private GameObject controlGroup;
    

    private string level = "Normal";
    public string Level => level;

    private bool isLive = true;
    public bool IsLive => isLive;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SelectLevel(string level)
    {
        this.level = level;
        menuGroup.SetActive(false);
        gameGroup.SetActive(true);
        blueBase.SetActive(true);
        redBase.SetActive(true);
        SoundManager.Instance.PlayBgm(true);
    }

    public void GameOver(bool result)
    {
        if (isLive)
        {
            SoundManager.Instance.PlayBgm(false);

            isLive = false;

            if (result)
            {
                victoryImage.SetActive(true);
                PlayerPrefs.SetInt("IsClear" + level, 1);
                SoundManager.Instance.PlaySfx(Type.Sound.Victory);
            }
            else
            {
                defeatImage.SetActive(true);
                SoundManager.Instance.PlaySfx(Type.Sound.Defeat);
            }

            StopAllUnits();

            controlGroup.SetActive(false);
            resetButton.SetActive(true);
            
        }
    }

    private void StopAllUnits()
    {
        foreach (GameObject unitObj in blueBase.GetComponent<Base>().UnitList)
        {
            if (unitObj.GetComponent<Unit>().Dust != null)
            {
                unitObj.GetComponent<Unit>().Dust.gameObject.SetActive(false);
            }
            unitObj.GetComponent<Unit>().enabled = false;
            unitObj.GetComponent<Animator>().enabled = false;
        }

        foreach (GameObject unitObj in redBase.GetComponent<Base>().UnitList)
        {
            if (unitObj.GetComponent<Unit>().Dust != null)
            {
                unitObj.GetComponent<Unit>().Dust.gameObject.SetActive(false);
            }
            unitObj.GetComponent<Unit>().enabled = false;
            unitObj.GetComponent<Animator>().enabled = false;
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
