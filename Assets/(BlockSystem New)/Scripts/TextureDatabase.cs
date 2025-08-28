using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "TextureDatabase", menuName = "Scriptable Objects/TextureDatabase")]
public class TextureDatabase : ScriptableObject
{
    public Material shaderToUse;
    public TextureData[] textureDataArray;

    private void OnValidate()
    {
        ApplyTextures();
    }

    // Call this manually from a manager or editor script
    public void ApplyTextures()
    {
        if (textureDataArray == null || textureDataArray.Length == 0 || shaderToUse == null)
        {
            Debug.LogWarning("TextureDatabase not fully set up!");
            return;
        }

        List<Texture2D> textureList = new();

        foreach (TextureData textureData in textureDataArray)
        {
            Texture2D texture = textureData.AllFacesTexture;

            if (texture != null)
                textureList.Add(textureData.AllFacesTexture);
        }

        var texArray = TextureArrayBuilder.Build(textureList.ToArray());
        shaderToUse.SetTexture("_BlockTextures", texArray);
    }
}
