using UnityEngine;

public class TextureAssistant : MonoBehaviour
{
    [SerializeField] private TextureDatabase _textureDatabase;

    public void RevalidateTextures()
    {
        if ( _textureDatabase == null)
        {
            Debug.LogWarning("TextureDatabase not assigned!");
            return;
        }

        _textureDatabase.ApplyTextures();
    }
}
