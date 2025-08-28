using UnityEngine;

[CreateAssetMenu(fileName = "TextureData", menuName = "Scriptable Objects/TextureData")]
public class TextureData : ScriptableObject
{
    [SerializeField] private Texture2D _allFacesTexture;

    public Texture2D AllFacesTexture
    {
        get
        {
            return _allFacesTexture;
        }
        set
        {
            _allFacesTexture = value;
        }
    }
}
