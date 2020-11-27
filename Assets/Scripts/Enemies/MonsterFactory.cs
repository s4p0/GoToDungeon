using Actions;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public static class MonsterFactory
    {
        public static GameObject Create(GameObject prefab, int minHealth = 10, int maxHealth = 20, GameObject[] attachPrefabs = null)
        {
            var objMonster = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            var monster = objMonster.AddComponent<Monster>();

            var life = Random.Range(minHealth, maxHealth); 
            monster.TotalHealth = life;

            var rb = objMonster.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.mass = life * 5;
            //rb.drag = rb.mass / 4.0f;
            rb.drag = 2.5f;
            rb.freezeRotation = true;

            objMonster.AddComponent<BoxCollider2D>();
            objMonster.AddComponent<Movement>();
            objMonster.AddComponent<ChaseAction>().speed = rb.mass;
            


            if (attachPrefabs != null)
            {
                foreach (var item in attachPrefabs)
                {
                    Object.Instantiate(item, objMonster.transform);
                }
            }

            //healthBar.Initialize();
            //return healthBar.gameObject;

            return objMonster;
        }

        public static GameObject Create(MonsterAsset asset, GameObject[] attachPrefabs = null)
        {
            var toGenerate = Random.Range(0, asset.prefabs.Length);
            return Create(asset.prefabs[toGenerate], asset.minHealth, asset.maxHealth, attachPrefabs);
        }
    }
}
