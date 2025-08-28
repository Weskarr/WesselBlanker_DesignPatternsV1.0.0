using UnityEngine;

public static class TextureArrayBuilder
{
    public static Texture2DArray Build(Texture2D[] textures)
    {
        int size = textures[0].width;
        var texArray = new Texture2DArray(size, size, textures.Length, TextureFormat.RGBA32, true);
        texArray.filterMode = FilterMode.Point; // keep pixels sharp
        texArray.wrapMode = TextureWrapMode.Clamp;

        for (int i = 0; i < textures.Length; i++)
        {
            Graphics.CopyTexture(textures[i], 0, 0, texArray, i, 0);
        }

        texArray.Apply();
        return texArray;
    }
}