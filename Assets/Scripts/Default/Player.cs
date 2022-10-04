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
    CameraController cameraController;
    SoundManager soundManager;
    UIBar bar;
    URPPP effect;
    // private int walkType;
    // public int WalkType
    // {
    //     get { return walkType; }
    //     set
    //     {
    //         walkType = value;
    //         walkType = Mathf.Clamp(walkType, 0, 4);
    //         animationController.SetWalk(walkType);
    //     }
    // }
    private float famous;
    public float Famous
    {
        get { return famous; }
        set
        {
            // famous = value;
            famous = Mathf.Clamp(value, 0, 100);
            animationController.SetWalk(Mathf.CeilToInt(famous / 100 * 5 - 1f));
            // switch (famous)
            // {
            //     case < 20: animationController.SetWalk(0); break;
            //     case < 40: animationController.SetWalk(1); break;
            //     case < 60: animationController.SetWalk(2); break;
            //     case < 80: animationController.SetWalk(3); break;
            //     case > 80: animationController.SetWalk(4); break;
            //     default: break;
            // }
            CanvasManager.Instance.HudFamous(value);
        }
    }


    private int likeCount;
    public int LikeCount
    {
        get { return likeCount; }
        set
        {
            likeCount = value;
            CanvasManager.Instance.HudCoin(likeCount.ToString());
        }
    }

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
        LikeCount = 0;
        Famous = 0;
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
            if ((int)collect.type < 2)
            {
                LikeCount++;
                Famous += 1;
            }
            else
            {
                LikeCount--;
                Famous -= 1;
            }
            collect.PlayerParticle();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Ice"))
        {
            animationController.FallBack();
        }
        else if (other.gameObject.CompareTag("CameraMan"))
        {
            
        }
    }
    public override void Die()
    {
        base.Die();
        GameManager.Instance.GameOver(this, EventArgs.Empty);
    }

    private void OnGamePlay(object sender, EventArgs e)
    {
        movement.SetSpeed(0.5f);
        movement.SetControlAble(true);
        // animationController.Dance();
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        // throw new NotImplementedException();
    }
}
