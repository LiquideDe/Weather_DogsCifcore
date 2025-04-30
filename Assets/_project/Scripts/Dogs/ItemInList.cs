using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemInList : MonoBehaviour
{
    public event Action<string> ShowThisDog;

    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _textName;

    private string _id;

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowThisDogPressed);
    }

    public void Initialize(string text, string id)
    {
        gameObject.SetActive(true);
        _textName.text = text;
        _id = id;
    }

    private void ShowThisDogPressed() => ShowThisDog?.Invoke(_id);
}
