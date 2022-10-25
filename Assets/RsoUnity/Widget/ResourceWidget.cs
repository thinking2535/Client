using System;
using UnityEngine;
using UnityEngine.UI;

public class ResourceWidget : MonoBehaviour
{
    class PoppingIconScale
    {
        readonly float _PopScale;
        readonly float _ScaleVelocity;
        bool _IsPopping = false;
        DateTime _LastUpdateDateDateTime; // Time.time 은 정밀도가 낮아지므로
        public float CurrentScale { get; private set; } = 1.0f;

        public PoppingIconScale(float PopScale_, float Duration_)
        {
            _PopScale = PopScale_;
            _ScaleVelocity = (1.0f - PopScale_) / Duration_;
        }
        public void Pop()
        {
            if (_IsPopping)
                return;

            _IsPopping = true;
            CurrentScale = _PopScale;
            _LastUpdateDateDateTime = DateTime.Now;
        }
        public bool Update() // true : need to draw
        {
            if (!_IsPopping)
                return false;

            var Now = DateTime.Now;
            var ElapsedDuration = (Now - _LastUpdateDateDateTime);

            CurrentScale += (_ScaleVelocity * (float)ElapsedDuration.TotalSeconds);
            _LastUpdateDateDateTime = Now;

            if (CurrentScale < 1.0f)
            {
                CurrentScale = 1.0f;
                _IsPopping = false;
            }

            return true;
        }
    }

    static readonly TimeSpan _AnimateDuration = TimeSpan.FromSeconds(1.0);

    [SerializeField] protected Image _Icon;
    [SerializeField] protected Text _Text;
    string _MaxValueString;
    Int32 _StartValue = 0;
    Int32 _CurrentValue = 0;
    Int32 _GoalValue = 0;
    DateTime _AnimateStartDateTime;
    PoppingIconScale _PopingIconScale = new PoppingIconScale(1.2f, 0.02f);
    public void Init(Sprite Icon_, Int32 MaxValue_, Int32 Value_)
    {
        _Icon.sprite = Icon_;

        if (MaxValue_ == Int32.MaxValue)
            _MaxValueString = "";
        else
            _MaxValueString = " / " + MaxValue_.ToString();

        _CurrentValue = _GoalValue = Value_;
        _UpdateText();
    }
    public void SetValue(Int32 Value_)
    {
        _StartValue = _CurrentValue;
        _GoalValue = Value_;
        _AnimateStartDateTime = DateTime.Now;
    }
    public void SetValueWithoutAnimation(Int32 Value_)
    {
        _CurrentValue = _GoalValue = Value_;
        _UpdateText();
        _Icon.transform.localScale = Vector2.one;
    }
    void Update()
    {
        if (_CurrentValue != _GoalValue)
        {
            var ElapsedTimeSpan = DateTime.Now - _AnimateStartDateTime;
            Int32 NewValue;
            if (ElapsedTimeSpan >= _AnimateDuration)
                NewValue = _GoalValue;
            else
                NewValue = _StartValue + (Int32)((_GoalValue - _StartValue) * (ElapsedTimeSpan.TotalSeconds / _AnimateDuration.TotalSeconds));

            if (NewValue != _CurrentValue)
            {
                if (NewValue > _CurrentValue)
                    _PopingIconScale.Pop();

                _CurrentValue = NewValue;
                _UpdateText();
            }
        }

        // rso todo moneyui 흘러들어가는 연출 적용 후
        //if (_PopingIconScale.Update())
        //    _Icon.transform.localScale = new Vector2(_PopingIconScale.CurrentScale, _PopingIconScale.CurrentScale);
    }
    void _UpdateText()
    {
        _Text.text = _CurrentValue.ToString() + _MaxValueString;
    }
}