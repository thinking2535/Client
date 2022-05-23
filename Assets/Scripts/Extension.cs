using rso.physics;
using bb;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Extension
{
    public static Vector2 ToVector2(this SPoint Point_)
    {
        return new Vector2(Point_.X, Point_.Y);
    }
    public static Vector3 ToVector3(this SPoint Point_)
    {
        return new Vector3(Point_.X, Point_.Y, 0.0f);
    }
    public static SPoint ToSPoint(this Vector2 Vector_)
    {
        return new SPoint(Vector_.x, Vector_.y);
    }
    public static SPoint ToSPoint(this Vector3 Vector_)
    {
        return new SPoint(Vector_.x, Vector_.y);
    }
    public static SRectCollider2D GetEngineRect(this BoxCollider2D Collider_)
    {
        // C++ 코드와 통일
        return new SRectCollider2D(
            Collider_.size.ToSPoint(),
            Collider_.offset.ToSPoint(),
            Collider_.transform.localScale.ToSPoint());
    }

    public static void GetStructures(this GameObject GameObject_, out SPoint PropPosition_, List<SStructure> Structures_, List<SStructureMove> StructureMoves_)
    {
        Debug.Log("GetStructures Begin");
        Debug.Log("GetStructures Begin 2");
        Debug.Log("GetStructures Begin : " + CGlobal.c_PropName);

        var Prop = GameObject_.transform.Find(CGlobal.c_PropName);

        Debug.Log("GetStructures 0");

        Debug.Assert(Prop != null);

        Debug.Log("GetStructures 1");

        PropPosition_ = Prop.position.ToSPoint();

        Debug.Log("GetStructures 2");

        for (Int32 i = 0; i < Prop.childCount; ++i)
        {
            Debug.Log("GetStructures 3");

            var tf = Prop.GetChild(i);

            var us = tf.gameObject.GetComponentInChildren<EngineUnityStructure>();
            if (us == null)
            {
                var BoxCollider2Ds = tf.gameObject.GetComponentsInChildren<BoxCollider2D>();
                if (BoxCollider2Ds.Length == 0)
                    continue;

                foreach (var c in BoxCollider2Ds)
                    Structures_.Add(new SStructure(c.GetEngineRect(), tf.localPosition.ToSPoint()));
            }
            else
            {
                var BoxCollider2Ds = tf.gameObject.GetComponentsInChildren<BoxCollider2D>();
                if (BoxCollider2Ds.Length == 0)
                    throw new Exception("EngineUnityStructure Need BoxCollider2D");

                var Colliders = new List<SRectCollider2D>();

                foreach (var c in BoxCollider2Ds)
                    Colliders.Add(c.GetEngineRect());

                StructureMoves_.Add(new SStructureMove(Colliders, us.BeginPos.ToSPoint(), us.EndPos.ToSPoint(), us.Velocity, us.Delay));
            }
        }

        Debug.Log("GetStructures End");
    }
}