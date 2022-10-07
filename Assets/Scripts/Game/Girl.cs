using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Girl : MonoBehaviour
{
    [Serializable]
    public class GirlModel
    {
        public List<GameObject> objects;
        public Avatar avatar;
    }
    public List<GirlModel> models;
    public int currentModelIndex { get; private set; } = 0;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        SpinModel();
    }

    public void ChangeGirl(int index, bool spin = true)
    {
        if (index < 0 || index > models.Count - 1 || index == currentModelIndex)
        {
            Debug.LogError("model index: " + index + " out of range");
            return;
        }
        // models[currentModelIndex].SetActive(false);
        // models[index].SetActive(true);
        currentModelIndex = index;

    }
    public void SpinModel(float deg = 360, float duration = 0.7f)
    {
        StartCoroutine(SpinCor(deg, duration));

    }
    IEnumerator SpinCor(float degree)
    {
        float time = 0;
        float duration = 0.5f;
        Vector3 from = transform.localEulerAngles;
        Vector3 to = transform.localEulerAngles + new Vector3(0, degree, 0);
        while (time < duration)
        {
            time += Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(from, to, time / duration);
            yield return null;
        }
    }
    IEnumerator SpinCor(float degree = 360, float duration = 0.5f)
    {
        float time = 0;
        // float duration = 0.5f;
        while (time < duration)
        {
            time += Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, degree, 0), time / duration);
            yield return null;
        }
        if (degree == 360)
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }
    public void Bikini()
    {
        ChangeGirl(4);
    }
}
