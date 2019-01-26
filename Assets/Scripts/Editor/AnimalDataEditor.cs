using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimalData))]
public class AnimalDataEditor : Editor
{
    [SerializeField]
    private SpriteRenderer _targetRenderer;
    [SerializeField]
    private string _pattern;
    [SerializeField]
    private string _sprite;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var obj = (AnimalData)target;

        GUILayout.Space(10f);
        GUILayout.Label("Debug");
        _targetRenderer = (SpriteRenderer)EditorGUILayout.ObjectField(_targetRenderer, typeof(SpriteRenderer), true);
        _pattern = EditorGUILayout.TextField("Pattern", _pattern);
        _sprite = EditorGUILayout.TextField("Sprite", _sprite);

        if (_targetRenderer == null)
        {
            return;
        }

        if (GUILayout.Button("SetSprite"))
        {
            _targetRenderer.material = obj.GetPattern(_pattern);
            _targetRenderer.sprite = obj.GetSprite(_sprite); 
        }
    }

    [MenuItem("Assets/Create/Animal/AnimalData")]
    public static AnimalData Create()
    {
        var asset = ScriptableObject.CreateInstance<AnimalData>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/AnimalData.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
