using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Characters/CharacterData")]
public class CharacterScriptableObject : ScriptableObject
{
    public string characterName;    // Tên nhân vật
    public int price;               // Giá nhân vật
    public Sprite characterImage;    // Hình ảnh nhân vật
    public GameObject prefab;       // Prefab của nhân vật
    public bool isOwned;            // Trạng thái mua nhân vật
    public bool isSelected;         // Trạng thái chọn nhân vật
}
