using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using System;

namespace TimeObjects
{
    public class RewindObject : MonoBehaviour
    {
        [Header("Rewind Settings")]
        public bool isRewinding = false;
        public Material rewindMaterial;
        public Material forwardMaterial;

        [Tooltip("True: object history == maxRewindInSeconds.\nFalse: object history == infinite")]
        [SerializeField]
        private bool fixedSequence = false;
        [SerializeField]
        private float maxRewindInSeconds = 5.0f;
        private List<PointInTime> objectHistory;
        private Rigidbody _rigidbody;
        private MeshRenderer meshRenderer;


        private void Start()
        {
            objectHistory = new List<PointInTime>();
            meshRenderer = GetComponent<MeshRenderer>();
            try
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        private void FixedUpdate()
        {
            if (isRewinding)
            {
                Rewind();
            }
            else
            {
                Record();
            }
        }

        public bool CanRewind()
        {
            if (fixedSequence && IsRecordingFull() == false)
            {
                return false;
            }

            return true;
        }

        private void Rewind()
        {
            if (objectHistory.Count > 0)
            {
                var last = objectHistory.Count - 1;

                PointInTime PointInTime = objectHistory[last];
                transform.position = objectHistory[last].position;
                transform.rotation = objectHistory[last].rotation;
                objectHistory.RemoveAt(last);
            }
            else
            {
                StopRewind();
            }
        }

        private void Record()
        {
            if (IsRecordingFull() && !fixedSequence)
            {
                objectHistory.RemoveAt(0);
            }

            if (!IsRecordingFull() || !fixedSequence)
                objectHistory.Add(new PointInTime(transform.position, transform.rotation));
        }

        public bool IsRecordingFull()
        {
            return objectHistory.Count > Mathf.Round(maxRewindInSeconds / Time.fixedDeltaTime);
        }

        public void StartRewind()
        {
            if (CanRewind())
            {
                // meshRenderer.material = rewindMaterial;
                isRewinding = true;
                // _rigidbody.isKinematic = true;
            }
        }

        public void StopRewind()
        {
            // meshRenderer.material = forwardMaterial;
            isRewinding = false;
            // _rigidbody.isKinematic = false;
        }
    }
}
