using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




public class UnitsWindow : EditorWindow
{
    private string _name;

    private float _hp;
    private float _atk;
    private float _def;
    private float _spd;
    private float _int;
    private float _acc;

    private bool _unitGroupEnabled;
    private bool _buildGroupEnabled;
    private bool _isEverythingSet;

    type _type;
   
    team _team;

    private GameObject _model;
    private Texture2D _preview;

    public enum type
    {
        Building,
        Unit,
        Prop
    }

    public enum team
    {
        Blue,
        Red,
        Green,
        Neutral
    }

    [MenuItem("Editor/Props")]

    static void CreateWindow()
    {
        ((UnitsWindow)GetWindow(typeof(UnitsWindow))).Show();
    }

    void OnGUI()

    {
        _name = EditorGUILayout.TextField("Name", _name);
        _type = (type)EditorGUILayout.EnumPopup("Type", _type);
        Debug.Log(_type);

        if (_type == type.Building || _type == type.Unit)
        {
            EditorGUILayout.BeginFadeGroup(1);
            _team = (team)EditorGUILayout.EnumPopup("Team", _team);
            _hp = EditorGUILayout.FloatField("HP", _hp);
            EditorGUILayout.EndFadeGroup();
        }
        if ( _type == type.Unit)
        {
            EditorGUILayout.BeginFadeGroup(1);
            _atk = EditorGUILayout.FloatField("ATK", _atk);
            _def = EditorGUILayout.FloatField("DEF", _def);
            _spd = EditorGUILayout.FloatField("SPD", _spd);
            EditorGUILayout.EndFadeGroup();
        }
        
        GUILayout.Label("Model", EditorStyles.boldLabel);
        _model = (GameObject)EditorGUILayout.ObjectField("Model: ", _model, typeof(GameObject), true);
        _preview = AssetPreview.GetAssetPreview(_model);
        if (_preview != null)
        {
            Repaint();
            GUILayout.BeginHorizontal();
            GUI.DrawTexture(GUILayoutUtility.GetRect(50, 50, 50, 50), _preview, ScaleMode.ScaleToFit);
            GUILayout.Label(_model.name);
            GUILayout.Label(AssetDatabase.GetAssetPath(_model));
            GUILayout.EndHorizontal();
        }
        else
            EditorGUILayout.LabelField("No model");

  

        if (_name == null) _name = "Default";
        if (_hp < 0) _hp = 0;
        if (_atk < 0) _atk = 0;
        if (_def < 0) _def = 0;
        if (_spd < 0) _spd = 0;
        if (_int < 0) _int = 0;
        if (_acc < 0) _acc = 0;

        if (_model == null)
        {
            EditorGUILayout.BeginFadeGroup(1);
            if (GUILayout.Button("Create " + _type))
            {
                ScriptableObjectUtility.CreateAsset<Mode>(_name , _acc, _atk, _def, _hp, _int, _spd, _model);
            }
            EditorGUILayout.EndFadeGroup();
        }
       

    }

}
 