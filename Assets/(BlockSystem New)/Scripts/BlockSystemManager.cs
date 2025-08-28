using UnityEngine;

public class BlockSystemManager : MonoBehaviour
{
    [SerializeField] private TextureAssistant _textureAssistant;

    private void Awake()
    {
        SetupBlockSystem();
    }

    private void SetupBlockSystem()
    {
        _textureAssistant.RevalidateTextures();
    }
}
