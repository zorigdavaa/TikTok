using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureCamera : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] RenderTexture texture;

    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
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
    }
}
