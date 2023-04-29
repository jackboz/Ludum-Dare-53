using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] Cart cart;
    [SerializeField] private Enemy prefab;
    [SerializeField] private int enemyCount;
    [HideInInspector] public List<Enemy> enemyList;

    IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitUntil(() => enemyList.Count == 0);
            yield return new WaitForSeconds(8);

            Vector3 carPos = cart.transform.position;
            enemyCount = Mathf.Clamp(enemyCount + 1, 1, 10);

            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-7, 7), 1, carPos.z + 40);
                enemyList.Add(Instantiate(prefab, spawnPos, Quaternion.identity));
            }
        }
    }
}
