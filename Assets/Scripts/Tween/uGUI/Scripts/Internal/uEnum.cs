using UnityEngine;
using System.Collections;

namespace uTools {
	public enum PlayDirection {
		Reverse = -1,
		Toggle = 0,
		Forward = 1
	}

	public enum Trigger {
        OnEnable,
		OnPointerEnter,
		OnPointerDown,
		OnPointerClick,
		OnPointerUp,
		OnPointerExit,
        Start,
	}
}