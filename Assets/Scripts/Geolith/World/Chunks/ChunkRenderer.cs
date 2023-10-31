﻿using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Geolith.World.Chunks
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class ChunkRenderer : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;
        private Mesh _mesh;
        public bool showGizmos;

        public ChunkData ChunkData { get; private set; }

        // Chunk has been modified since it was last saved.
        public bool Dirty
        {
            get => ChunkData.Dirty;
            set => ChunkData.Dirty = value;
        }

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
            _mesh = _meshFilter.mesh;
            // _meshCollider.sharedMesh = _mesh;
        }

        public void InitializeChunk(ChunkData data)
        {
            ChunkData = data;
        }

        private void RenderMesh(MeshData meshData)
        {
            _mesh.Clear();

            _mesh.subMeshCount = 2;
            _mesh.vertices = meshData.Vertices.Concat(meshData.WaterMeshData.Vertices).ToArray();

            _mesh.SetTriangles(meshData.Triangles.ToArray(), 0);
            _mesh.SetTriangles(meshData.WaterMeshData.Triangles.Select(val => val + meshData.Vertices.Count).ToArray(), 1);

            _mesh.uv = meshData.Uvs.Concat(meshData.WaterMeshData.Uvs).ToArray();
            _mesh.RecalculateNormals();

            _meshCollider.sharedMesh = null;

            var collisionMesh = new Mesh()
            {
                vertices = meshData.ColliderVertices.ToArray(),
                triangles = meshData.ColliderTriangles.ToArray()
            };
            collisionMesh.RecalculateNormals();

            _meshCollider.sharedMesh = collisionMesh;
        }

        public void UpdateChunk()
        {
            RenderMesh(Chunk.GetChunkMeshData(ChunkData));
        }

        public void UpdateChunk(MeshData data)
        {
            RenderMesh(data);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!showGizmos || !Application.isPlaying || ChunkData == null) return;
            Gizmos.color = Selection.activeObject == gameObject ? new Color(0, 1, 0, 0.4f) : new Color(1, 0, 1, 0.4f);

            Gizmos.DrawCube(
                transform.position + new Vector3(ChunkData.ChunkSize / 2f, ChunkData.ChunkHeight / 2f, ChunkData.ChunkSize / 2f),
                new Vector3(ChunkData.ChunkSize, ChunkData.ChunkHeight, ChunkData.ChunkSize)
            );
        }
#endif
    }
}