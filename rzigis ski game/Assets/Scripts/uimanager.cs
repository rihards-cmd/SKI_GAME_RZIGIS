using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class uimanager : MonoBehaviour
{
    [SerializeField] private CanvasGroup screenOverlay;

    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private GameObject raceOverPanel;
    [SerializeField] private int nextLevelIndex = 1;

    void Start()
    {
        screenOverlay.gameObject.SetActive(true);
        raceOverPanel.SetActive(false);
        StartCoroutine((FadeOutOverlay()));
    }

    private void OnEnable()
    {
        FinishGate.FinishRace += OnRaceFinished;
    }

    private void OnDisable()
    {
        FinishGate.FinishRace -= OnRaceFinished;
    }

    private void OnRaceFinished()
    {
        raceOverPanel.SetActive(true);
    }

    private IEnumerator FadeOutOverlay()
    {
        while (screenOverlay.alpha > 0)
        {
            screenOverlay.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator FadeInOverlay()
    {
        while (screenOverlay.alpha < 1)
        {
            screenOverlay.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    private IEnumerator RestartCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Quit()
    {
        StartCoroutine(QuitCoroutine());
    }
    private IEnumerator QuitCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        Application.Quit();

    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    public IEnumerator NextLevelCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(nextLevelIndex);
    }
    
}
