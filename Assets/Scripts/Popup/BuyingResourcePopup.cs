using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BuyingResourcePopup : BasePopup
{
    [SerializeField] Text _ResourcePopupText = null;
    [SerializeField] Image _targetResourceIcon;
    [SerializeField] Image _costResourceIcon;
    [SerializeField] Text _Cost = null;
    [SerializeField] Slider _Slider = null;
    [SerializeField] CustomButton _SliderLeftButton = null;
    [SerializeField] CustomButton _SliderRightButton = null;

    ExchangeValue _exchangeValue;
    double _rate;
    Int32 _sliderMinCount;
    Int32 _sliderMaxCount;
    Int32 _sliderCount;

    protected override void Awake()
    {
        base.Awake();

        _Slider.onValueChanged.AddListener(OnValueChanedSlider);
        _SliderLeftButton.onClick.AddListener(OnClickSliderLeftButton);
        _SliderRightButton.onClick.AddListener(OnClickSliderRightButton);
    }
    protected override void OnDestroy()
    {
        _SliderRightButton.onClick.RemoveAllListeners();
        _SliderLeftButton.onClick.RemoveAllListeners();
        _Slider.onValueChanged.RemoveAllListeners();

        base.OnDestroy();
    }
    protected override void _ok()
    {
        _okWithReturnValue(_sliderCount);
    }
    void _updateText()
    {
        _Cost.text = _sliderCount.ToString() + "/" + _sliderMaxCount.ToString();
        okButton.text = _exchangeValue.getCostValue(_sliderCount).ToString();
    }
    public void init(EResource targetResource, Int32 targetResourceMinValue, Int32 targetResourceMaxValue, ExchangeValue exchangeValue, string message)
    {
        base.init(EText.Global_Popup_Confirm, false);

        confirmEnabled = false;
        _exchangeValue = exchangeValue;
        _rate = _exchangeValue.rate;
        _sliderMinCount = _sliderCount = targetResourceMinValue;
        _sliderMaxCount = targetResourceMaxValue;
        _targetResourceIcon.sprite = CGlobal.GetResourceSprite(targetResource);
        _costResourceIcon.sprite = CGlobal.GetResourceSprite(_exchangeValue.costResourceType);
        _ResourcePopupText.text = message;

        // _Slider 값 초기화 시에 OnClickSliderLeftButton 가 호출될 수 있으므로 _exchangeValue 초기화 이후 설정
        _Slider.value = _sliderCount;
        _Slider.minValue = _sliderMinCount;
        _Slider.maxValue = _sliderMaxCount;
        _SliderLeftButton.repeatEnabled = true;
        _SliderLeftButton.repeatTime = 0.01f;
        _SliderRightButton.repeatEnabled = true;
        _SliderRightButton.repeatTime = 0.01f;

        _updateText();
    }
    public void OnValueChanedSlider(float value)
    {
        var newSliderCount = (Int32)value;
        if (newSliderCount == _sliderCount)
            return;

        _sliderCount = newSliderCount;
        _updateText();
    }
    public void OnClickSliderLeftButton()
    {
        if (_sliderCount <= _sliderMinCount)
            return;

        _sliderCount -= 1;
        _Slider.value = _sliderCount;
        _updateText();
    }
    public void OnClickSliderRightButton()
    {
        if (_sliderCount >= _sliderMaxCount)
            return;

        _sliderCount += 1;
        _Slider.value = _sliderCount;
        _updateText();
    }
}
