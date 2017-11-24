using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




public class BuildingWindow : EditorWindow
{
    private string _name;

    private float _hp;
    private float _def;

    private bool _unitGroupEnabled;
    private bool _buildGroupEnabled;
    private bool _isEverythingSet;


    [MenuItem("Create/Scriptable Object/Building")]

    static void CreateWindow()
    {
        ((BuildingWindow)GetWindow(typeof(BuildingWindow))).Show();
    }

    void OnGUI()

    {
        _name = EditorGUILayout.TextField("Name", _name);
        _hp = EditorGUILayout.FloatField("HP", _hp);
        _def = EditorGUILayout.FloatField("DEF", _def);
        


        if (_name == null) _name = "Default";
        if (_hp < 0) _hp = 0;
        if (_def < 0) _def = 0;
     


        if (GUILayout.Button("Create Building"))
        {
            ScriptableObjectUtility.CreateBuilding<Building>(_name, _def, _hp);
        }




    }

}
