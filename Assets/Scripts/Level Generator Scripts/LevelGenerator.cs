using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level_Generator_Scripts
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int levelLength;
        [SerializeField] private int startStartPlatformLength = 5, endPlatformLength = 5;
        [SerializeField] private int distance_between_platfomrs;
        [SerializeField] private Transform platformPrefab, platform_parent;
        [SerializeField] private Transform monster, monster_parent; 
        [SerializeField] private Transform health_Collectable, healthCollectable_parent;
        [SerializeField] private float platformPosition_MinY = 0f, platformPosition_MaxY = 10f;
        [SerializeField] private int platformLength_Min = 1, platformLength_Max = 4;
        [SerializeField] private float chanceForMonsterExistence = 0.25f, chanceForCollectableExistence = 0.1f;
        [SerializeField] private float healthCollectable_MinY = 1f, healthCollectable_MaxY = 3f;
        private float platformPositionX;

        private enum PlatformType
        {
            None,
            Flat
        }

        private class PlatformPositionInfo {
            public PlatformType platformType;
            public float positionY;
            public bool hasMonster;
            public bool hasHealthCollectable;

            public PlatformPositionInfo(PlatformType type, float posY, bool has_monster, bool has_collectable) {
                platformType = type;
                positionY = posY;
                hasMonster = has_monster;
                hasHealthCollectable = has_collectable;

            }
        }// class platform position info

        void Start()
        {
            GenerateLevel();
        }

        void FillOutPositionInfo(PlatformPositionInfo[] platformPositionInfos)
                {
                    int currentPlatformIndex = 0;

                    for (int i = 0; i < startStartPlatformLength; i++)
                    {
                        platformPositionInfos[currentPlatformIndex].platformType = PlatformType.Flat;
                        platformPositionInfos[currentPlatformIndex].positionY = 0f;

                        currentPlatformIndex++;
                    }

                    while (currentPlatformIndex < levelLength - endPlatformLength)
                    {
                        if (platformPositionInfos[currentPlatformIndex - 1].platformType != PlatformType.None)
                        {
                            currentPlatformIndex++;
                            continue;
                        }

                        float platformPositionY = Random.Range(platformPosition_MinY, platformPosition_MaxY);
                        int platformLength = Random.Range(platformLength_Min, platformLength_Max);

                        for (int i = 0; i < platformLength; i++)
                        {
                            bool has_Monster = (Random.Range(0f, 1f) < chanceForMonsterExistence);
                            bool has_healthCollectable = (Random.Range(0f, 1f) < chanceForCollectableExistence);

                            platformPositionInfos[currentPlatformIndex].platformType = PlatformType.Flat;
                            platformPositionInfos[currentPlatformIndex].positionY = platformPositionY;
                            platformPositionInfos[currentPlatformIndex].hasMonster = has_Monster;
                            platformPositionInfos[currentPlatformIndex].hasHealthCollectable = has_healthCollectable;

                            currentPlatformIndex++;

                            if (currentPlatformIndex > (levelLength - endPlatformLength))
                            {
                                currentPlatformIndex = levelLength - endPlatformLength;
                                break;
                            }

                        }

                        for (int i = 0; i < endPlatformLength; i++)
                        {
                            platformPositionInfos[currentPlatformIndex].platformType = PlatformType.Flat;
                            platformPositionInfos[currentPlatformIndex].positionY = 0f;

                            currentPlatformIndex++;

                        }
                    }// while
                }
        

    void CreatePlatformPositionInfo(PlatformPositionInfo [] platformPositionInfos)
        {
            for (int i = 0; i < platformPositionInfos.Length; i++)
            {
                PlatformPositionInfo platformPositionInfo = platformPositionInfos[i];
                if (platformPositionInfo.platformType == PlatformType.None)
                {
                    continue;   
                }

                Vector3 platformPosition;
                platformPosition = new Vector3(distance_between_platfomrs * i, platformPositionInfo.positionY, 0f);
                
                Transform createBlock = (Transform) Instantiate(platformPrefab, platformPosition, Quaternion.identity);
                createBlock.parent = platform_parent;

                if (platformPositionInfo.hasMonster)
                {
                    //Code later
                }
                
                if (platformPositionInfo.hasHealthCollectable)
                {
                    //Code later
                }
                
            }// for loop
        }

        public void GenerateLevel()
        {
            PlatformPositionInfo[] platformPositionInfos = new PlatformPositionInfo[levelLength];
            for (int i = 0; i < platformPositionInfos.Length; i++)
            {
                platformPositionInfos[i] = new PlatformPositionInfo(PlatformType.None, -1f, false, false);
            }

            FillOutPositionInfo(platformPositionInfos);
            CreatePlatformPositionInfo(platformPositionInfos);
        }
    }
}// class
