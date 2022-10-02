using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using ZPackage.Utility;

namespace ZPackage
{
    public class LevelSpawner : GenericSingleton<LevelSpawner>
    {
        [SerializeField] List<GameObject> Collects;
        [SerializeField] GameObject Gate;
        [SerializeField] GameObject Hana;
        List<Vector3> points;

        public void Init()
        {

        }
        private void Start()
        {
            // InstantiateHana(5);
            InstantiateGate(5);
            InstantiateCollect(20);
        }
        float lastHanaPos = 35;
        private void InstantiateHana(int v)
        {
            for (int i = 0; i < v; i++)
            {
                Instantiate(Hana, new Vector3(0, 0, lastHanaPos), Quaternion.identity, transform);
                lastHanaPos += 35;
            }
        }
        float lastCollect = 20;
        private void InstantiateCollect(int v)
        {
            for (int i = 0; i < v; i++)
            {
                int random = Random.Range(0, Collects.Count);
                GameObject collectParent = Instantiate(Collects[random], new Vector3(0, 0, lastCollect), Quaternion.identity, transform);
                // foreach (Transform item in collectParent.transform)
                // {
                //     item.GetComponent<Collect>().SetType(Random.Range(0, 4));
                // }
                lastCollect += 20;
            }
        }
        float lastGatePos = 50f;
        private void InstantiateGate(int v)
        {
            for (int i = 0; i < v; i++)
            {
                // int random = Random.Range(0, Boxes.Count);
                Instantiate(Gate, new Vector3(0, 0, lastGatePos), Quaternion.identity, transform);
                lastGatePos += 50;
            }
        }
    }
}

