using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelWindow : EditorWindow
{
    string _aux = null;

    string myName;

    int width;
    int hight;
    int length;

    Material currentMaterial;

    bool _showMaterials;
    bool _showObstacle;
    bool _showObstacle2;
    bool _showSelectedObstacle;

    Texture2D previewMaterialList;
    Texture2D previewCurrentMaterial;
    Texture2D previewObstacle;
    Texture2D previewObstacleList;


    List<GameObject> obstacleList = new List<GameObject>();

    List<Object> _objectList = new List<Object>();
    List<Object> _objectList2 = new List<Object>();


    Vector2 slider;

    GameObject level;


    [MenuItem("Create/Game Object/Level")]
    static void CreateWindow()
    {
        ((LevelWindow)GetWindow(typeof(LevelWindow))).Show();
    }
    void OnGUI()
    {
        slider = EditorGUILayout.BeginScrollView(slider);
        myName = EditorGUILayout.TextField("Level name : ", myName);

        width = EditorGUILayout.IntField("Heigh Level :", width);
        hight = EditorGUILayout.IntField("Width Level :", hight);
        length = EditorGUILayout.IntField("Deep Level :", length);

        if (width <= 0) width = 1;
        if (hight <= 0) hight = 1;
        if (length <= 0) length = 1;

        #region material
        if (currentMaterial == null)
        {
            MaterialSearcher();
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            currentMaterial = (Material)EditorGUILayout.ObjectField("Material ", currentMaterial, typeof(Material), false);
            previewCurrentMaterial = AssetPreview.GetAssetPreview(currentMaterial);
            if (GUILayout.Button("Clear material"))
            {
                currentMaterial = null;
            }
            GUILayout.EndVertical();
            if (previewCurrentMaterial != null)
            {
                GUI.DrawTexture(GUILayoutUtility.GetRect(50, 50, 50, 50), previewCurrentMaterial, ScaleMode.ScaleToFit);
            }
            GUILayout.EndHorizontal();
        }
        if (currentMaterial == null)
        {
            EditorGUILayout.HelpBox("There is no material selected", MessageType.Warning);
        }
        #endregion

        _showObstacle = EditorGUILayout.Foldout(_showObstacle, "Pick obstacle");
        if (_showObstacle)
        {
            _showObstacle2 = EditorGUILayout.Foldout(_showObstacle, "Pick obstacle");
            if(_showObstacle2)
            Obstacle();

            _showSelectedObstacle = EditorGUILayout.Foldout(_showSelectedObstacle, "Selected obstacles");
            if (_showSelectedObstacle)
            {

                for (int i = 0; i < obstacleList.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical();
                    obstacleList[i] = (GameObject)EditorGUILayout.ObjectField("Obstacle " + i, obstacleList[i], typeof(GameObject), false);
                    previewObstacle = AssetPreview.GetAssetPreview(obstacleList[i]);
                    if (GUILayout.Button("Remove"))
                    {
                        obstacleList.Remove(obstacleList[i]);

                    }
                    GUILayout.EndVertical();
                    if (previewObstacle != null)
                    {
                        GUI.DrawTexture(GUILayoutUtility.GetRect(50, 50, 50, 50), previewObstacle, ScaleMode.ScaleToFit);
                    }
                        GUILayout.EndHorizontal();

                }
            }
        }


        #region create
        if (GUILayout.Button("Create and save"))
        {
            level = GameObject.CreatePrimitive(PrimitiveType.Cube);

            level.transform.localScale = new Vector3(width, hight, length);
            level.name = myName;
            level.GetComponent<Renderer>().material = currentMaterial;
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefab/Levels/" + myName + ".prefab");

            PrefabUtility.CreatePrefab(assetPathAndName, level);

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();

            for (int i = 0; i < obstacleList.Count; i++)
            {
                var obstacle = Instantiate(obstacleList[i]);
                obstacle.transform.parent = level.transform;
                obstacle.transform.position = new Vector3(0, 2, 0);
            }
        }
        #endregion
        EditorGUILayout.EndScrollView();
    }

    private void MaterialSearcher()
    {
        int i;

        _objectList.Clear();
        string[] allPaths = AssetDatabase.FindAssets(_aux);
        for (i = allPaths.Length - 1; i >= 0; i--)
        {
            allPaths[i] = AssetDatabase.GUIDToAssetPath(allPaths[i]);
            _objectList.Add(AssetDatabase.LoadAssetAtPath(allPaths[i], typeof(Object)));
        }

        _showMaterials = EditorGUILayout.Foldout(_showMaterials, "Pick level material");

        if (_showMaterials)
        {
            for (i = _objectList.Count - 1; i >= 0; i--)
            {
                if (_objectList[i].GetType() == typeof(Material) && _objectList[i].name.Contains("level"))
                {


                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(_objectList[i].name);
                    previewMaterialList = AssetPreview.GetAssetPreview(_objectList[i]);
                    if (previewMaterialList != null)
                    {
                        GUI.DrawTexture(GUILayoutUtility.GetRect(50, 50, 50, 50), previewMaterialList, ScaleMode.ScaleToFit);
                    }
                    if (GUILayout.Button("Select"))
                    {
                        currentMaterial = (Material)_objectList[i];
                    }
                    EditorGUILayout.EndHorizontal();
                }

            }

        }
    }
    private void Obstacle()
    {

        int i;
        _objectList2.Clear();
        string[] allPaths = AssetDatabase.FindAssets(_aux);

        for (i = allPaths.Length - 1; i >= 0; i--)
        {
            allPaths[i] = AssetDatabase.GUIDToAssetPath(allPaths[i]);

            _objectList2.Add(AssetDatabase.LoadAssetAtPath(allPaths[i], typeof(Object)));

        }


        for (i = _objectList2.Count - 1; i >= 0; i--)
        {

            if (_objectList2[i].GetType() == typeof(GameObject) && _objectList2[i].name.Contains("Obstacle"))
            {
                EditorGUILayout.BeginHorizontal();
                previewObstacleList = AssetPreview.GetAssetPreview(_objectList2[i]);
                if (previewObstacleList != null)
                {
                    GUI.DrawTexture(GUILayoutUtility.GetRect(50, 50, 50, 50), previewObstacleList, ScaleMode.ScaleToFit);
                }
                if (GUILayout.Button("Select"))
                {
                    obstacleList.Add((GameObject)_objectList2[i]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }


    }
}
