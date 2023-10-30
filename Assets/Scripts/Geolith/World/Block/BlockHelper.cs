using Geolith.World.Chunks;
using UnityEngine;

namespace Geolith.World.Block
{
    public static class BlockHelper
    {
        private static readonly Direction[] Directions =
        {
            Direction.Backwards,
            Direction.Down,
            Direction.Forward,
            Direction.Left,
            Direction.Right,
            Direction.Up
        };

        public static MeshData GetMeshData(ChunkData chunk, int x, int y, int z, MeshData meshData, BlockType blockType)
        {
            if (blockType is BlockType.Air or BlockType.Nothing)
                return meshData;

            foreach (var direction in Directions)
            {
                var neighbourBlockCoordinates = new Vector3Int(x, y, z) + direction.GetVector();
                var neighbourBlockType = Chunk.GetBlockFromChunkCoordinates(chunk, neighbourBlockCoordinates);

                if (neighbourBlockType != BlockType.Nothing && // todo: fix
                    !BlockDataManager.BlockTextureDataDictionary[neighbourBlockType].isSolid)
                {
                    if (blockType == BlockType.Water && neighbourBlockType == BlockType.Air)
                        meshData.WaterMeshData = GetFaceDataIn(direction, chunk, x, y, z, meshData.WaterMeshData, blockType);
                    else
                        meshData = GetFaceDataIn(direction, chunk, x, y, z, meshData, blockType);
                }
            }

            return meshData;
        }

        // TODO: check why chunk is not used
        public static MeshData GetFaceDataIn(Direction direction, ChunkData chunk, int x, int y, int z,
            MeshData meshData, BlockType blockType)
        {
            GetFaceVertices(direction, x, y, z, meshData, blockType);
            meshData.AddQuadTriangles(BlockDataManager.BlockTextureDataDictionary[blockType].generateCollider);
            meshData.Uvs.AddRange(FaceUVs(direction, blockType));

            return meshData;
        }

        public static void GetFaceVertices(Direction direction, int x, int y, int z, MeshData meshData, BlockType blockType)
        {
            var generateCollider = BlockDataManager.BlockTextureDataDictionary[blockType].generateCollider;
            //order of vertices matters for the normals and how we render the mesh
            switch (direction)
            {
                case Direction.Backwards:
                    meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), generateCollider);
                    break;
                case Direction.Forward:
                    meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), generateCollider);
                    break;
                case Direction.Left:
                    meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), generateCollider);
                    break;

                case Direction.Right:
                    meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), generateCollider);
                    break;
                case Direction.Down:
                    meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), generateCollider);
                    break;
                case Direction.Up:
                    meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), generateCollider);
                    meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), generateCollider);
                    break;
                default:
                    break;
            }
        }

        public static Vector2[] FaceUVs(Direction direction, BlockType blockType)
        {
            var uvs = new Vector2[4];
            var tilePos = TexturePosition(direction, blockType);
            
            // TODO: this need to be reworked

            uvs[0] = new Vector2(
                BlockDataManager.TileSize.x * tilePos.x + BlockDataManager.TileSize.x - BlockDataManager.TextureOffset,
                BlockDataManager.TileSize.y * tilePos.y + BlockDataManager.TextureOffset
            );

            uvs[1] = new Vector2(
                BlockDataManager.TileSize.x * tilePos.x + BlockDataManager.TileSize.x - BlockDataManager.TextureOffset,
                BlockDataManager.TileSize.y * tilePos.y + BlockDataManager.TileSize.y - BlockDataManager.TextureOffset
            );

            uvs[2] = new Vector2(
                BlockDataManager.TileSize.x * tilePos.x + BlockDataManager.TextureOffset,
                BlockDataManager.TileSize.y * tilePos.y + BlockDataManager.TileSize.y - BlockDataManager.TextureOffset
            );

            uvs[3] = new Vector2(
                BlockDataManager.TileSize.x * tilePos.x + BlockDataManager.TextureOffset,
                BlockDataManager.TileSize.y * tilePos.y + BlockDataManager.TextureOffset
            );

            return uvs;
        }

        public static Vector2Int TexturePosition(Direction direction, BlockType blockType)
        {
            // TODO: rework for multiface textures
            return direction switch
            {
                Direction.Up => BlockDataManager.BlockTextureDataDictionary[blockType].up,
                Direction.Down => BlockDataManager.BlockTextureDataDictionary[blockType].down,
                _ => BlockDataManager.BlockTextureDataDictionary[blockType].side
            };
        }
    }
}