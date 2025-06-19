using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class AdditionalSceneLoader: MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(LoadVFXSceneAndResetActive());
        }

        private IEnumerator LoadVFXSceneAndResetActive()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            yield return new WaitUntil(() => op.isDone);

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        }
    }
}