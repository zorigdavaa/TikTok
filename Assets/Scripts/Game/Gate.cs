using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] List<GameObject> subGates = new List<GameObject>(2);
    [SerializeField] List<GateType> Gates = new List<GateType>(2);
    [SerializeField] List<Sprite> GateIcons = new List<Sprite>(2);
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < subGates.Count; i++)
        {
            subGates[i].transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = GateIcons[(int)Gates[i]];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<Player>();
        if (player)
        {
            if (other.transform.position.x < 0)
            {
                player.DoAction(Gates[0]);
            }
            else
            {
                player.DoAction(Gates[1]);
            }
        }
    }
}
public enum GateType
{
    sword, dance
}
