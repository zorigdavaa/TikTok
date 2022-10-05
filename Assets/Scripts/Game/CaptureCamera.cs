using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureCamera : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] RenderTexture texture;
    public EventHandler OnImageCapture;

    // Start is called before the first frame update
    void Start()
    {
        // cam = transform.GetChild(0).GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CaptureImage();
        }
    }

    public void CaptureImage()
    {
        cam.targetTexture = texture;
        cam.Render();
        cam.targetTexture = null;
        CanvasManager.Instance.ShowPost();
    }

    internal void SubscribeButton()
    {
        CanvasManager.Instance.GetCaptureButton().GetComponent<Button>().onClick.AddListener
        (
            () =>
            {
                CaptureImage();
                OnImageCapture?.Invoke(this, EventArgs.Empty);
            }
        );

    }
}
