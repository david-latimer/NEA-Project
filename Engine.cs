using System.Numerics;

namespace Engine
{
    public class Camera
    {
        // for default the camera is at the origin and does not move, might change later
        public Vector3 Position = new Vector3(0, 0, 0);
        public Vector3 Target;
    }

    public class Mesh
    {
        public string Name;
        public Vector3[] Vertices;
        public Vector3[,] Tris;
        public Vector3 Position;
        public Vector3 Rotation;

        public Mesh(string name, int vertexCount)
        {
            Name = name;
            Vertices = new Vector3[vertexCount];
            Tris = new Vector3[(int)(vertexCount * 1.5f), 3];
        }
    }

    // for a better future in which i am less tired and less suicidal also

/*    public class Cube : Mesh
    {
        public Cube(string name = "defaultCube")
        {
            Name = name;
            Vertices = new Vector3[8] { new Vector3(0, 0, 0),
                                        new Vector3(0, 1, 0),
                                        new Vector3(1, 1, 0),
                                        new Vector3(1, 0, 0),
                                        new Vector3(0, 0, 1),
                                        new Vector3(0, 1, 1),
                                        new Vector3(1, 1, 1),
                                        new Vector3(1, 0, 1), };

            Tris = new Vector3[12, 3] { { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) },
                                        { new Vector3(0, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0) },
                                        { new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 1) },
                                        { new Vector3(1, 0, 0), new Vector3(1, 1, 1), new Vector3(1, 0, 1) },
                                        { new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(0, 1, 1) },
                                        { new Vector3(1, 0, 1), new Vector3(0, 1, 1), new Vector3(0, 0, 1) },
                                        { new Vector3(0, 0, 1), new Vector3(0, 1, 1), new Vector3(0, 1, 0) },
                                        { new Vector3(0, 0, 1), new Vector3(0, 1, 0), new Vector3(0, 0, 0) },
                                        { new Vector3(0, 1, 0), new Vector3(0, 1, 1), new Vector3(1, 1, 1) },
                                        { new Vector3(0, 1, 0), new Vector3(1, 1, 1), new Vector3(1, 1, 0) },
                                        { new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 0, 0) },
                                        { new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1) } };
        }
    }*/
}