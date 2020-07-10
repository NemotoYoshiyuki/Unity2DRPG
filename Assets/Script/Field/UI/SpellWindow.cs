using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpellWindow : BaseWindow
{
    public GameObject list;
    public SpellSlot spellSlotPrefab;

    [HideInInspector] public int selectedItemIndex;
    private List<SpellSlot> spellSlots = new List<SpellSlot>();
    private List<SelectableButton> selectables = new List<SelectableButton>();

    public TextMeshProUGUI spellDescription;
    public CharacterSelect selectTarget;
    public CharacterWindow characterWindow;
    public FieldEffect fieldEffect;

    //public PlayerCharacter owner;//選択対象のキャラ
    public CharacterData owner;//選択対象のキャラ
    //private List<PlayerCharacter> party;
    private List<CharacterData> party;
    private Spell selectedItem;

    private SideMenu sideMenu;
    private MenuGuide menuGuide;

    public Action OnCancel;

    private void OnEnable()
    {
        MenuWindow.instance.sideMenu.Lock();
    }

    private void OnDisable()
    {
        MenuWindow.instance.sideMenu.Unlock();
    }

    public override void Open()
    {
        MenuWindow.instance.focusWindow = this;
        menuGuide = MenuWindow.instance.menuGuide;

        UserSelect();
    }

    public override void Close()
    {
        base.Close();
    }

    public void UserSelect()
    {
        //ターゲットリストを表示
        party = Party.GetMember();
        menuGuide.Show("だれが呪文を使用しますか");

        characterWindow.Select(0);
        characterWindow.ClearLisner();
        characterWindow.AddLisner((int index) =>
        {
            owner = party[index];
            ShowSpellList(owner);
        });

        ////キャンセルが押されたら
        OnCancel = () =>
        {
            MenuWindow.instance.focusWindow = MenuWindow.instance;
            menuGuide.Hide();
            selectTarget.Release();
        };
    }

    public void ShowSpellList(CharacterData owner)
    {
        //アイテムリストを表示
        gameObject.SetActive(true);

        ClearSpellList();
        CreateSpellList(owner);
        spellSlots[0].selectable.Select();
        ShowSpellDescription(spellSlots[0].spell);

        //キャンセルが押されたら
        OnCancel = () =>
        {
            HideSpellList();
            UserSelect();
        };
    }

    //public void ShowSpellList(PlayerCharacter owner)
    //{
    //    //アイテムリストを表示
    //    gameObject.SetActive(true);

    //    ClearSpellList();
    //    CreateSpellList(owner);
    //    spellSlots[0].selectable.Select();
    //    ShowSpellDescription(spellSlots[0].spell);

    //    //キャンセルが押されたら
    //    OnCancel = () =>
    //    {
    //        HideSpellList();
    //        UserSelect();
    //    };
    //}

    private void CreateSpellList(CharacterData playerCharacter)
    {
        //List<SpellData> spellDatas = playerCharacter.GetSpells();
        List<Spell> spellDatas = playerCharacter.GetSpells();

        if (spellDatas.Count == 0) return;

        //ボタン作成
        for (int i = 0; i < spellDatas.Count; i++)
        {
            Spell item = spellDatas[i];
            SpellSlot spellSlot = CreateButton(item);
            spellSlot.index = i;
            spellSlot.transform.SetParent(list.transform);
            spellSlots.Add(spellSlot);
            selectables.Add(spellSlot.selectable);
        }
        RegisterNavigation();
    }

    //private void CreateSpellList(PlayerCharacter playerCharacter)
    //{
    //    List<SpellData> spellDatas = playerCharacter.GetSpells();

    //    if (spellDatas.Count == 0) return;

    //    //ボタン作成
    //    for (int i = 0; i < spellDatas.Count; i++)
    //    {
    //        SpellData item = spellDatas[i];
    //        SpellSlot spellSlot = CreateButton(item);
    //        spellSlot.index = i;
    //        spellSlot.transform.SetParent(list.transform);
    //        spellSlots.Add(spellSlot);
    //        selectables.Add(spellSlot.selectable);
    //    }
    //    RegisterNavigation();
    //}

    public void RefreshSpellList()
    {
        ClearSpellList();
        CreateSpellList(owner);

        spellSlots[selectedItemIndex].selectable.Select();
        ShowSpellDescription(spellSlots[selectedItemIndex].spell);
    }

    private void ClearSpellList()
    {
        spellSlots.ForEach(x => Destroy(x.gameObject));
        spellSlots.Clear();
        selectables.Clear();
    }

    private void ShowSpellDescription(Spell spellData)
    {
        spellDescription.SetText(spellData.description);
    }

    private SpellSlot CreateButton(Spell spellData)
    {
        SpellSlot spellSlot = Instantiate(spellSlotPrefab);
        spellSlot.SetUp(spellData);

        //クリック動作
        spellSlot.selectable.onClick.AddListener(() =>
        {
            selectedItemIndex = spellSlot.index;
            OnItemButtonClick(spellData);
        });

        spellSlot.selectable.onHover = (() => OnSpellButtonHover(spellData));

        //if (CanFieldSpell(spellData) == false) spellSlot.selectable.interactable = false;
        if (CanFieldSpell(spellData) == false) spellSlot.Invalid();
        return spellSlot;
    }

    private void OnItemButtonClick(Spell spellData)
    {
        UseSpell(spellData);
    }

    private void OnSpellButtonHover(Spell spellData)
    {
        ShowSpellDescription(spellData);
    }

    private void UseSpell(Spell spellData)
    {
        selectedItem = spellData;
        HideSpellList();
        fieldEffect.UseSpell(selectedItem, owner);

        OnCancel = () =>
        {
            ShowSpellList(owner);
        };
    }

    public void HideSpellList()
    {
        ClearSpellList();
        gameObject.SetActive(false);
    }

    public bool CanFieldSpell(Spell spellData)
    {
        if (spellData.useType != UseType.戦闘中)
        {
            if (owner.status.mp <= spellData.mp) return false;
            return true;
        }
        return false;
    }

    public override void Cancel()
    {
        OnCancel.Invoke();
    }

    private void RegisterNavigation()
    {
        StartCoroutine(CreateNavigation());
    }

    //Button.Navigation.modeのExplicitに適切なButtonを割り当てます
    private IEnumerator CreateNavigation()
    {
        //Layoutgroupを使用しているため、レイアウトの構築が完了するまで待つ必要があります
        yield return new WaitForEndOfFrame();

        foreach (var item in selectables)
        {
            item.FindSelectable(selectables);
        }
        yield break;
    }
}
