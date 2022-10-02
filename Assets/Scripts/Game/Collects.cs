using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collects : MonoBehaviour
{
    [SerializeField] List<Collect> myCollects;
    // Start is called before the first frame update
    void Start()
    {

        foreach (var item in myCollects)
        {
            if (!item.typeSettled)
            {
                item.SetType(Random.Range(0, 4));
                // item.typeSettled = true;
            }
        }

    }
}
