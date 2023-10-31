using System.Collections;
using System.Collections.Generic;
using System.IO;
using AwakeSolutions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoLayout : MonoBehaviour
{
    GridLayoutGroup _gridLayoutGroup => GetComponent<GridLayoutGroup>();
    [SerializeField] private string _infoLayoutImagesFolder;
    [SerializeField] private AwakeMediaPlayer _infoLayoutImage;
     
    public void SetInfoLayout(string folder, string spritePath)
    {
        _infoLayoutImage.Open(folder, spritePath);
    }
}
