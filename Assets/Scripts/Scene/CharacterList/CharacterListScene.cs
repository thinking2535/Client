using bb;
using rso.Base;
using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class CharacterListScene : MoneyUIScene
{
    [SerializeField] CharacterPanel _characterPanelPrefab;
    [SerializeField] Button _BackButton;
    [SerializeField] Text _title;
    public GameObject ActiveCharListParent = null;
    public GameObject DeactiveCharListParent = null;
    public GameObject CharacterListContent = null;
    public Text TipText = null;
    private Dictionary<Int32, CharacterPanel> _CharPanels = new Dictionary<int, CharacterPanel>();
    Int32 _LastSelectedCode;

    public new void init()
    {
        base.init();

        _BackButton.onClick.AddListener(Back);
        _LastSelectedCode = CGlobal.LoginNetSc.User.SelectedCharCode;

        foreach (var Char in CGlobal.MetaData.Characters)
        {
            Transform Parent;

            if (CGlobal.LoginNetSc.doesHaveCharacter(Char.Key))
                Parent = ActiveCharListParent.transform;
            else
                Parent = DeactiveCharListParent.transform;

            var Panel = Object.Instantiate(_characterPanelPrefab, Parent);
            Panel.transform.localScale = Vector3.one;
            Panel.transform.localPosition = new Vector3(Panel.transform.localPosition.x, Panel.transform.localPosition.y, 0.0f);
            Panel.init(Char.Value);
            _CharPanels.Add(Char.Key, Panel);
        }

        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Character);
    }
    protected override void Awake()
    {
        base.Awake();

        _title.text = CGlobal.MetaData.getText(EText.SceneLobby_BtnText_Character);
    }
    protected override void Update()
    {
        base.Update();

        TipText.text = string.Format("{0}/{1}", CGlobal.LoginNetSc.Chars.Count(), CGlobal.MetaData.Characters.Count());
    }
    protected override void OnDestroy()
    {
        _BackButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    protected override async Task<bool> _backButtonPressed()
    {
        if (await base._backButtonPressed())
            return true;

        Back();
        return true;
    }
    public override async void ShowCharacterInfoPopup(SCharacterMeta CharacterMeta_)
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        await pushCharacterInfoPopup(CharacterMeta_, false, false);

        if (CGlobal.LoginNetSc.User.SelectedCharCode != _LastSelectedCode)
        {
            _CharPanels[_LastSelectedCode].isSelected = false;
            _CharPanels[CGlobal.LoginNetSc.User.SelectedCharCode].isSelected = true;
            _LastSelectedCode = CGlobal.LoginNetSc.User.SelectedCharCode;
        }
    }
    public override void UpdateResources()
    {
        CGlobal.MoneyUI.UpdateResources();
    }
    public override void buyCharacterNetSc(Int32 characterCode)
    {
        var dialog = _dialogController.peek() as CharacterInfoPopup;
        if (dialog != null)
            dialog.buyCharacterNetSc();

        _CharPanels[characterCode].isNew = true;
        _CharPanels[characterCode].isDisabled = false;
        _CharPanels[characterCode].transform.SetParent(ActiveCharListParent.transform);
    }
    public void Back()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.sceneController.pop();
    }
}
