using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;
using ZPackage.Helper;
using Random = UnityEngine.Random;
using UnityEngine.Pool;

public class Player : Character
{
    [SerializeField] Transform Horn;
    CameraController cameraController;
    SoundManager soundManager;
    UIBar bar;
    URPPP effect;
    int killCount;
    int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MovementForgeRun>();
        soundManager = FindObjectOfType<SoundManager>();
        cameraController = FindObjectOfType<CameraController>();
        // line.positionCount = LineResolution;
        effect = FindObjectOfType<URPPP>();
        // bar = FindObjectOfType<UIBar>();
        // bar.gameObject.SetActive(false);
        GameManager.Instance.GameOverEvent += OnGameOver;
        GameManager.Instance.GamePlay += OnGamePlay;
        GameManager.Instance.LevelCompleted += OnGameOver;

        // cameraController.Zoom(0.5f, 20, () => cameraController.Zoom(1, 60));
    }

    internal void DoAction(GateType gateType)
    {
        switch (gateType)
        {
            case GateType.sword: print("sword"); break;
            case GateType.dance: animationController.Dance(); break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Collect collect = other.GetComponent<Collect>();
        if (collect)
        {
            if (collect.type == CollectType.Flower)
            {
                Horn.localScale += Vector3.one * 0.2f;
            }
            else
            {
                Horn.localScale += -Vector3.one * 0.2f;
                if (Horn.localScale.x < 0)
                {
                    Horn.localScale = Vector3.zero;
                }
            }
            // inventory.AddInventory(collect.gameObject);
            Destroy(collect.gameObject);
        }
    }
    public override void Die()
    {
        base.Die();
        GameManager.Instance.GameOver(this, EventArgs.Empty);
    }

    private void OnGamePlay(object sender, EventArgs e)
    {
        // movement.SetSpeed(1);
        // movement.SetControlAble(true);
        animationController.Dance();
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        // throw new NotImplementedException();
    }
}
