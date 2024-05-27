using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopUI; // UI của shop
    public Button[] skinButtons; // Các nút mua skin
    public TextMeshProUGUI goldText; // Hiển thị số lượng vàng hiện tại
    private int currentGold; // Số vàng hiện tại của người chơi
    public TextMeshProUGUI[] costText; // Hiển thị giá của các skin
    public int[] skinCosts; // Chi phí cho mỗi skin
    private bool[] skinOwned; // Mảng để xác định xem người chơi đã sở hữu skin nào

    void Start()
    {
        currentGold = GoldManage.instance.GetCurrentGold();
        UpdateGoldUI(); // Cập nhật số vàng khi khởi tạo scene
        UpdateButtonInteractivity(); // Cập nhật trạng thái các nút mua
        UpdateCostTexts(); // Cập nhật giá của các skin
        LoadOwnedSkins(); // Tải thông tin về các skin đã sở hữu
    }

    void UpdateGoldUI()
    {
        goldText.text = "Gold: " + currentGold.ToString();
    }

    private void OnEnable()
    {
        // Đảm bảo cập nhật số vàng khi scene được hiển thị
        UpdateGoldUI();
        UpdateButtonInteractivity(); // Cập nhật trạng thái các nút mua khi scene được hiển thị
    }

    // Cập nhật trạng thái các nút mua dựa trên số vàng hiện tại và các skin đã sở hữu
    void UpdateButtonInteractivity()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (currentGold >= skinCosts[i] && !skinOwned[i])
            {
                skinButtons[i].interactable = true;
            }
            else
            {
                skinButtons[i].interactable = false;
                if (skinOwned[i])
                {
                    skinButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Owner"; // Hiển thị văn bản "Owner"
                }
            }
        }
    }

    // Cập nhật giá của các skin trên các nút
    void UpdateCostTexts()
    {
        for (int i = 0; i < costText.Length; i++)
        {
            costText[i].text = skinCosts[i].ToString() + " Gold";
        }
    }

    // Tải thông tin về các skin đã sở hữu từ lưu trữ
    void LoadOwnedSkins()
    {
        // Đây là nơi bạn có thể tải thông tin từ lưu trữ (PlayerPrefs, database, etc.) về các skin đã sở hữu của người chơi
        // Ở đây, tôi giả định rằng không có skin nào được sở hữu khi mới bắt đầu game
        skinOwned = new bool[skinButtons.Length];
        for (int i = 0; i < skinOwned.Length; i++)
        {
            skinOwned[i] = false;
        }
    }

    // Hàm để mua skin
    public void BuySkin(int index)
    {
        if (currentGold >= skinCosts[index] && !skinOwned[index])
        {
            currentGold -= skinCosts[index];
            GoldManage.instance.SetCurrentGold(currentGold); // Cập nhật số vàng trong hệ thống quản lý vàng
            UpdateGoldUI();
            UpdateButtonInteractivity(); // Cập nhật lại trạng thái các nút mua sau khi mua
            skinOwned[index] = true; // Đánh dấu skin đã sở hữu

            // Logic để thêm skin vào kho đồ của người chơi (tùy thuộc vào hệ thống của bạn)
            Debug.Log("Skin " + index + " purchased.");
        }
        else if (skinOwned[index])
        {
            Debug.Log("You already own skin " + index);
        }
        else
        {
            Debug.Log("Not enough gold to purchase skin " + index);
        }
    }

    // Hàm để quay lại menu
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
