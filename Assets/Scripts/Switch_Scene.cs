using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch_Scene : MonoBehaviour
{
    void Start()
    {
    }
    
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID, LoadSceneMode.Single);
    }
}
