using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionsManager : MonoBehaviour
{
    public GameObject optionsUI; // Tham chiếu đến UI Options

    private static UIOptionsManager instance; // Biến static để lưu instance của UIOptionsManager

    private void Awake()
    {
        // Kiểm tra nếu đã có một instance tồn tại
        if (instance == null)
        {
            instance = this; // Gán instance hiện tại
            DontDestroyOnLoad(gameObject); // Giữ lại đối tượng này khi chuyển scene
        }
        else
        {
            Destroy(gameObject); // Xóa đối tượng mới nếu đã có một instance tồn tại
        }
    }

    public void ToggleOptions()
    {
        optionsUI.SetActive(!optionsUI.activeSelf);
    }
}
