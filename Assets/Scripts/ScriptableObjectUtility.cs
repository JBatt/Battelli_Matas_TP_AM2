using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
    public static void CreateAsset<T>(string Name, float _hp, float _atk, float _def, float _spd, float _int, float _acc, GameObject _model) where T : Mode
    {
        T asset = ScriptableObject.CreateInstance<T>();

        asset._acc = _acc;
        asset._atk = _atk;
        asset._def = _def;
        asset._hp = _hp;
        asset._int = _int;
        asset._spd = _spd;
        

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObject/" + Name + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}