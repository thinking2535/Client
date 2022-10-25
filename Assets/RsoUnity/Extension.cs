using rso.physics;
using UnityEditor;
using UnityEngine;

namespace rso.unity
{
    public static class Extension
    {
        public static Vector2 ToVector2(this SPoint Self_)
        {
            return new Vector2(Self_.X, Self_.Y);
        }
        public static Vector3 ToVector3(this SPoint Self_)
        {
            return new Vector3(Self_.X, Self_.Y, 0.0f);
        }
        public static Vector3 ToVector3(this SPoint3 Self_)
        {
            return new Vector3(Self_.X, Self_.Y, Self_.Z);
        }
        public static SPoint ToSPoint(this Vector2 Self_)
        {
            return new SPoint(Self_.x, Self_.y);
        }
        public static SPoint ToSPoint(this Vector3 Self_)
        {
            return new SPoint(Self_.x, Self_.y);
        }
        public static SPoint3 ToSPoint3(this Vector3 Self_)
        {
            return new SPoint3(Self_.x, Self_.y, Self_.z);
        }
        public static STransform ToSTransform(this Transform Self_)
        {
            return new STransform(
                Self_.localPosition.ToSPoint(),
                Self_.localEulerAngles.ToSPoint3(),
                Self_.localScale.ToSPoint());
        }

        public static SRectCollider2D ToSRectCollider2D(this BoxCollider2D Collider_)
        {
            return new SRectCollider2D(Collider_.size.ToSPoint(), Collider_.offset.ToSPoint());
        }
        public static CRectCollider2D ToCRectCollider2D(this BoxCollider2D Collider_)
        {
            return new CRectCollider2D(CPhysics.GetDefaultTransform(Collider_.transform.localPosition.ToSPoint()), 0, Collider_.ToSRectCollider2D());
        }
        public static SBoxCollider2D ToSBoxCollider2D(this BoxCollider2D Collider_)
        {
            return new SBoxCollider2D(
                Collider_.transform.ToSTransform(),
                Collider_.ToSRectCollider2D());
        }
#if UNITY_EDITOR
        public static string getFullPrefabPathNameWithoutExtension(this GameObject self)
        {
            var prefabFilePath = AssetDatabase.GetAssetPath(self);
            var extensionIndex = prefabFilePath.LastIndexOf(".prefab");
            return prefabFilePath.Substring(0, extensionIndex);
        }
#endif
    }
}