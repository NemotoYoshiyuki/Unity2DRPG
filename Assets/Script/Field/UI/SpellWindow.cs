using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellWindow : BaseWindow
{
    // Start is called before the first frame update
    void Start()
    {
        party = PlayerParty.instance.partyMember;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject list;
    public MenuItem menuItem;
    public TextMeshProUGUI spellDescription;
    public CharacterWindow characterWindow;
    public FieldEffect fieldEffect;

    public PlayerCharacter owner;
    private List<PlayerCharacter> party;
    private SpellData hoverItem;
    private SpellData selectedItem;

    public void OpenWindow()
    {
        party = PlayerParty.instance.partyMember;
        characterWindow.OnSelect((int index) =>
        {
            owner = party[index];
            OpenSpellList(owner);
        });
    }

    public void OpenSpellList(PlayerCharacter owner)
    {
        gameObject.SetActive(true);

        List<SpellData> spellDatas = owner.GetSpells();

        for (int i = 0; i < spellDatas.Count; i++)
        {
            MenuItem _menuItem = Instantiate(menuItem);
            _menuItem.index = i;
            _menuItem.text.SetText(spellDatas[i].skillName);

            _menuItem.transform.parent = list.transform;

            _menuItem.onHover += (int index) =>
            {
                hoverItem = spellDatas[index];
                ObjectHoveredEnter(index);
            };

            _menuItem.onLeftClick += (int index) =>
            {
                selectedItem = spellDatas[index];
                fieldEffect.Spell(selectedItem,owner);
                gameObject.SetActive(false);
            };
        }
    }

    public void ObjectHoveredEnter(int index)
    {
        spellDescription.SetText(hoverItem.description);
    }
}
