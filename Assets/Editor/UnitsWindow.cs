﻿using System.Collections;
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

    [MenuItem("Create/Scriptable Object/Unit")]

    static void CreateWindow()
    {
        ((UnitsWindow)GetWindow(typeof(UnitsWindow))).Show();
    }

    void OnGUI()

    {
        _name = EditorGUILayout.TextField("Name", _name);
        _hp = EditorGUILayout.FloatField("HP", _hp);
        _atk = EditorGUILayout.FloatField("ATK", _atk);
        _def = EditorGUILayout.FloatField("DEF", _def);
        _spd = EditorGUILayout.FloatField("SPD", _spd);
 

        if (_name == null) _name = "Default";
        if (_hp < 0) _hp = 0;
        if (_atk < 0) _atk = 0;
        if (_def < 0) _def = 0;
        if (_spd < 0) _spd = 0;
        if (_int < 0) _int = 0;
        if (_acc < 0) _acc = 0;



        if (GUILayout.Button("Create unit"))
        {
            ScriptableObjectUtility.CreateUnit<Unit>(_name, _acc, _atk, _def, _hp, _int, _spd);
        }




    }

}
