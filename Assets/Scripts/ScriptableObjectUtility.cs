using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
    public static void CreateUnit<T>(string Name, float _hp, float _atk, float _def, float _spd, float _int, float _acc) where T : Unit
    {
        T unit = ScriptableObject.CreateInstance<T>();

        unit._acc = _acc;
        unit._atk = _atk;
        unit._def = _def;
        unit._hp = _hp;
        unit._int = _int;
        unit._spd = _spd;
        

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObject/Unit/" + Name + ".asset");

        AssetDatabase.CreateAsset(unit, assetPathAndName);

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = unit;
    }
    public static void CreateBuilding<T>(string Name,float _hp, float _def) where T : Building
    {
        T building = ScriptableObject.CreateInstance<T>();

      
        building._def = _def;
        building._hp = _hp;
        

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObject/Building/" + Name + ".asset");

        AssetDatabase.CreateAsset(building, assetPathAndName);

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = building;
    }
}