using System.Collections.Generic;
using Geolith.World.Block;
using Geolith.World.Chunks;
using UnityEngine;

namespace Geolith.World
{
    public class World : MonoBehaviour
    {
        public int mapSizeInChunks = 10;
        public int chunkSize = 16;
        public int chunkHeight = 384;
        public int waterThreshold = 50;
        public float noiseScale = 0.03f;
        public GameObject chunkPrefab;
        private GameObject _chunksParent;
        // public int chunkDrawingRange = 12; // TODO: make this a settings

        public Dictionary<Vector3Int, ChunkData> ChunkDataDictionary = new();
        public Dictionary<Vector3Int, ChunkRenderer> ChunkDictionary = new();

        private void Start()
        {
            _chunksParent = transform.GetChild(0).gameObject;
            // TODO: move this later
            GenerateWorld();
        }

        public void GenerateWorld()
        {
            ChunkDataDictionary.Clear();
            foreach (var chunkRenderer in ChunkDictionary.Values)
            {
                Destroy(chunkRenderer.gameObject);
            }

            ChunkDictionary.Clear();

            for (var x = 0; x < mapSizeInChunks; x++)
            {
                for (var z = 0; z < mapSizeInChunks; z++)
                {
                    var data = new ChunkData(chunkSize, chunkHeight, this, new Vector3Int(x * chunkSize, 0, z * chunkSize));
                    GenerateVoxels(data);
                    ChunkDataDictionary.Add(data.Position, data);
                }
            }

            foreach (var chunkData in ChunkDataDictionary.Values)
            {
                var meshData = Chunk.GetChunkMeshData(chunkData);
                var chunkObject = Instantiate(chunkPrefab, chunkData.Position, Quaternion.identity);
                chunkObject.transform.parent = _chunksParent.transform;
                var chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();

                ChunkDictionary.Add(chunkData.Position, chunkRenderer);
                chunkRenderer.InitializeChunk(chunkData);
                chunkRenderer.UpdateChunk(meshData);
            }
        }

        private void GenerateVoxels(ChunkData data)
        {
            for (var x = 0; x < data.ChunkSize; x++)
            {
                for (var z = 0; z < data.ChunkSize; z++)
                {
                    var noiseValue = Mathf.PerlinNoise((data.Position.x + x) * noiseScale, (data.Position.z + z) * noiseScale);
                    var groundPosition = Mathf.RoundToInt(noiseValue * chunkHeight);
                    for (int y = 0; y < chunkHeight; y++)
                    {
                        var voxelType = BlockType.Dirt;
                        if (y > groundPosition)
                            voxelType = /* y < waterThreshold ? BlockType.Water : */BlockType.Air;
                        else if (y == groundPosition)
                            voxelType = BlockType.Grass;

                        Chunk.SetBlock(data, new Vector3Int(x, y, z), voxelType);
                    }
                }
            }
        }

        // TODO: check why chunkData is not used
        internal BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
        {
            var pos = Chunk.ChunkPositionFromBlockCoords(this, x, y, z);
            ChunkDataDictionary.TryGetValue(pos, out ChunkData containerChunk);

            if (containerChunk == null)
                return BlockType.Nothing; // TODO: should be nothing?
            var blockInChunkCoordinates = Chunk.GetBlockInChunkCoordinates(containerChunk, new Vector3Int(x, y, z));
            return Chunk.GetBlockFromChunkCoordinates(containerChunk, blockInChunkCoordinates);
        }
    }
}