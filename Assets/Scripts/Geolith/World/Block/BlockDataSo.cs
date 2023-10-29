using System;
using System.Collections.Generic;
using UnityEngine;

namespace Geolith.World.Block
{
    // So = ScriptableObject
    [CreateAssetMenu(fileName = "Block Data", menuName = "Data/Block Data")]
    public class BlockDataSo : ScriptableObject
    {
        public Vector2 textureSize;
        public List<TextureData> textureDataList;
    }

    [Serializable]
    public class TextureData
    {
        public BlockType BlockType;
        public Vector2Int up, down, side;
        public bool isSolid = true;
        public bool generateCollider = true;
    }
}
// TODO: rework this entire file