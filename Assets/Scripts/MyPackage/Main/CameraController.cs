using ZPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZPackage
{

    public class CameraController : GenericSingleton<CameraController>
    {
        [SerializeField] Transform followTf, lastFollowing;
        [SerializeField] CamerShaker shaker;
        [SerializeField] Vector3 offset;
        Vector3 velocity = Vector3.zero;
        [SerializeField][Range(0.01f, 1f)] float smoothTime = 0.125f;
        Camera cam;

        [Header("Zoom")]
        public float zoomDuration = 1;
        public float zoomMagnitude = 10;
        public static bool IsZoom = false;

        private void Awake()
        {
            // player = FindObjectOfType<PlayerMovement>().transform;
            //offset = new Vector3(6, 14, -19);
            offset = transform.position - followTf.position;
            cam = transform.GetChild(0).GetComponent<Camera>();
            Follow(followTf);
        }
        void OnGameStart()
        {
            transform.position = new Vector3(7, 15, -22);
        }
        private void LateUpdate()
        {
            if (followTf)
            {
                Vector3 targetPos = followTf.position + offset;
                // transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
                transform.position = targetPos;
            }
            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     GoToPosition(new Vector3(10, 0, 20), new Vector3(0, 0, 0));
            // }
            // else if (Input.GetKeyDown(KeyCode.E))
            // {
            //     GotoDefault();
            // }
        }
        // private void FixedUpdate()
        // {
        //     if (followTf)
        //     {
        //         Vector3 targetPos = followTf.position + offset;
        //         // transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        //         transform.position = targetPos;
        //     }

        //     //else if(Flying)
        //     //{
        //     //    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        //     //}
        // }
        public void Follow(Transform follow)
        {
            lastFollowing = followTf;
            followTf = follow;
        }
        public void FollowLast()
        {
            Follow(lastFollowing);
        }
        public void GoToPosition(Vector3 toPos, Vector3 rotation)
        {
            Follow(null);
            StartCoroutine(LocalCoroutine());
            IEnumerator LocalCoroutine()
            {
                float t = 0;
                float time = 0;
                float duration = 1;
                Vector3 initialPosition = transform.position;
                Quaternion initRot = transform.rotation;
                Quaternion toRotation = Quaternion.Euler(rotation);
                while (time < duration)
                {
                    time += Time.deltaTime;
                    t = time / duration;
                    transform.position = Vector3.Lerp(initialPosition, toPos, t);
                    transform.rotation = Quaternion.Lerp(initRot, toRotation, t);

                    yield return null;
                }
            }
        }
        public void GotoDefault()
        {

            StartCoroutine(LocalCoroutine());
            IEnumerator LocalCoroutine()
            {
                float t = 0;
                float time = 0;
                float duration = 1;
                Vector3 initialPosition = transform.position;
                Vector3 toPos = lastFollowing.position + offset;
                Quaternion initRot = transform.rotation;
                Quaternion toRotation = Quaternion.Euler(new Vector3(30, 0, 0));
                while (time < duration)
                {
                    time += Time.deltaTime;
                    t = time / duration;
                    transform.position = Vector3.Lerp(initialPosition, toPos, t);
                    transform.rotation = Quaternion.Lerp(initRot, toRotation, t);

                    yield return null;
                }
                FollowLast();
            }
        }

        Coroutine offsetCoroutine;
        public void SetOffset(Vector3 incominOff, Vector3 rotation, float duration)
        {
            if (offsetCoroutine != null)
            {
                StopCoroutine(offsetCoroutine);
            }
            offsetCoroutine = StartCoroutine(SetOffsetCor());
            IEnumerator SetOffsetCor()
            {
                float time = 0;
                Vector3 startingOffset = offset;
                Vector3 startingRotation = transform.eulerAngles;
                Quaternion startQuaternin = transform.rotation;
                Quaternion toRoataion = Quaternion.Euler(rotation);
                while (time < duration)
                {
                    time += Time.deltaTime;
                    offset = Vector3.Lerp(startingOffset, incominOff, time / duration);
                    transform.rotation = Quaternion.Lerp(startQuaternin, toRoataion, time / duration);
                    //  transform.eulerAngles = Vector3.Lerp(startingRotation,IncomingRotation,time/duration);
                    yield return null;
                }
                offset = incominOff;
                print(incominOff);
                // transform.eulerAngles = IncomingRotation;
            }
        }

        public void Shake(float lenght = 0.5f, float power = 0.3f)
        {
            // shaker.ShakeCor();
            shaker.ShakeLateUpdate(lenght, power);
        }

        public void SetOffset(Vector3 target, float duration = 2.0f)
        {
            StartCoroutine(SetOffsetCoroutine(target, duration));
            //offset = target;
            //while (offset != target)
            //{
            //offset = Vector3.SmoothDamp(offset, target, ref velocity, smoothTime);
            //}
            IEnumerator SetOffsetCoroutine(Vector3 target, float duration = 2.0f)
            {
                float time = 0;
                Vector3 startingOffset = offset;
                while (time < duration)
                {
                    time += Time.deltaTime;
                    float t = time / duration;
                    Vector3 offsets = Vector3.Lerp(startingOffset, target, t);
                    offset = offsets;
                    yield return null;
                }
            }
        }
        public void SetRotation(Vector3 target, float duration = 1.0f)
        {
            StartCoroutine(SetRotationCor(target, duration));
            //offset = target;
            //while (offset != target)
            //{
            //offset = Vector3.SmoothDamp(offset, target, ref velocity, smoothTime);
            //}
            IEnumerator SetRotationCor(Vector3 target, float duration = 2.0f)
            {
                float time = 0;
                Quaternion initRot = transform.rotation;
                Quaternion targetRot = Quaternion.Euler(target);
                while (time < duration)
                {
                    time += Time.deltaTime;
                    float t = time / duration;
                    transform.rotation = Quaternion.Lerp(initRot,targetRot,t);
                    yield return null;
                }
            }
        }
        Coroutine zoomCoroutine;
        public void Zoom(float duration = 1, float to = 20, Action afterAction = null)
        {
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            zoomCoroutine = StartCoroutine(LocalFunction());
            IEnumerator LocalFunction()
            {
                if (cam.fieldOfView == to)
                {
                    if (afterAction != null)
                    {
                        afterAction();
                    }
                    yield break;
                }
                float time = 0;
                float initialvalue = cam.fieldOfView;
                while (time < duration)
                {
                    time += Time.deltaTime;
                    cam.fieldOfView = Mathf.Lerp(initialvalue, to, time / duration);
                    yield return null;
                }
                if (afterAction != null)
                {
                    afterAction();
                }
            }
        }
        public void ZoomToWard(float value, float speed = 0.3f)
        {
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, value, speed);
        }
        public void ZoomInstant(float value = 60)
        {
            cam.fieldOfView = value;
        }
        // public void Zoom(Vector3 pos)
        // {
        //     StartCoroutine(Zoom(pos, Vector3.Distance(transform.position, pos) / zoomMagnitude * zoomDuration));
        // }
        // IEnumerator Zoom(Vector3 pos, float duration)
        // {
        //     if (!IsZoom)
        //     {
        //         IsZoom = true;
        //         Vector3 startPos = transform.position;
        //         for (float t = 0; t < duration; t += DT)
        //         {
        //             transform.position = Vector3.Lerp(startPos, pos, t / duration);
        //             yield return null;
        //         }
        //         transform.position = pos;
        //         IsZoom = false;
        //     }
        // }


        public void AddValueToOffset(Vector3 addingV3)
        {
            var taret = offset += addingV3;
            SetOffset(taret);
        }
        public void MinusValueFromOffset(Vector3 minusV3)
        {
            var taret = offset -= minusV3;
            SetOffset(taret);
        }
    }
}
