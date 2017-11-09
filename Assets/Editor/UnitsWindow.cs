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

    private GameObject _goToPreview;
    private Texture2D _preview;

    public enum type
    {
        Building,
        Unit,
        Prop
    }

    public enum team
    {
        None,
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

        _buildGroupEnabled = EditorGUILayout.BeginToggleGroup("(Si seleccionas build o unit)", _buildGroupEnabled);
        _hp = EditorGUILayout.FloatField("HP", _hp);
        EditorGUILayout.EndToggleGroup();

        _unitGroupEnabled = EditorGUILayout.BeginToggleGroup("(Si seleccionas Unit)", _unitGroupEnabled);
        _atk = EditorGUILayout.FloatField("ATK", _atk);
        _def = EditorGUILayout.FloatField("DEF", _def);
        _spd = EditorGUILayout.FloatField("SPD", _spd);
        EditorGUILayout.EndToggleGroup();

        

        _team = (team)EditorGUILayout.EnumPopup("Team", _team);

       

        GUILayout.Label("Model", EditorStyles.boldLabel);
        _goToPreview = (GameObject)EditorGUILayout.ObjectField("Objeto: ", _goToPreview, typeof(GameObject), true);
        _preview = AssetPreview.GetAssetPreview(_goToPreview);
        if (_preview != null)
        {
            Repaint();
            GUILayout.BeginHorizontal();
            GUI.DrawTexture(GUILayoutUtility.GetRect(50, 50, 50, 50), _preview, ScaleMode.ScaleToFit);
            GUILayout.Label(_goToPreview.name);
            GUILayout.Label(AssetDatabase.GetAssetPath(_goToPreview));
            GUILayout.EndHorizontal();
        }
        else
            EditorGUILayout.LabelField("No model");

        _isEverythingSet = EditorGUILayout.BeginToggleGroup("(Una vez que este todo seteado apretar boton magico invisible que crea el SO)", _isEverythingSet);


        EditorGUILayout.EndToggleGroup();

    }

}
 