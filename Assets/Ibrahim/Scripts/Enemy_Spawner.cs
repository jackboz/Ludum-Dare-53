using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] Crate cart;
    [SerializeField] private Enemy prefab;
    [SerializeField] private Enemy prefab2;
    [SerializeField] private int enemyCount;
    [SerializeField] private int enemyCount2;
    [HideInInspector] public List<Enemy> enemyList;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitUntil(() => enemyList.Count == 0);
            yield return new WaitForSeconds(8);

            Vector3 carPos = cart.transform.position;
            enemyCount = Mathf.Clamp(enemyCount + 1, 1, 7);

            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-7, 7), 1, carPos.z + 40);
                enemyList.Add(Instantiate(prefab, spawnPos, Quaternion.identity));
            }

            if (enemyCount > 4)
            {
                enemyCount2 = Mathf.Clamp(enemyCount2 + 1, 1, 4);
                for (int i = 0; i < enemyCount2; i++)
                {
                    Vector3 spawnPos = new Vector3(Random.Range(-7, 7), 1, carPos.z + 40);
                    enemyList.Add(Instantiate(prefab2, spawnPos, Quaternion.identity));
                }
            }
        }
    }
}
