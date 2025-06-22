using UnityEngine;

[CreateAssetMenu(fileName = "MaterialTemplates", menuName = "Scriptable Objects/MaterialTemplates")]
public class MaterialTemplates : ScriptableObject
{
    [SerializeField] private Material _lit;
    [SerializeField] private Material _unlit;
    [SerializeField] private Material _transparent;

    public Material Lit { get => _lit; }
    public Material Unlit { get => _unlit; }
    public Material Transparent { get => _transparent; }
}
