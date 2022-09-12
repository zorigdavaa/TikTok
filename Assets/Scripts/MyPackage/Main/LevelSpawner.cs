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
        [SerializeField] List<GameObject> Boxes;
        [SerializeField] GameObject Hana;
        List<Vector3> points;

        public void Init()
        {

        }
        private void Start()
        {
            InstantiateHana(5);
            InstantiateBox(10);
            InstantiateCollect(10);
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
        float lastCollect = 15;
        private void InstantiateCollect(int v)
        {
            for (int i = 0; i < v; i++)
            {
                int random = Random.Range(0, Collects.Count);
                Instantiate(Collects[random], new Vector3(0, 0, lastCollect), Quaternion.identity, transform);
                lastCollect += 20;
            }
        }
        float lastBoxPos = 5f;
        private void InstantiateBox(int v)
        {
            for (int i = 0; i < v; i++)
            {
                int random = Random.Range(0, Boxes.Count);
                Instantiate(Boxes[random], new Vector3(Random.Range(-2, 2), 0, lastBoxPos), Quaternion.identity, transform);
                lastBoxPos += 10;
            }
        }

    }
}

