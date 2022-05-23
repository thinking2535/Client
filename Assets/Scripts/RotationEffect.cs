using UnityEngine;
using System.Collections;

public enum RotationAxis {
	X = 0,
	Y = 1, 
	Z = 2,
}

public class RotationEffect : MonoBehaviour
{
	public RotationAxis rotationAxis = RotationAxis.Y;
	public float rotationSpeed = 100.0f;

	public Transform cacheTransfrom
	{
		get
		{
			if (_cacheTransfrom == null)
				_cacheTransfrom = transform;

			return _cacheTransfrom;
		}
	}

	private Transform _cacheTransfrom = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch(rotationAxis) {
		case RotationAxis.X:
				cacheTransfrom.Rotate (Vector3.right * rotationSpeed * Time.deltaTime);
			break;
		case RotationAxis.Y:
				cacheTransfrom.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);
			break;
		case RotationAxis.Z:
				cacheTransfrom.Rotate (Vector3.forward * rotationSpeed * Time.deltaTime);
			break;
		}
	}
}
