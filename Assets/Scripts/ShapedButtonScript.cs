using UnityEngine;
using UnityEngine.UI;

public class ShapedButtonScript : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}
