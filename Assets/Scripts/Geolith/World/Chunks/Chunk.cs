using System;
using Geolith.World.Block;
using UnityEngine;

namespace Geolith.World.Chunks
{
    public static class Chunk
    {
        public static void LoopThroughTheBlocks(ChunkData chunkData, Action<int, int, int> actionCallback)
        {
            for (var index = 0; index < chunkData.Blocks.Length; index++)
            {
                var position = GetPositionFromIndex(chunkData, index);
                actionCallback(position.x, position.y, position.z);
            }
        }

        private static Vector3Int GetPositionFromIndex(ChunkData chunkData, int index)
        {
            var x = index % chunkData.ChunkSize;
            var y = (index / chunkData.ChunkSize) % chunkData.ChunkHeight;
            var z = index / (chunkData.ChunkSize * chunkData.ChunkHeight);
            return new Vector3Int(x, y, z);
        }

        //in chunk coordinate system
        private static bool InRange(ChunkData chunkData, int axisCoordinate)
        {
            return !(axisCoordinate < 0 || axisCoordinate >= chunkData.ChunkSize);
        }

        //in chunk coordinate system
        private static bool InRangeHeight(ChunkData chunkData, int yCoordinate)
        {
            return !(yCoordinate < 0 || yCoordinate >= chunkData.ChunkHeight);
        }

        public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int chunkCoordinates)
        {
            return GetBlockFromChunkCoordinates(chunkData, chunkCoordinates.x, chunkCoordinates.y, chunkCoordinates.z);
        }

        public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
        {
            if (InRange(chunkData, x) && InRangeHeight(chunkData, y) && InRange(chunkData, z))
            {
                var index = GetIndexFromPosition(chunkData, x, y, z);
                return chunkData.Blocks[index];
            }

            return chunkData.World.GetBlockFromChunkCoordinates(chunkData, chunkData.Position.x + x, chunkData.Position.y + y, chunkData.Position.z + z);
        }

        public static void SetBlock(ChunkData chunkData, Vector3Int localPosition, BlockType block)
        {
            if (InRange(chunkData, localPosition.x) && InRangeHeight(chunkData, localPosition.y) && InRange(chunkData, localPosition.z))
            {
                var index = GetIndexFromPosition(chunkData, localPosition.x, localPosition.y, localPosition.z);
                chunkData.Blocks[index] = block;
            }
            else
            {
                // TODO: fix
                throw new NotImplementedException("Chunk.SetBlock: not implemented");
                // WorldDataHelper.SetBlock(chunkData.worldReference, localPosition, block);
            }
        }

        private static int GetIndexFromPosition(ChunkData chunkData, Vector3Int pos)
        {
            return GetIndexFromPosition(chunkData, pos.x, pos.y, pos.z);
        }

        private static int GetIndexFromPosition(ChunkData chunkData, int x, int y, int z)
        {
            return x + chunkData.ChunkSize * y + chunkData.ChunkSize * chunkData.ChunkHeight * z;
        }

        public static Vector3Int GetBlockInChunkCoordinates(ChunkData chunkData, Vector3Int pos)
        {
            return pos - chunkData.Position;
            // return new Vector3Int
            // {
            // x = pos.x - chunkData.Position.x,
            // y = pos.y - chunkData.Position.y,
            // z = pos.z - chunkData.Position.z
            // };
        }

        public static MeshData GetChunkMeshData(ChunkData chunkData)
        {
            MeshData meshData = new();

            LoopThroughTheBlocks(chunkData, (x, y, z) =>
            {
                meshData = BlockHelper.GetMeshData(chunkData, x, y, z, meshData, chunkData.Blocks[GetIndexFromPosition(chunkData, x, y, z)]);
            });

            return meshData;
        }

        internal static Vector3Int ChunkPositionFromBlockCoords(World world, int x, int y, int z)
        {
            Vector3Int pos = new Vector3Int
            {
                x = Mathf.FloorToInt(x / (float)world.chunkSize) * world.chunkSize,
                y = Mathf.FloorToInt(y / (float)world.chunkHeight) * world.chunkHeight,
                z = Mathf.FloorToInt(z / (float)world.chunkSize) * world.chunkSize
            };
            return pos;
        }
    }
}
