using System.Collections.Generic;
using UnityEngine;

namespace Geolith.World.Block
{
    public class BlockDataManager : MonoBehaviour
    {
        public static float TextureOffset = 0.001f;
        public static Vector2 TileSize;
        public static Dictionary<BlockType, TextureData> BlockTextureDataDictionary = new();
        public BlockDataSo textureData;

        private void Awake()
        {
            foreach (var item in textureData.textureDataList)
                BlockTextureDataDictionary.TryAdd(item.BlockType, item);
            TileSize = textureData.textureSize;
        }
    }

}