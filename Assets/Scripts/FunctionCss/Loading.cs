using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image image;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLoading_2(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator StartLoading_2(int scene)
    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
        DelayComponent.Delay(100, () => { UIManeger.Instance.Show<MainView>(); });
    }

    void SetLoadingPercentage(int displayProgress)
    {
        image.fillAmount = displayProgress / 100f;
        text.text = displayProgress.ToString();
    }
}
