using Geolith.World.Block;
using UnityEngine;

namespace Geolith.World.Chunks
{
    public class ChunkData
    {
        private BlockType[] _blocks; // [x + (y * _chunkSize) + (z * _chunkSize * _chunkHeight)]
        private int _chunkSize = 32; // TODO: make this a constant & check the value
        private int _chunkHeight = 384;
        private World _world;
        private Vector3Int _position;

        /**
         * Whether or not the chunk has been modified since it was last saved.
         */
        public bool Dirty { get; set; }

        public ChunkData(int chunkSize, int chunkHeight, World world, Vector3Int position)
        {
            _chunkSize = chunkSize;
            _chunkHeight = chunkHeight;
            _world = world;
            _position = position;
            _blocks = new BlockType[_chunkSize * _chunkSize * _chunkHeight]; // TODO: change this to a Block class
        }


        public BlockType[] Blocks => _blocks;
        public int ChunkSize => _chunkSize;
        public int ChunkHeight => _chunkHeight;
        public World World => _world;
        public Vector3Int Position => _position;
    }
}