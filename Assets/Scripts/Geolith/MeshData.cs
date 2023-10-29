using System.Collections.Generic;
using UnityEngine;

namespace Geolith
{
    public class MeshData
    {
        private List<Vector3> _vertices = new();
        private List<int> _triangles = new();
        private List<Vector2> _uvs = new();

        private List<Vector3> _colliderVertices = new();
        private List<int> _colliderTriangles = new();

        private MeshData _waterMeshData;
        private bool _mainMesh = true;

        public MeshData(bool mainMesh = true)
        {
            if (mainMesh)
            {
                _waterMeshData = new MeshData(false);
            }
        }

        public List<Vector3> Vertices => _vertices;
        public List<int> Triangles => _triangles;
        public List<Vector2> Uvs => _uvs;
        public List<Vector3> ColliderVertices => _colliderVertices;
        public List<int> ColliderTriangles => _colliderTriangles;

        public MeshData WaterMeshData
        {
            get => _waterMeshData;
            set => _waterMeshData = value;
        }

        public bool MainMesh => _mainMesh;

        // Maybe create a MeshHelper class to handle this?
        public void AddVertex(Vector3 vertex, bool vertexGeneratesCollider)
        {
            _vertices.Add(vertex);
            if (vertexGeneratesCollider)
            {
                _colliderVertices.Add(vertex);
            }
        }

        public void AddQuadTriangles(bool quadGeneratesCollider)
        {
            _triangles.Add(_vertices.Count - 4); // bottom left, 0
            _triangles.Add(_vertices.Count - 3); // top left, 1
            _triangles.Add(_vertices.Count - 2); // top right, 2

            _triangles.Add(_vertices.Count - 4); // bottom left, 0
            _triangles.Add(_vertices.Count - 2); // top right, 2
            _triangles.Add(_vertices.Count - 1); // bottom right, 3

            if (!quadGeneratesCollider) return;

            _colliderTriangles.Add(_colliderVertices.Count - 4); // bottom left, 0
            _colliderTriangles.Add(_colliderVertices.Count - 3); // top left, 1
            _colliderTriangles.Add(_colliderVertices.Count - 2); // top right, 2

            _colliderTriangles.Add(_colliderVertices.Count - 4); // bottom left, 0
            _colliderTriangles.Add(_colliderVertices.Count - 2); // top right, 2
            _colliderTriangles.Add(_colliderVertices.Count - 1); // bottom right, 3
        }
    }
}