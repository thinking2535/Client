//      ______  _____
//      |     ||  |  |
//      | | | ||    -|
//  THE |_|_|_||__|__|
//  -------------------------------------------------------
//  Copyright 2017-2018 Stair Games Corp. All Rights Reserved.
//  Author: ParkSungJun

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace uTools
{
    [AddComponentMenu("uTools/Tween/Tween Alpha New(uTools)")]
    public class uTweenAlphaNew : uTweenValue
    {
        public bool includeChilds = true;
        
        private class ValueInfo
        {
            public UnityEngine.Object _component;
            public float _originValue;
        }
        private List<ValueInfo> _valueInfos = new List<ValueInfo>();

        float mAlpha = 0f;

        public float alpha
        {
            get
            {
                return mAlpha;
            }
            set
            {
                this.SetAlpha(value);
                mAlpha = value;
            }
        }

        void Awake()
        {
            var _transform = transform;

            if (includeChilds)
            {
                CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
                if(canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                    canvasGroup.alpha = 1.0f;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.ignoreParentGroups = false;
                    canvasGroup.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSave;
                }

                if (canvasGroup != null)
                    _valueInfos.Add(new ValueInfo() { _component = canvasGroup, _originValue = canvasGroup.alpha });
            }
            else
            {
                Light light = _transform.GetComponent<Light>();
                if(light != null)
                    _valueInfos.Add(new ValueInfo() { _component = light, _originValue = light.color.a });

                Image image = _transform.GetComponent<Image>();
                if(image != null)
                    _valueInfos.Add(new ValueInfo() { _component = image, _originValue = image.color.a });

                SpriteRenderer spriteRenderer = _transform.GetComponent<SpriteRenderer>();
                if(spriteRenderer != null)
                    _valueInfos.Add(new ValueInfo() { _component = spriteRenderer, _originValue = spriteRenderer.color.a });

                Renderer renderer = _transform.GetComponent<Renderer>();
                if(renderer != null && renderer.material != null && renderer.material.HasProperty("_Color"))
                    _valueInfos.Add(new ValueInfo() { _component = renderer.material, _originValue = renderer.material.color.a });
            }
        }

        protected override void ValueUpdate(float value, bool isFinished)
        {
            alpha = value;
        }

        void SetAlpha(float _alpha)
        {
            Color c = Color.white;

#if UNITY_EDITOR
            if (Application.isPlaying == false && (_valueInfos == null || _valueInfos.Count <= 0))
            {
                Awake();
            }
#endif

            foreach(var valueInfo in _valueInfos)
            {
                if (valueInfo._component is Light)
                {
                    Light light = valueInfo._component as Light;
                    c = light.color;
                    c.a = valueInfo._originValue * _alpha;
                    light.color = c;
                }
                else if (valueInfo._component is Image)
                {
                    Image image = valueInfo._component as Image;
                    c = image.color;
                    c.a = valueInfo._originValue * _alpha;
                    image.color = c;
                }
                else if (valueInfo._component is SpriteRenderer)
                {
                    SpriteRenderer spriteRenderer = valueInfo._component as SpriteRenderer;
                    c = spriteRenderer.color;
                    c.a = valueInfo._originValue * _alpha;
                    spriteRenderer.color = c;
                }
                else if (valueInfo._component is Material)
                {
                    Material material = valueInfo._component as Material;
                    c = material.color;
                    c.a = valueInfo._originValue * _alpha;
                    material.color = c;
                }
                else if (valueInfo._component is CanvasGroup)
                {
                    CanvasGroup canvasGroup = valueInfo._component as CanvasGroup;
                    canvasGroup.alpha = valueInfo._originValue * _alpha;
                }
            }
        }

		[ContextMenu("Assume value of 'From'")]
		public override void SetCurrentValueToStart() { alpha = from; }
	}
}
