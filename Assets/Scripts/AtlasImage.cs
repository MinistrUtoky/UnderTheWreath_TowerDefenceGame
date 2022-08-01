using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class AtlasImage: MonoBehaviour
{
    [SerializeField] private SpriteAtlas atlas;
    [SerializeField] private string originalImage;

    private Image _currentImage;

    private void Awake()
    {
        _currentImage = GetComponent<Image>();
    }

    private void Start()
    {
        _currentImage.sprite = atlas.GetSprite(originalImage);
    }
}
