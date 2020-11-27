using System.Linq;
using Assets.Scripts.Enemies;
using Level;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelGeneratorByAsset : LevelGenerator
    {
        public LevelAsset level;
        private int smallMonsters;
        private int mediumMonsters;
        private int bigMonsters;
        private int createdRooms = 0;
        private int createdAisles = 0;


        public LevelGeneratorByAsset()
        {

        }

        public override void Start()
        {

            LoadAsset();

            base.Start();
        }

        private void LoadAsset()
        {
            if (level == null)
                Debug.LogError("Level is not set");

            aisles = level.aisles.ToList();
            rooms = level.rooms.ToList();
            sidewaysProbability = level.sidewaysProbability;
            roomsToCreate = level.numberOfRooms;

            smallMonsters = level.numberOfSmallMonsters;
            mediumMonsters = level.numberOfMediumMonsters;
            bigMonsters = level.numberOfBigMonsters;
        }

        public override void Spawned(Grid createdObj, Room room, Vector3 worldPos, bool isAisle = false)
        {
            if (isAisle)
                createdAisles++;
            else
                createdRooms++;

            if (createdRooms == roomsToCreate)
                LastRoom();
            
        }

        public void LastRoom()
        {
            var _smallEnemies = level.monsters.Where(n => n.size == Monster.Size.Small).ToArray();
            var _mediumEnemies = level.monsters.Where(n => n.size == Monster.Size.Medium).ToArray();
            var _bigEnemies = level.monsters.Where(n => n.size == Monster.Size.Big).ToArray();
            // all enemies
            var enemiesTag = GameObject.FindGameObjectsWithTag(enemySpawnTag);
            

            for (int aux = enemiesTag.Length; aux > 0; aux--)
            {
                var position = Random.Range(0, aux);

                var temp = enemiesTag[aux - 1];
                enemiesTag[aux - 1] = enemiesTag[position];
                enemiesTag[position] = temp;

                var tag = enemiesTag[aux - 1];

                if (smallMonsters >= 0)
                {
                    //TODO: explains
                    smallMonsters--;

                    var randomAsset = Random.Range(0, _smallEnemies.Length);
                    var asset = _smallEnemies[randomAsset];
                    var prefab = asset.prefabs[Random.Range(0, asset.prefabs.Length)];
                    var monster = MonsterFactory.Create(prefab, asset.minHealth, asset.maxHealth);
                    monster.transform.position = tag.transform.position;
                    DestroyImmediate(tag.gameObject);
                }
                else if (mediumMonsters >= 0)
                {
                    mediumMonsters--;
                }
                else if (bigMonsters >= 0)
                {
                    bigMonsters--;
                }
                else
                {
                    break;
                }

            }

        }
    }
}
