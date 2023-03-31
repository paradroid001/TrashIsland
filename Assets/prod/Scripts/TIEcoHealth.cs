using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    [CreateAssetMenu(fileName = "New TIEcoHealth Asset", menuName = "TrashIsland/EcoHealth", order = 1)]
    public class TIEcoHealth : ScriptableObject
    {
        public string terrainObjectName;
        public string trashObjectTagName;
        private Terrain _terrain;
        private List<GameObject> _trackedObjects;

        private float[,,] element;
        private float[,,] map;
        private float[,] _ecoHealth; //The health at this X/Y coordinate
        //private int mapX;
        //private int mapY;
        private TerrainData _terrainData;
        private Vector3 _terrainPosition;
        private Vector3 lastPos;

        void OnEnable()
        {
            if (_terrain == null)
            {
                GameObject t = GameObject.Find(terrainObjectName);
                if (t != null)
                {
                    _terrain = t.GetComponent<Terrain>();
                    _terrainData = _terrain.terrainData;
                    _terrainPosition = _terrain.transform.position;
                    Debug.Log("Got terrain");
                }
                else
                {
                    Debug.Log("Could not find terrain");
                }
            }
            else
            {
                Debug.Log("terrain already initialised");
            }
            if (ecoHealth == null)
            {
                if (_terrain == null)
                {
                    Debug.Log("Cannot init ecohealth without terrain");
                }
                else
                {
                    _ecoHealth = new float[_terrainData.size.x, _terrainData.size.z];
                    for (int x = 0; x < _terrainData.size.x; x++)
                    {
                        for (int y = 0; y < _terrainData.size.z; y++)
                        {
                            _ecoHealth[x, y] = 0.0f; //default value
                        }
                    }
                }
            }
            //map = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, terrain.terrainData.alphamapLayers];
            //element = new float[1, 1, terrain.terrainData.alphamapLayers];
            //
            //lastPos = transform.position;


            /*
            terrainPosition = terrain.transform.position;

            Debug.Log("EcoHealth:");
            //Debug.Log($"Player position: {transform.position}");
            Debug.Log($"Terrain position: {terrainPosition}");
            Debug.Log($"Terrain Data Size: {terrainData.size}");
            Debug.Log($"Terrain Alphamap Data Size: {terrainData.alphamapWidth}, {terrainData.alphamapHeight}");
            */

        }

        void Awake()
        {
            Debug.Log("Terrain SI awoke");
        }

        void Reset()
        {
            Debug.Log("We did a reset");
        }

        // Update is called once per frame
        void Update()
        {
            UpdateMapOnTheTarget();
            Debug.Log("Terrain SI Update");
        }

        private void UpdateMapOnTheTarget()
        {
            /*
            // only update if we move
            if (Vector3.Distance(transform.position, lastPos) > 1)
            {
                // convert world coords to terrain coords
                mapX = (int)(((transform.position.x - terrainPosition.x) / terrainData.size.x) * terrainData.alphamapWidth);
                mapY = (int)(((transform.position.z - terrainPosition.z) / terrainData.size.z) * terrainData.alphamapHeight);
                Debug.Log($"MapX,Y = {mapX}, {mapY} vs my pos: {(int)transform.position.x}, {(int)transform.position.z}");

                map[mapY, mapX, 0] = element[0, 0, 0] = 0;
                map[mapY, mapX, 1] = element[0, 0, 1] = 1;

                terrain.terrainData.SetAlphamaps(mapX, mapY, element);
                //Debug.Log($"Set terraindata at {mapX}, {mapY}");

                lastPos = transform.position;
            }
            */
        }

    }
}