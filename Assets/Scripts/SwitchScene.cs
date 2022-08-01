using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private int switchSceneId;
    
    public void ChangeScene()
    {
        SceneManager.LoadScene(switchSceneId);
    }
}
