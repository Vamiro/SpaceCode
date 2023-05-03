using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : UIPanel<LoadingSceneState>
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _camera;
    
    protected override void OnShow()
    {
        if (_argument.Scene != null) StartCoroutine(ShowLoadnig(_argument));
        else _argument.OnLoadingCompleted();
    }

    protected override void OnClose()
    {
    }

    protected IEnumerator ShowLoadnig(LoadingSceneState sceneState)
    {
        _camera.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneState.Scene, sceneState.LoadMode);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            _slider.value = asyncOperation.progress;
            
            if (asyncOperation.progress >= 0.9f)
            {
                _slider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }
            yield return new WaitForEndOfFrame();
        }
        _camera.SetActive(false);
        sceneState.OnLoadingCompleted();
    }
}
