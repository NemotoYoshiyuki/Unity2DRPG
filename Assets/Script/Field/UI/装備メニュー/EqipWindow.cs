using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class EqipWindow : BaseWindow
{

    public CharacterData character;
    public CharacterWindow characterWindow;
    private MenuGuide menuGuide => MenuWindow.instance.menuGuide;
    private Action onCancel;

    public override void Open()
    {
        gameObject.SetActive(true);
        ShowEqipSlot();
        ShowParameter(character.equip.Copy());
    }

    public override void Close()
    {

    }

    public override void Cancel()
    {
        onCancel?.Invoke();
    }

    public void UserSelect()
    {
        var party = Party.GetMember();
        menuGuide.Show("誰の装備を変更しますか？");

        characterWindow.Select(0);
        characterWindow.ClearLisner();
        characterWindow.AddLisner((int index) =>
        {
            character = party[index];
            Open();
        });

        onCancel = (() =>
        {
            MenuWindow.instance.focusWindow = MenuWindow.instance;
            MenuWindow.instance.sideMenu.sideButtons[4].Select();
            menuGuide.Hide();
        });
    }

    [Header("装備スロット")]
    public Button WeopnButton;
    public Button ArmerButton;
    public Button accessoryButton;
    public TextMeshProUGUI weopnText;
    public TextMeshProUGUI armerText;
    public TextMeshProUGUI accessoryText;

    public void ShowEqipSlot()
    {
        //現在装備している装備を表示する
        Equip equip = character.equip;
        Debug.Log(equip.weapon.name);
        weopnText.text = equip.weapon != null ? $"武器　{equip.weapon.name}" : "武器　ーーー";
        //armerText.text = $"防具　{equip.armor.name}";
        armerText.text = equip.armor != null ? $"防具　{equip.armor.name}" : "防具　ーーー";
        accessoryText.text = equip.accessory != null ? $"装飾　{equip.accessory.name}" : "装飾　ーーー";

        WeopnButton.Select();

        onCancel = (() =>
        {
            UserSelect();
        });
    }

    public void OnWeponClick()
    {
        //武器一覧を表示
        List<Weapon> weapons = InventorySystem.GetEquipments().Where(x => x is Weapon).Select(x => x as Weapon).ToList();
        ShowItemList(weapons);
    }

    public void OnArmerClick()
    {
        ShowArmerList();
    }
    public void OnAccessoryClick()
    {
        ShowArmerList();
    }

    [Header("装備リスト")]
    public GameObject list;
    public TextMeshProUGUI eqipDescription;
    public SelectableButton itemButton;
    private List<SelectableButton> itemButtons = new List<SelectableButton>();
    public void ShowItemList(List<Weapon> equips)
    {
        if (equips.Count == 0) return;

        //脱ぐボタンを作成
        SelectableButton detachButton = Instantiate(itemButton);
        detachButton.onClick.AddListener(() => { WeopnTakeOff(); });
        detachButton.transform.SetParent(list.transform);
        itemButtons.Add(detachButton);

        //武器ボタン
        foreach (var item in equips)
        {
            SelectableButton button = CreateItemButton(item);
            button.transform.SetParent(list.transform);
            itemButtons.Add(button);
        }

        itemButtons[0].Select();

        //キャンセル動作
        onCancel = () =>
        {
            //WeopnButton.Select();
            ShowEqipSlot();
        };
    }

    [Header("外すボタン")]
    public SelectableButton detachButton;
    public TextMeshProUGUI detachButtonText;
    public void ShowArmerList()
    {
        detachButton.onClick.AddListener(() => { ArmerTakeOff(); });
        detachButtonText.text = "防具を外す";

        List<Armor> armer = InventorySystem.GetEquipments().Where(x => x is Armor).Select(x => x as Armor).ToList();
        if (armer.Count == 0) return;

        //防具ボタン作成
        foreach (var item in armer)
        {
            SelectableButton button = CreateItemButton(item);
            button.transform.SetParent(list.transform);
            itemButtons.Add(button);
        }

        itemButtons[0].Select();

        //キャンセル動作
        onCancel = () =>
        {
            //WeopnButton.Select();
            ShowEqipSlot();
        };
    }

    public void ShowAccessoryList()
    {
        detachButton.onClick.AddListener(() => { AccessoryTakeOff(); });
        detachButtonText.text = "装飾を外す";

        List<Armor> armer = InventorySystem.GetEquipments().Where(x => x is Armor).Select(x => x as Armor).ToList();
        if (armer.Count == 0) return;

        //防具ボタン作成
        foreach (var item in armer)
        {
            SelectableButton button = CreateItemButton(item);
            button.transform.SetParent(list.transform);
            itemButtons.Add(button);
        }

        itemButtons[0].Select();

        //キャンセル動作
        onCancel = () =>
        {
            //WeopnButton.Select();
            ShowEqipSlot();
        };
    }

    private SelectableButton CreateItemButton(Weapon equip)
    {
        SelectableButton selectableButton = Instantiate(itemButton);
        selectableButton.GetComponentInChildren<TextMeshProUGUI>().text = equip.name;

        selectableButton.onClick.AddListener(() =>
        {
            //Equip(equip);
            weaponEquip(equip);
            //同じ武器なので＋はださない
            ShowParameter(character.equip.Copy());
        });

        selectableButton.onHover = (() =>
        {
            OnHoverEqip(equip);
        });

        //if(CanClick())selectableButton.Invalid();
        return selectableButton;
    }

    private SelectableButton CreateItemButton(Armor equip)
    {
        SelectableButton selectableButton = Instantiate(itemButton);
        selectableButton.GetComponentInChildren<TextMeshProUGUI>().text = equip.name;

        selectableButton.onClick.AddListener(() =>
        {
            //Equip(equip);
            AramerEquip(equip);
            //同じ武器なので＋はださない
            ShowParameter(character.equip.Copy());
        });

        selectableButton.onHover = (() =>
        {
            OnHoverEqip(equip);
        });

        //if(CanClick())selectableButton.Invalid();
        return selectableButton;
    }

    private SelectableButton CreateItemButton(Accessory equip)
    {
        SelectableButton selectableButton = Instantiate(itemButton);
        selectableButton.GetComponentInChildren<TextMeshProUGUI>().text = equip.name;

        selectableButton.onClick.AddListener(() =>
        {
            //Equip(equip);
            AcssessryEquip(equip);
            //同じ武器なので＋はださない
            ShowParameter(character.equip.Copy());
        });

        selectableButton.onHover = (() =>
        {
            OnHoverEqip(equip);
        });

        //if(CanClick())selectableButton.Invalid();
        return selectableButton;
    }

    private void ClearList()
    {
        itemButtons.ForEach(x => Destroy(x.gameObject));
        itemButtons.Clear();
        eqipDescription.SetText(string.Empty);
    }

    public void OnClickEqip(Equipment equip)
    {
        //ボタンで装備
        Equip(equip);
    }

    public void OnHoverEqip(Weapon equip)
    {
        Equip _equip = character.equip.Copy();
        _equip.weapon = equip;
        ShowParameter(_equip);
        ShowDescription(equip);
    }

    public void OnHoverEqip(Armor equip)
    {
        Equip _equip = character.equip.Copy();
        _equip.armor = equip;
        ShowParameter(_equip);
        ShowDescription(equip);
    }

    public void OnHoverEqip(Accessory equip)
    {
        Equip _equip = character.equip.Copy();
        _equip.accessory = equip;
        ShowParameter(_equip);
        ShowDescription(equip);
    }

    private bool CanClick()
    {
        //誰かが装備しているか
        return false;
    }

    private void ShowDescription(Equipment equip)
    {
        eqipDescription.text = equip.description;
        //他のメンバーが装備しているときそれを伝える
    }

    [Header("パラメータ")]
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI mp;
    public TextMeshProUGUI attack;
    public TextMeshProUGUI defence;
    public TextMeshProUGUI speed;
    //引数を装備スロットの数用意する
    public void ShowParameter(Equip equip)
    {
        //武器の補正を含んだstatus
        //現在の武器補正status - 新しく装備する武器補正status
        //差分の計算
        Equip now = character.equip;
        Equip _new = equip;

        //差分の値
        int m_maxHp = _new.GetMaxHp() - now.GetMaxHp();
        int m_maxMp = _new.GetMaxMp() - now.GetMaxMp();
        int m_attack = _new.GetAttack() - now.GetAttack();
        int m_defence = _new.GetDeffence() - now.GetDeffence();
        int m_speed = _new.GetSpeed() - now.GetSpeed();

        characterName.text = character.GetName();
        //装備の値を含んだステータス
        Status status = character.GetStatus();
        hp.text = status.hp.ToString();
        if (m_maxHp > 0) hp.text += "+" + m_maxHp;
        if (m_maxHp < 0) hp.text += "-" + m_maxHp;
        mp.text = status.mp.ToString();
        if (m_maxMp > 0) hp.text += $"<color=red>+{m_maxMp}</color>";
        if (m_maxMp < 0) hp.text += $"<color=blue>-{m_maxMp}</color>";
        attack.text = status.attack.ToString();
        if (m_attack > 0) hp.text += $"<color=red>+{m_attack}</color>";
        if (m_attack < 0) hp.text += $"<color=blue>-{m_attack}</color>";
        defence.text = status.deffence.ToString();
        if (m_defence > 0) hp.text += $"<color=red>+{m_defence}</color>";
        if (m_defence < 0) hp.text += $"<color=blue>-{m_defence}</color>";
        speed.text = status.speed.ToString();
        if (m_speed > 0) hp.text += $"<color=red>+{m_speed}</color>";
        if (m_speed < 0) hp.text += $"<color=blue>-{m_speed}</color>";
    }

    public void ShowWeopn(Weapon weapon)
    {
        Equip equip = character.equip.Copy();
        equip.weapon = weapon;
    }

    public void WeopnTakeOff()
    {
        //装備を外す
        character.equip.weapon = null;
        Debug.Log("装備を外しました");
        //装備スロットに反映する
        ShowEqipSlot();
        //パラメータに反映する
        ShowParameter(character.equip.Copy());
        //対象の武器を誰も装備していない状態にする
    }

    public void ArmerTakeOff()
    {
        Debug.Log("防具を外しました");
        //装備を外す
        character.equip.armor = null;
        Debug.Log("装備を外しました");
        //装備スロットに反映する
        ShowEqipSlot();
        //パラメータに反映する
        ShowParameter(character.equip.Copy());
        //対象の武器を誰も装備していない状態にする
    }

    public void AccessoryTakeOff()
    {
        Debug.Log("装飾を外しました");
        //装備を外す
        character.equip.accessory = null;
        Debug.Log("装備を外しました");
        //装備スロットに反映する
        ShowEqipSlot();
        //パラメータに反映する
        ShowParameter(character.equip.Copy());
        //対象の武器を誰も装備していない状態にする
    }

    private enum Slot
    {
        weapon, armor, accessory
    }

    private void TakeOff(Slot slot)
    {
        switch (slot)
        {
            case Slot.weapon:
                WeopnTakeOff();
                break;
                //default:
        }
    }

    private void weaponEquip(Weapon weapon)
    {
        character.equip.weapon = weapon;
    }

    private void AramerEquip(Armor armor)
    {
        character.equip.armor = armor;
    }

    private void AcssessryEquip(Accessory weapon)
    {
        character.equip.accessory = weapon;
    }

    public void Equip(Equipment equipment)
    {
        //装備する
        Debug.Log("装備しました");
        //装備スロットに反映する
        //パラメータに反映する
        //対象の武器を対象のキャラが装備している状態にする
    }

    public void Change()
    {
        //誰かが装備している装備を自分に装備する
    }
}
