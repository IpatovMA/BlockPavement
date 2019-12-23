using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMeshBuilder

    {

        private List<Vector3> _vertices = new List<Vector3>();

        private List<Vector2> _uvs = new List<Vector2>();

        private List<int> _indices = new List<int>();


        public void AddConnectedEdge(Vector3 p1, Vector3 p2)

        {

            var count = _vertices.Count;

            AddRectSurface(_vertices[count - 1], _vertices[count - 2], p1, p2);

        }


        public void AddRectSurface(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)

        {

            int count = _vertices.Count;

            _vertices.Add(p1);

            _vertices.Add(p2);

            _vertices.Add(p3);

            _vertices.Add(p4);


            _indices.Add(count);

            _indices.Add(count + 1);

            _indices.Add(count + 2);


            _indices.Add(count + 3);

            _indices.Add(count);

            _indices.Add(count + 2);


            _uvs.Add(new Vector2(0, 0));

            _uvs.Add(new Vector2(0, 1));

            _uvs.Add(new Vector2(1, 1));

            _uvs.Add(new Vector2(1, 0));

        }


        public Mesh ToMesh()

        {

            var mesh = new Mesh();


            mesh.vertices = _vertices.ToArray();

            mesh.uv = _uvs.ToArray();

            mesh.triangles = _indices.ToArray();

 

            mesh.RecalculateNormals();

            mesh.RecalculateBounds();

            return mesh;

        }


}

