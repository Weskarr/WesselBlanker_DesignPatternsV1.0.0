using UnityEngine;

[RequireComponent (typeof(MeshRenderer), typeof(MeshFilter))]
public class BlockTextureTester : MonoBehaviour
{
    public int blockID = 0;

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        // Fill UV2 with blockID for each vertex
        Vector2[] uv2 = new Vector2[mesh.vertexCount];
        for (int i = 0; i < uv2.Length; i++)
        {
            uv2[i] = new Vector2(blockID, 0);
        }
        mesh.uv2 = uv2;
    }

}
