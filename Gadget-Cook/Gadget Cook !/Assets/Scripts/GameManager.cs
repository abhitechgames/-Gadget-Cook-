using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region  Variables
    public int levelNo = 1;

    public GameObject winUI;
    public GameObject winButton;
    public GameObject failButton;
    public GameObject failUI;

    public GameObject settingPannel;

    public TMPro.TMP_Text levelNoText;

    public UnityEngine.UI.Slider slider;
    #endregion

    #region  Singleton
    public static GameManager instance;

    GameManager()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        levelNo = PlayerPrefs.GetInt("Level", 1);
        levelNoText.text = "Level " + levelNo.ToString();
    }

    public void Play()
    {
        StartCoroutine(IPlay());
    }

    public void Next()
    {
        StartCoroutine(INext());
    }

    public void Retry()
    {
        StartCoroutine(IRetry());
    }

    IEnumerator IPlay() //To Delay Screen Loading
    {
        AudioManager.instance.Play("Click");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level Name", 1));
    }

    IEnumerator INext() //To Delay Screen Loading
    {
        AudioManager.instance.Play("Click");
        yield return new WaitForSeconds(0.4f);
        levelNo++;
        PlayerPrefs.SetInt("Level", levelNo);
        if (levelNo > 4)
        {
        label:
            int rand = Random.Range(1, 5);
            if (rand != SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(rand);
                PlayerPrefs.SetInt("Level Name", rand);
            }
            else
                goto label;
        }
        else
        {
            SceneManager.LoadScene(levelNo);
        }
    }

    IEnumerator IRetry() //To Delay Screen Loading
    {
        AudioManager.instance.Play("Click");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Fail_UI() //Got Called in case of FAIL
    {
        levelNoText.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        winUI.SetActive(false);
        winButton.SetActive(false);
        failUI.SetActive(true);
        failButton.SetActive(true);

        TurnOffSettings();
    }

    public void Win_UI() //Got Called in case of WIN
    {
        levelNoText.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        failUI.SetActive(false);
        failButton.SetActive(false);
        winUI.SetActive(true);
        winButton.SetActive(true);

        TurnOffSettings();
    }

    public void TurnOnSettings() // Turn On Settings Menu
    {
        settingPannel.SetActive(true);
    }

    public void TurnOffSettings() // Turn Off Settings Menu
    {
        settingPannel.SetActive(false);
    }

    // --- Slider Values Changes ---

    private float value = 0f;

    public void AddSliderValue(float val) // Updates Progress Slider
    {
        value = val;
    }

    private void Update()
    {
        slider.value = Mathf.Lerp(slider.value, value, Time.deltaTime * 5f);
    }
}
