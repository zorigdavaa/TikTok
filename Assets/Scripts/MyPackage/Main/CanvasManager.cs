using ZPackage;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : GenericSingleton<CanvasManager>
{
    public GameObject beforeStartMenu, afterLostMenu, afterWinMenu, Coin, Level, Hud;
    [SerializeField] TMP_Text CoinText, ScoreText, ScoreMultipText, LevelText, ThrowCount;
    [SerializeField] Image famous;
    [SerializeField] Animation CameraLight;
    [SerializeField] GameObject Post, PoseButtons, CaptureButton;
    private void OnEnable()
    {
        CoinText = Coin.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        LevelText = Level.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
        GameManager.Instance.GamePlay += OnGamePlay;
        GameManager.Instance.GameOverEvent += OnGameOver;
        GameManager.Instance.GameStart += OnGameStart;
    }
    private void Start()
    {
        Hud.SetActive(false);
        ShowButtons(false);
    }

    private void OnGamePlay(object sender, EventArgs e)
    {
        beforeStartMenu.SetActive(false);
        Hud.SetActive(true);
    }

    private void OnGameStart(object sender, EventArgs e)
    {
        beforeStartMenu.SetActive(true);
    }
    public void HudCoin(string value)
    {
        CoinText.text = value;
    }
    public void HudFamous(float value)
    {
        famous.fillAmount = value / 100;
    }
    public void HudScore(string value)
    {
        ScoreText.text = value;
    }
    public void HudThrowCount(string value)
    {
        ThrowCount.text = value;
    }
    public void HudScoreMult(int value)
    {
        ScoreMultipText.text = value.ToString() + "X";
    }
    public void HudLevel(string value)
    {
        LevelText.text = value;
    }
    public void CameraLightPlay()
    {
        CameraLight.Play();
    }
    public void ShowPost()
    {
        StartCoroutine(LocalCoroutine());
        IEnumerator LocalCoroutine()
        {
            Post.SetActive(true);
            yield return new WaitForSeconds(2);
            Post.SetActive(false);
            // float t = 0;
            // float time = 0;
            // float duration = 1;
            // Vector3 initialPosition = transform.position;
            // while (time < duration)
            // {
            //     t = time / duration;
            //     time += Time.deltaTime;
            //     transform.position = Vector3.Lerp(initialPosition, toPos, t);

            //     yield return null;
            // }
        }

    }
    public void ShowButtons(bool value)
    {
        PoseButtons.SetActive(value);
    }
    public GameObject GetCaptureButton()
    {
        return CaptureButton;
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        Hud.SetActive(false);
        Invoke(nameof(ShowAfterLostMenu), 1);
    }
    void ShowAfterLostMenu()
    {
        afterLostMenu.SetActive(true);
    }

    private void OnLevelCompleted(object sender, LevelCompletedEventArgs e)
    {
        Hud.SetActive(false);
        ShowWinMenu(e);
    }

    private void ShowWinMenu(LevelCompletedEventArgs e)
    {
        afterWinMenu.SetActive(true);
        Transform nextButton = afterWinMenu.transform.Find("ButtonNext");
        nextButton.gameObject.SetActive(false);
        StartCoroutine(LocalFunc());
        IEnumerator LocalFunc()
        {
            float time = 0;
            float duration = 1;
            // float scoreToCoin = e.score * 0.3f - e.level * 5;
            // int coinFrom = GameManager.Instance.Coin;
            // int coinTo = coinFrom + Mathf.CeilToInt(scoreToCoin);
            while (time < duration)
            {
                // GameManager.Instance.Coin = (int)Mathf.Lerp(coinFrom, coinTo, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            nextButton.gameObject.SetActive(true);
        }
    }
}
