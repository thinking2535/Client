using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace uTools {
	[AddComponentMenu("uTools/Tween/Tween Alpha(uTools)")]
	public class uTweenAlpha : uTweenValue {

		public bool includeChilds = false;

		private Light mLight;
		private Material mMat;
		private Image mImage;
		private SpriteRenderer mSpriteRender;
        private CanvasGroup mCanvasGroup;

		float mAlpha = 0f;

		public float alpha {
			get {
				return mAlpha;
			}
			set {
				SetAlpha(transform, value);
				mAlpha = value;
			}
		}

        void Awake() {
            var _transform = transform;
            mLight = _transform.GetComponent<Light>();
            mImage = _transform.GetComponent<Image>();
            mSpriteRender = _transform.GetComponent<SpriteRenderer>();
            mCanvasGroup = _transform.GetComponent<CanvasGroup>();

            if (_transform.GetComponent<Renderer>() != null)
                mMat = _transform.GetComponent<Renderer>().material;
        }

        protected override void ValueUpdate (float value, bool isFinished)
		{
			alpha = value;
		}

		void SetAlpha(Transform _transform, float _alpha) {
			Color c = Color.white;
			if (mLight != null){ 
				c = mLight.color;
				c.a = _alpha;
				mLight.color = c;
			}
			if (mImage != null) {
				c = mImage.color;
				c.a = _alpha;
				mImage.color = c;
			}
			if (mSpriteRender != null) {
				c = mSpriteRender.color;
				c.a = _alpha;
				mSpriteRender.color = c;
			}
            if(mCanvasGroup != null) {
                mCanvasGroup.alpha = _alpha;
            }
            if (mMat != null)
            {
                c = mMat.color;
                c.a = _alpha;
                mMat.color = c;
            }
			if (includeChilds) {
				for (int i = 0; i < _transform.childCount; ++i) {
					Transform child = _transform.GetChild(i);
					SetAlphaChild(child, _alpha);
				}
			}
		}

        void SetAlphaChild(Transform _transform, float _alpha)
        {
            Color c = Color.white;

            Light light = _transform.GetComponent<Light>();
            if (light != null)
            {
                c = light.color;
                c.a = _alpha;
                light.color = c;
            }

            Image image = _transform.GetComponent<Image>();
            if (image != null)
            {
                c = image.color;
                c.a = _alpha;
                image.color = c;
            }

            SpriteRenderer spriteRender = _transform.GetComponent<SpriteRenderer>();
            if (spriteRender != null)
            {
                c = spriteRender.color;
                c.a = _alpha;
                spriteRender.color = c;
            }

            Material mat = null;
            if (_transform.GetComponent<Renderer>() != null)
                mat = _transform.GetComponent<Renderer>().material;

            if (mat != null)
            {
                c = mat.color;
                c.a = _alpha;
                mat.color = c;
            }

            for (int i = 0; i < _transform.childCount; ++i)
            {
                Transform child = _transform.GetChild(i);
                SetAlphaChild(child, _alpha);
            }
        }

		[ContextMenu("Assume value of 'From'")]
		public override void SetCurrentValueToStart() { alpha = from; }

		[ContextMenu("Assume value of 'To'")]
		public override void SetCurrentValueToEnd() { alpha = to; }
	}
}
