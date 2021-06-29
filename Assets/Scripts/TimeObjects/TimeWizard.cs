using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Controller;
using UnityStandardAssets.Characters.ThirdPerson;

namespace TimeObjects
{
    public class TimeWizard : MonoBehaviour
    {
        public bool enableSuperHot = true;
        public float minTimeScale = 0.5f;
        public float timeAcceleration = 0.5f;
        public float timeDeceleration = 0.5f;
        public float maxTimeScale = 0.5f;
        private ThirdPersonUserControl controller;
        public GameObject cometView;
        public bool showComet = false;

        private void Start()
        {
            controller = GetComponent<ThirdPersonUserControl>();

            if (enableSuperHot)
            {
                Time.timeScale = minTimeScale;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleCometView();
            }
            if (Input.GetMouseButtonDown(0))
            {
                RewindAll();
            }
            if (cometView.activeSelf && !showComet)
            {
                cometView.SetActive(false);
            }
            if (!cometView.activeSelf && showComet)
            {
                cometView.SetActive(true);
            }
            if (enableSuperHot)
            {
                if (controller.ShouldMove())
                {
                    if (enableSuperHot)
                    {
                        Time.timeScale = Mathf.Clamp(Time.timeScale + timeAcceleration * Time.unscaledDeltaTime, minTimeScale, maxTimeScale);
                    }
                }
                else
                {
                    if (enableSuperHot)
                    {
                        Time.timeScale = Mathf.Clamp(Time.timeScale - timeDeceleration * Time.unscaledDeltaTime, minTimeScale, maxTimeScale);
                    }
                }

            }
        }

        private void ToggleCometView()
        {
            if (!showComet) showComet = true;
            else
            {
                showComet = false;
            }
        }

        private void RewindAll()
        {
            var rewindables = GameObject.FindGameObjectsWithTag("Rewindable");
            foreach (var item in rewindables)
            {
                var rewindable = item.GetComponent<RewindObject>();

                if (!rewindable.isRewinding)
                    rewindable.StartRewind();
            }
        }
    }
}