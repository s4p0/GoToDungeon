using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public static class MonsterFactory
    {
        public static GameObject Create(GameObject prefab, int minHealth = 10, int maxHealth = 20, GameObject[] attachPrefabs = null)
        {
            var life = Random.Range(minHealth, maxHealth); 

            var obj = Object.Instantiate(prefab);
            var monster = obj.AddComponent<Monster>();
            monster.TotalHealth = life;

            var rb = obj.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.mass = life * 3;
            rb.drag = rb.mass / 4.0f;
            rb.freezeRotation = true;

            obj.AddComponent<BoxCollider2D>();

            foreach (var item in attachPrefabs)
            {
                Object.Instantiate(item, obj.transform);
            }

            return obj;
        }

        public static GameObject Create(MonsterAsset asset, GameObject[] attachPrefabs = null)
        {
            var toGenerate = Random.Range(0, asset.prefabs.Length);
            return Create(asset.prefabs[toGenerate], asset.minHealth, asset.maxHealth, attachPrefabs);
        }
    }
}
