using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collect : MonoBehaviour
{
    [SerializeField] List<GameObject> grxs;
    [SerializeField] ParticleSystem particle;
    [SerializeField] List<Texture> particleSprites;
    public CollectType type;
    public bool typeSettled = false;
    Color _color;
    private void Start()
    {
        if (typeSettled)
        {
            SetType(type);
        }
    }
    public void PlayerParticle()
    {
        particle.Play();
        particle.transform.SetParent(null);
        Destroy(particle.gameObject, 2);
    }

    public Color GetColor()
    {
        return _color;
    }
    public void SetColor(Color incomingColor)
    {
        transform.GetChild(0).GetComponent<Renderer>().material.color = incomingColor;
        _color = incomingColor;
    }

    internal void SetFree()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().isTrigger = false;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }
    internal void BecomeTrigger()
    {
        StartCoroutine(LocalCoroutine());
        IEnumerator LocalCoroutine()
        {
            yield return new WaitForSeconds(3);
            Vector3 position = transform.position;
            position.y = 0.5f;
            transform.position = position;
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    internal void SetType(int v)
    {
        SetType((CollectType)v);
    }

    internal void SetType(CollectType v)
    {
        type = v;
        foreach (GameObject child in grxs)
        {
            child.SetActive(false);
        }
        grxs[(int)type].SetActive(true);
        particle.GetComponent<ParticleSystemRenderer>().material.SetTexture("_BaseMap", particleSprites[(int)type]);
    }

    internal void HideCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
}
public enum CollectType
{
    Like, Love, Poop, Angry
}
