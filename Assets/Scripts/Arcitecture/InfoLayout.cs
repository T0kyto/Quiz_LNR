using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoLayout : MonoBehaviour
{
    GridLayoutGroup _gridLayoutGroup => GetComponent<GridLayoutGroup>();
    [SerializeField] private TMP_Text _infoLayoutText;
    [SerializeField] private Image _infoLayoutImage;
    [SerializeField] private string _infoLayoutImagesFolder;
     
    public void SetInfoLayout(string infoText, string spritePath)
    {
        _infoLayoutText.text = infoText;
        
        if (spritePath != "")
        {
            Debug.Log(Path.Combine(_infoLayoutImagesFolder, spritePath));
            _infoLayoutImage.gameObject.SetActive(true);
            _infoLayoutImage.sprite = DataLoader.LoadImage(Path.Combine(_infoLayoutImagesFolder, spritePath));
        }
        else
        {
            _infoLayoutImage.gameObject.SetActive(false);
        }
    }
}
