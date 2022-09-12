using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZPackage;

public class Character : Mb
{
    public MovementForgeRun movement;
    public LeaderBoardData data;
    float Health = 100;
    TextMeshPro followTmp;
    public AnimationController animationController;
    public Inventory inventory;
    bool ControlAble = true;
    private Color? color = null;
    public bool IsAlive => Health > 0;
    [SerializeField] Image healthBar;
    public Color GetColor()
    {
        if (color == null)
        {
            color = transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color;
        }
        return color.Value;
    }

    public void SetColor(Color incomingColor)
    {
        color = incomingColor;
        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = incomingColor;
        data.col = incomingColor;
    }

    protected virtual void CollectBone(Collider other)
    {
        if (other.gameObject.CompareTag("Collect"))
        {
            inventory.AddInventory(other.gameObject);
            data.score++;
        }
    }
    public float GetHealth()
    {
        return Health;
    }
    public void SetHealth(float value)
    {
        Health = value;
        healthBar.fillAmount = Health * 0.01f;
    }
    public void TakeDamage(float amount)
    {
        SetHealth(Health += amount);
        if (Health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        animationController.Die();
        gameObject.layer = 2;
        movement.Cancel();
        rb.isKinematic = true;
        healthBar.transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectBone(other);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.transform.GetComponent<Character>() && other.transform.GetComponent<Character>().inventory.Count > inventory.Count)
    //     {
    //         Fall();
    //     }
    // }


    public void WaitAndCallAction(float time, Action action)
    {
        StartCoroutine(LocalCoroutine());
        IEnumerator LocalCoroutine()
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }

    public void Init()
    {
        // transform.SetParent(A.BS.transform);

    }
    public void UpdateColor(Color col) { }

}