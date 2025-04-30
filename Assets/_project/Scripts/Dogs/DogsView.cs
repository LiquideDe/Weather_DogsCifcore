using System.Collections.Generic;
using UnityEngine;
using System;
using WeatherDogs;

public class DogsView : MonoBehaviour
{
    public event Action<string> ShowThisBreed;

    [SerializeField] private ItemInList _itemPrefab;
    [SerializeField] private Transform _content;
    private List<ItemInList> _items = new List<ItemInList>();

    public void Initialize(DogBreedsResponse dogBreeds)
    {
        int i = 1;
        foreach (var dogBreed in dogBreeds.data)
        {
            _items.Add(Instantiate(_itemPrefab, _content));
            _items[^1].Initialize($"{i} - {dogBreed.attributes.name}",dogBreed.id);
            _items[^1].ShowThisDog += ShowThisBreedPressed;
            i++;
        }
    }

    public void ClearList()
    {
        foreach (var item in _items)
        {
            item.ShowThisDog -= ShowThisBreedPressed;
            Destroy(item.gameObject);            
        }
        _items.Clear();
    }

    public void Hide() => gameObject.SetActive(false);

    public void Show() => gameObject.SetActive(true);

    private void ShowThisBreedPressed(string id) => ShowThisBreed?.Invoke(id);
}
