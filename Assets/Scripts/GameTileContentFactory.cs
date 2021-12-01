using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]

public class GameTileContentFactory : ScriptableObject
{
    //Вернуть
    public void Reclaim(RunTimeContent content) {
        Destroy(content.gameObject);
    }

    private RunTimeContent Get(RunTimeContent prefab) {
        RunTimeContent instance = Instantiate(prefab);
        instance.OriginFactory = this;
        MoveToFactoryScene(instance.gameObject);
        return instance;
    }

    private Scene contentScene;

    private void MoveToFactoryScene(GameObject obj) {
        if(!contentScene.isLoaded)
            if (Application.isEditor) {
                contentScene = SceneManager.GetSceneByName(name);
                if (!contentScene.isLoaded)
                    contentScene = SceneManager.CreateScene(name);
            } else {
                contentScene = SceneManager.CreateScene(name);
            }
        SceneManager.MoveGameObjectToScene(obj, contentScene);
    }
}
