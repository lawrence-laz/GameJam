﻿using DG.Tweening;
using Libs.Base.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Libs.Base.GameLogic
{
    public class SceneLoadingBehaviour : MonoBehaviour
    {
        public string SceneName = "";
        public int SceneOffset = 1;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                RestartScene();
            }
        }

        [ContextMenu("LoadScene")]
        public void LoadScene()
        {
            if (!string.IsNullOrWhiteSpace(SceneName))
            {
                LoadScene(SceneName);
            }
            else
            {
                if (load != null)
                    return;

                ScreenFade.Instance.FadeOut();
                load = DOTween.Sequence()
                    .AppendInterval(1)
                    .AppendCallback(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SceneOffset));
            }
        }

        [ContextMenu("LoadSceneAsync")]
        public void LoadSceneAsync()
        {
            SceneManager.LoadSceneAsync(SceneName);
        }

        Sequence load;

        public void LoadScene(string name)
        {
            if (load != null)
                return;

            ScreenFade.Instance.FadeOut();
            load = DOTween.Sequence()
                .AppendInterval(1)
                .AppendCallback(() => SceneManager.LoadScene(name));
        }

        public void RestartScene()
        {
            if (load != null)
                return;

            FindObjectOfType<TurnManager>().enabled = false;

            load = DOTween.Sequence()
                .AppendInterval(UnityEngine.Input.GetKeyDown(KeyCode.R) ? 0f : 0.8f)
                .AppendCallback(() => ScreenFade.Instance.FadeOut(.3f))
                .AppendInterval(0.2f)
                .AppendCallback(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }
}
