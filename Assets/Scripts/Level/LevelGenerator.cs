using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Level
{
    public class LevelGenerator : MonoBehaviour
    {

        public string enemySpawnTag = "Enemy Spawn";
        public string itemSpawnTag = "Item Spawn";

        public float percentageOfEnemy = .1f;

        public List<Room> rooms;
        public List<Room> aisles;

        public List<GameObject> SmallMonsters;
        public List<GameObject> MediumMonsters;
        public List<GameObject> BigMonsters;

        private int smallMonsterCounter = 0;
        private int mediumMonsterCounter = 0;
        private int bigMonsterCounter = 0;

        private int roomsCreated = 0;

        public int roomsToCreate = 5;
        public float sidewaysProbability = .5f;

        public static int engine = 3;
        private static readonly List<Bounds> bounds = new List<Bounds>();

        public bool hasSiblingGenerator = true;
        private IList<Room> horizontalAisles;
        private IList<Room> verticalAisles;

        bool isOpen = false;
        public void Start()
        {

            horizontalAisles = aisles.Where(n => n.IsHorizontal).ToList();
            verticalAisles = aisles.Except(horizontalAisles).ToList();

            Vector3 worldPos = transform.position;

            Grid previousRoom = null;

            Room previous = null;
            RoomDirectionEnum? previousDirection = null;

            var directions = new RoomDirectionEnum[] { RoomDirectionEnum.LEFT, RoomDirectionEnum.RIGHT, RoomDirectionEnum.DOWN, RoomDirectionEnum.UP };

            for (int aux = 0; aux < roomsToCreate; aux++)
            {

                var isLastRoom = aux == roomsToCreate - 1;

                /// first
                /// we pick a random room
                /// and a random aisle
                /// secondly
                /// basend on the aisle type (horizontal or not)
                /// we increment worldPos vector to the RIGHT or TOP
                /// of the created room, to instantiate the next one



                var isSideways = Random.value > sidewaysProbability;
                var direction = GetNextDirection(previousDirection);


                Room aisleRoom = direction == RoomDirectionEnum.LEFT || direction == RoomDirectionEnum.RIGHT
                    ? GetRandomFromList(horizontalAisles)
                    : GetRandomFromList(verticalAisles);


                Room room = GetRandomFromList(rooms);
                


                if (previous != null)
                {
                    //var previousDoors = previousRoom.GetComponent<RoomDoors>();
                    //if (previousDoors != null && previousDirection.HasValue)
                    //{
                    //    previousDoors.OpenDoor(previousDirection.Value);
                    //}



                    if (previousDirection == RoomDirectionEnum.RIGHT)
                    {
                        worldPos += previous.Wide;
                        worldPos -= previous.VerticalOffset(room);
                        
                    }
                    else if (previousDirection == RoomDirectionEnum.LEFT)
                    {
                        worldPos -= previous.Wide;
                        worldPos -= (room.Wide - previous.Wide);
                        worldPos -= previous.VerticalOffset(room);
                    }
                    else if (previousDirection == RoomDirectionEnum.UP || previousDirection == RoomDirectionEnum.DOWN)
                    {
                        worldPos += previous.Height;
                        worldPos -= previous.HorizontalOffset(room);
                    }
                }

                //var newRoom = Instantiate(room.GridPrefab, worldPos, Quaternion.identity, transform);
                var newRoom = UnityEditor.PrefabUtility.InstantiatePrefab(room.GridPrefab) as Grid;
                UnityEditor.PrefabUtility.UnpackPrefabInstance(newRoom.gameObject, UnityEditor.PrefabUnpackMode.Completely, UnityEditor.InteractionMode.AutomatedAction);
                newRoom.transform.position = worldPos;
                Spawned(newRoom, room, worldPos);
                
                //if (aux < roomsToCreate - 1)
                if(!isLastRoom)
                {
                    if (direction == RoomDirectionEnum.RIGHT)
                    {
                        GoRight(ref worldPos, aisleRoom, room);
                    }
                    else if (direction == RoomDirectionEnum.LEFT)
                    {
                        GoLeft(ref worldPos, aisleRoom, room);
                    }
                    else if (direction == RoomDirectionEnum.DOWN || direction == RoomDirectionEnum.UP)
                    {
                        GoUp(ref worldPos, aisleRoom, room);
                        direction = RoomDirectionEnum.UP;
                    }
                }

                var doors = newRoom.GetComponent<RoomDoors>();
                if (doors != null)
                {
                    if (!isLastRoom)
                        doors.OpenDoor(direction);
                    if (previousDirection != null)
                        doors.OpenDoor(previousDirection.Value, true);
                }

                previous = aisleRoom;
                previousDirection = direction;
                previousRoom = newRoom;

                roomsCreated++;
            }

            Debug.Log(string.Format("Small: {0}", smallMonsterCounter));
            Debug.Log(string.Format("Medium: {0}", mediumMonsterCounter));
            Debug.Log(string.Format("Big: {0}", bigMonsterCounter));
        }

        private RoomDirectionEnum GetNextDirection(RoomDirectionEnum? previous)
        {
            RoomDirectionEnum[] possibleDirections = new RoomDirectionEnum[] { RoomDirectionEnum.LEFT, RoomDirectionEnum.UP, RoomDirectionEnum.RIGHT, RoomDirectionEnum.DOWN };

            if (previous.HasValue)
            {
                switch (previous)
                {
                    case RoomDirectionEnum.LEFT:
                        possibleDirections = new RoomDirectionEnum[] { RoomDirectionEnum.UP, RoomDirectionEnum.DOWN, RoomDirectionEnum.LEFT };
                        break;
                    case RoomDirectionEnum.RIGHT:
                        possibleDirections = new RoomDirectionEnum[] { RoomDirectionEnum.UP, RoomDirectionEnum.DOWN, RoomDirectionEnum.RIGHT };
                        break;
                    case RoomDirectionEnum.UP:
                        possibleDirections = new RoomDirectionEnum[] { RoomDirectionEnum.LEFT, RoomDirectionEnum.RIGHT, RoomDirectionEnum.UP };
                        break;
                    case RoomDirectionEnum.DOWN:
                        possibleDirections = new RoomDirectionEnum[] { RoomDirectionEnum.LEFT, RoomDirectionEnum.RIGHT, RoomDirectionEnum.DOWN };
                        break;
                }
            }

            return possibleDirections[Random.Range(0, possibleDirections.Length)];
        }

        private Grid GoLeft(ref Vector3 worldPos, Room aisleRoom, Room room)
        {
            Grid newRoom;
            worldPos -= room.Wide;
            worldPos += room.VerticalOffset(aisleRoom);

            worldPos += (room.Wide - aisleRoom.Wide);

            //worldPos -= aisleRoom.Wide;

            newRoom = Instantiate(aisleRoom.GridPrefab, worldPos, Quaternion.identity, transform);
            Spawned(newRoom, aisleRoom, worldPos, true);
            //worldPos += aisleRoom.Wide;



            return newRoom;
        }

        private Grid GoUp(ref Vector3 worldPos, Room aisleRoom, Room room)
        {
            Grid newRoom;
            worldPos += room.Height;
            worldPos += room.HorizontalOffset(aisleRoom);

            newRoom = Instantiate(aisleRoom.GridPrefab, worldPos, Quaternion.identity, transform);
            Spawned(newRoom, aisleRoom, worldPos, true);
            // moved to next for (room creation)
            //worldPos += aisleRoom.Height;
            return newRoom;
        }

        private Grid GoRight(ref Vector3 worldPos, Room aisleRoom, Room room)
        {
            Grid newRoom;
            worldPos += room.Wide;
            worldPos += room.VerticalOffset(aisleRoom);

            newRoom = Instantiate(aisleRoom.GridPrefab, worldPos, Quaternion.identity, transform);
            Spawned(newRoom, aisleRoom, worldPos, true);
            // moved to next for (room creation)
            //worldPos += aisleRoom.Wide;
            return newRoom;
        }

        private T GetRandomFromList<T>(IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public void Spawned(Grid createdObj, Room room, Vector3 worldPos, bool isAisle = false)
        {


            var enemies = GameObject.FindGameObjectsWithTag(enemySpawnTag);
            var tospawn = Random.Range(1, enemies.Length);
            

            for (int aux = 0; aux < enemies.Length; aux++)
            {
                var pos = enemies[aux];
                if (aux < tospawn && roomsCreated > 0)
                {
                    GameObject monsterPrefab = null;
                    var rnd = Random.value;
                    if (rnd > .5)
                    {
                        smallMonsterCounter++;
                        monsterPrefab = SmallMonsters[Random.Range(0, SmallMonsters.Count)];
                        var monster = Instantiate(monsterPrefab, pos.transform.position, Quaternion.identity, transform);
                        monster.AddComponent<Actions.Movement>();
                        var action = monster.AddComponent<Actions.AvoidAction>();
                        action.vigilantDistance = Random.Range(3, 12);
                        action.speed = Random.Range(500, 750f);
                        action.keepOriginalPosition = Random.value > .8f;
                    }
                    else if (rnd <= .5 && rnd >= .2)
                    {
                        mediumMonsterCounter++;
                        monsterPrefab = MediumMonsters[Random.Range(0, MediumMonsters.Count)];
                        var monster = Instantiate(monsterPrefab, pos.transform.position, Quaternion.identity, transform);
                        monster.AddComponent<Actions.Movement>();
                        var chase = monster.AddComponent<Actions.ChaseAction>();
                        chase.vigilantDistance = Random.Range(6, 12);
                        chase.speed = Random.Range(600, 850f);
                        chase.keepOriginalPosition = Random.value > .5f;
                    }
                    else
                    {
                        bigMonsterCounter++;
                        monsterPrefab = BigMonsters[Random.Range(0, BigMonsters.Count)];
                        var monster = Instantiate(monsterPrefab, pos.transform.position, Quaternion.identity, transform);
                        monster.AddComponent<Actions.Movement>();
                        var chase = monster.AddComponent<Actions.ChaseAction>();
                        chase.vigilantDistance = Random.Range(6, 8);
                        chase.speed = Random.Range(600, 850f);
                        chase.keepOriginalPosition = Random.value > .9f;
                    }

                    
                }

                DestroyImmediate(pos.gameObject);
            }


            var a = 0;
            if (a <= 0)
                return;


            if (isAisle)
            {
                var smallMonstersCount = Random.Range(1, 6);

                for (int aux = 0; aux < smallMonstersCount; aux++)
                {
                    var randomMonster = Random.Range(0, SmallMonsters.Count);
                    var randomX = Random.Range(1, room.GridSize.x);
                    var randomY = Random.Range(1, room.GridSize.y);
                    var pos = room.GridPrefab.CellToWorld(new Vector3Int(randomX, randomY, 0));

                    var monster = Instantiate(SmallMonsters[randomMonster], pos + worldPos, Quaternion.identity, transform);

                    monster.AddComponent<Actions.Movement>();

                    if(Random.value > .7f)
                    {
                        var chase = monster.AddComponent<Actions.ChaseAction>();
                        chase.vigilantDistance = Random.Range(3, 12);
                        chase.speed = Random.Range(500, 750f);
                        chase.keepOriginalPosition = Random.value > .5f;
                    }
                    else
                    {
                        var chase = monster.AddComponent<Actions.AvoidAction>();
                        chase.vigilantDistance = Random.Range(3, 12);
                        chase.speed = Random.Range(500, 750f);
                        chase.keepOriginalPosition = Random.value > .5f;
                    }
                    

                }
            }
            else
            {
                var mediumMonsterCount = Random.Range(1, (room.GridSize.x * room.GridSize.y) / 20);

                for (int aux = 0; aux < mediumMonsterCount; aux++)
                {
                    var randomMonster = Random.Range(0, MediumMonsters.Count);
                    var randomX = Random.Range(1, room.GridSize.x);
                    var randomY = Random.Range(1, room.GridSize.y);
                    var pos = room.GridPrefab.CellToWorld(new Vector3Int(randomX, randomY, 0));

                    var monster = Instantiate(MediumMonsters[randomMonster], pos + worldPos, Quaternion.identity, transform);

                    monster.AddComponent<Actions.Movement>();

                    var chase = monster.AddComponent<Actions.ChaseAction>();
                    chase.vigilantDistance = Random.Range(6, 12);
                    chase.speed = Random.Range(600, 850f);
                    chase.keepOriginalPosition = Random.value > .5f;
                }
            }
        }

        public void SpawnGenerator(Vector3 worldPos, Room room)
        {
            sidewaysProbability = .9f;

            hasSiblingGenerator = true;
            var w = worldPos + room.Wide;
            //w += worldPos + rooms[randomRoom].VerticalOffset(aisles[aisle]);
            var engine = new GameObject("other generator");
            var generator = engine.AddComponent<LevelGenerator>();
            engine.transform.position = w;
            generator.sidewaysProbability = .1f;
            generator.SmallMonsters = SmallMonsters;
            generator.MediumMonsters = MediumMonsters;
            generator.BigMonsters = BigMonsters;
            generator.rooms = rooms;
            generator.aisles = aisles;
            generator.roomsToCreate = roomsToCreate;
        }

    }

}