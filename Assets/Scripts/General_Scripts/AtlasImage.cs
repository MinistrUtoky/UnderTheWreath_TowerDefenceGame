using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class AtlasImage: MonoBehaviour
{
    [SerializeField] SpriteAtlas atlas;
    [SerializeField] string original_image;
    void Start()
    {
        GetComponent<Image>().sprite = atlas.GetSprite(original_image);
    }
}
