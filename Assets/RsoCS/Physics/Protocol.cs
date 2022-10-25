using System;
using System.Collections.Generic;
using rso.core;


namespace rso
{
	namespace physics
	{
		public class SPoint : SProto
		{
			public Single X = default(Single);
			public Single Y = default(Single);
			public SPoint()
			{
			}
			public SPoint(SPoint Obj_)
			{
				X = Obj_.X;
				Y = Obj_.Y;
			}
			public SPoint(Single X_, Single Y_)
			{
				X = X_;
				Y = Y_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref X);
				Stream_.Pop(ref Y);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("X", ref X);
				Value_.Pop("Y", ref Y);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(X);
				Stream_.Push(Y);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("X", X);
				Value_.Push("Y", Y);
			}
			public void Set(SPoint Obj_)
			{
				X = Obj_.X;
				Y = Obj_.Y;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(X) + "," + 
					SEnumChecker.GetStdName(Y);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(X, "X") + "," + 
					SEnumChecker.GetMemberName(Y, "Y");
			}
		}
		public class SPoint3 : SProto
		{
			public Single X = default(Single);
			public Single Y = default(Single);
			public Single Z = default(Single);
			public SPoint3()
			{
			}
			public SPoint3(SPoint3 Obj_)
			{
				X = Obj_.X;
				Y = Obj_.Y;
				Z = Obj_.Z;
			}
			public SPoint3(Single X_, Single Y_, Single Z_)
			{
				X = X_;
				Y = Y_;
				Z = Z_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref X);
				Stream_.Pop(ref Y);
				Stream_.Pop(ref Z);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("X", ref X);
				Value_.Pop("Y", ref Y);
				Value_.Pop("Z", ref Z);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(X);
				Stream_.Push(Y);
				Stream_.Push(Z);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("X", X);
				Value_.Push("Y", Y);
				Value_.Push("Z", Z);
			}
			public void Set(SPoint3 Obj_)
			{
				X = Obj_.X;
				Y = Obj_.Y;
				Z = Obj_.Z;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(X) + "," + 
					SEnumChecker.GetStdName(Y) + "," + 
					SEnumChecker.GetStdName(Z);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(X, "X") + "," + 
					SEnumChecker.GetMemberName(Y, "Y") + "," + 
					SEnumChecker.GetMemberName(Z, "Z");
			}
		}
		public class STransform : SProto
		{
			public SPoint LocalPosition = new SPoint();
			public SPoint3 LocalRotation = new SPoint3();
			public SPoint LocalScale = new SPoint();
			public STransform()
			{
			}
			public STransform(STransform Obj_)
			{
				LocalPosition = Obj_.LocalPosition;
				LocalRotation = Obj_.LocalRotation;
				LocalScale = Obj_.LocalScale;
			}
			public STransform(SPoint LocalPosition_, SPoint3 LocalRotation_, SPoint LocalScale_)
			{
				LocalPosition = LocalPosition_;
				LocalRotation = LocalRotation_;
				LocalScale = LocalScale_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref LocalPosition);
				Stream_.Pop(ref LocalRotation);
				Stream_.Pop(ref LocalScale);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("LocalPosition", ref LocalPosition);
				Value_.Pop("LocalRotation", ref LocalRotation);
				Value_.Pop("LocalScale", ref LocalScale);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(LocalPosition);
				Stream_.Push(LocalRotation);
				Stream_.Push(LocalScale);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("LocalPosition", LocalPosition);
				Value_.Push("LocalRotation", LocalRotation);
				Value_.Push("LocalScale", LocalScale);
			}
			public void Set(STransform Obj_)
			{
				LocalPosition.Set(Obj_.LocalPosition);
				LocalRotation.Set(Obj_.LocalRotation);
				LocalScale.Set(Obj_.LocalScale);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(LocalPosition) + "," + 
					SEnumChecker.GetStdName(LocalRotation) + "," + 
					SEnumChecker.GetStdName(LocalScale);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(LocalPosition, "LocalPosition") + "," + 
					SEnumChecker.GetMemberName(LocalRotation, "LocalRotation") + "," + 
					SEnumChecker.GetMemberName(LocalScale, "LocalScale");
			}
		}
		public class SVector : SProto
		{
			public SPoint Pos = new SPoint();
			public SPoint Dir = new SPoint();
			public SVector()
			{
			}
			public SVector(SVector Obj_)
			{
				Pos = Obj_.Pos;
				Dir = Obj_.Dir;
			}
			public SVector(SPoint Pos_, SPoint Dir_)
			{
				Pos = Pos_;
				Dir = Dir_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Pos);
				Stream_.Pop(ref Dir);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Pos", ref Pos);
				Value_.Pop("Dir", ref Dir);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Pos);
				Stream_.Push(Dir);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Pos", Pos);
				Value_.Push("Dir", Dir);
			}
			public void Set(SVector Obj_)
			{
				Pos.Set(Obj_.Pos);
				Dir.Set(Obj_.Dir);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Pos) + "," + 
					SEnumChecker.GetStdName(Dir);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Pos, "Pos") + "," + 
					SEnumChecker.GetMemberName(Dir, "Dir");
			}
		}
		public class SCollisionInfo : SProto
		{
			public Single Time = default(Single);
			public SPoint Point = new SPoint();
			public SPoint Normal = new SPoint();
			public SCollisionInfo()
			{
			}
			public SCollisionInfo(SCollisionInfo Obj_)
			{
				Time = Obj_.Time;
				Point = Obj_.Point;
				Normal = Obj_.Normal;
			}
			public SCollisionInfo(Single Time_, SPoint Point_, SPoint Normal_)
			{
				Time = Time_;
				Point = Point_;
				Normal = Normal_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Time);
				Stream_.Pop(ref Point);
				Stream_.Pop(ref Normal);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Time", ref Time);
				Value_.Pop("Point", ref Point);
				Value_.Pop("Normal", ref Normal);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Time);
				Stream_.Push(Point);
				Stream_.Push(Normal);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Time", Time);
				Value_.Push("Point", Point);
				Value_.Push("Normal", Normal);
			}
			public void Set(SCollisionInfo Obj_)
			{
				Time = Obj_.Time;
				Point.Set(Obj_.Point);
				Normal.Set(Obj_.Normal);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Time) + "," + 
					SEnumChecker.GetStdName(Point) + "," + 
					SEnumChecker.GetStdName(Normal);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Time, "Time") + "," + 
					SEnumChecker.GetMemberName(Point, "Point") + "," + 
					SEnumChecker.GetMemberName(Normal, "Normal");
			}
		}
		public class SVLine : SProto
		{
			public Single X = default(Single);
			public SVLine()
			{
			}
			public SVLine(SVLine Obj_)
			{
				X = Obj_.X;
			}
			public SVLine(Single X_)
			{
				X = X_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref X);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("X", ref X);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(X);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("X", X);
			}
			public void Set(SVLine Obj_)
			{
				X = Obj_.X;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(X);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(X, "X");
			}
		}
		public class SHLine : SProto
		{
			public Single Y = default(Single);
			public SHLine()
			{
			}
			public SHLine(SHLine Obj_)
			{
				Y = Obj_.Y;
			}
			public SHLine(Single Y_)
			{
				Y = Y_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Y);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Y", ref Y);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Y);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Y", Y);
			}
			public void Set(SHLine Obj_)
			{
				Y = Obj_.Y;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Y);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Y, "Y");
			}
		}
		public class SLine : SProto
		{
			public SPoint Point0 = new SPoint();
			public SPoint Point1 = new SPoint();
			public SLine()
			{
			}
			public SLine(SLine Obj_)
			{
				Point0 = Obj_.Point0;
				Point1 = Obj_.Point1;
			}
			public SLine(SPoint Point0_, SPoint Point1_)
			{
				Point0 = Point0_;
				Point1 = Point1_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Point0);
				Stream_.Pop(ref Point1);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Point0", ref Point0);
				Value_.Pop("Point1", ref Point1);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Point0);
				Stream_.Push(Point1);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Point0", Point0);
				Value_.Push("Point1", Point1);
			}
			public void Set(SLine Obj_)
			{
				Point0.Set(Obj_.Point0);
				Point1.Set(Obj_.Point1);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Point0) + "," + 
					SEnumChecker.GetStdName(Point1);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Point0, "Point0") + "," + 
					SEnumChecker.GetMemberName(Point1, "Point1");
			}
		}
		public class SVSegment : SVLine
		{
			public Single Bottom = default(Single);
			public Single Top = default(Single);
			public SVSegment()
			{
			}
			public SVSegment(SVSegment Obj_) : base(Obj_)
			{
				Bottom = Obj_.Bottom;
				Top = Obj_.Top;
			}
			public SVSegment(SVLine Super_, Single Bottom_, Single Top_) : base(Super_)
			{
				Bottom = Bottom_;
				Top = Top_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref Bottom);
				Stream_.Pop(ref Top);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("Bottom", ref Bottom);
				Value_.Pop("Top", ref Top);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(Bottom);
				Stream_.Push(Top);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("Bottom", Bottom);
				Value_.Push("Top", Top);
			}
			public void Set(SVSegment Obj_)
			{
				base.Set(Obj_);
				Bottom = Obj_.Bottom;
				Top = Obj_.Top;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(Bottom) + "," + 
					SEnumChecker.GetStdName(Top);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(Bottom, "Bottom") + "," + 
					SEnumChecker.GetMemberName(Top, "Top");
			}
		}
		public class SHSegment : SHLine
		{
			public Single Left = default(Single);
			public Single Right = default(Single);
			public SHSegment()
			{
			}
			public SHSegment(SHSegment Obj_) : base(Obj_)
			{
				Left = Obj_.Left;
				Right = Obj_.Right;
			}
			public SHSegment(SHLine Super_, Single Left_, Single Right_) : base(Super_)
			{
				Left = Left_;
				Right = Right_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref Left);
				Stream_.Pop(ref Right);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("Left", ref Left);
				Value_.Pop("Right", ref Right);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(Left);
				Stream_.Push(Right);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("Left", Left);
				Value_.Push("Right", Right);
			}
			public void Set(SHSegment Obj_)
			{
				base.Set(Obj_);
				Left = Obj_.Left;
				Right = Obj_.Right;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(Left) + "," + 
					SEnumChecker.GetStdName(Right);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(Left, "Left") + "," + 
					SEnumChecker.GetMemberName(Right, "Right");
			}
		}
		public class SSegment : SProto
		{
			public SPoint Point0 = new SPoint();
			public SPoint Point1 = new SPoint();
			public SSegment()
			{
			}
			public SSegment(SSegment Obj_)
			{
				Point0 = Obj_.Point0;
				Point1 = Obj_.Point1;
			}
			public SSegment(SPoint Point0_, SPoint Point1_)
			{
				Point0 = Point0_;
				Point1 = Point1_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Point0);
				Stream_.Pop(ref Point1);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Point0", ref Point0);
				Value_.Pop("Point1", ref Point1);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Point0);
				Stream_.Push(Point1);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Point0", Point0);
				Value_.Push("Point1", Point1);
			}
			public void Set(SSegment Obj_)
			{
				Point0.Set(Obj_.Point0);
				Point1.Set(Obj_.Point1);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Point0) + "," + 
					SEnumChecker.GetStdName(Point1);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Point0, "Point0") + "," + 
					SEnumChecker.GetMemberName(Point1, "Point1");
			}
		}
		public class SCircle : SPoint
		{
			public Single Radius = default(Single);
			public SCircle()
			{
			}
			public SCircle(SCircle Obj_) : base(Obj_)
			{
				Radius = Obj_.Radius;
			}
			public SCircle(SPoint Super_, Single Radius_) : base(Super_)
			{
				Radius = Radius_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref Radius);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("Radius", ref Radius);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(Radius);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("Radius", Radius);
			}
			public void Set(SCircle Obj_)
			{
				base.Set(Obj_);
				Radius = Obj_.Radius;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(Radius);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(Radius, "Radius");
			}
		}
		public class SRect : SProto
		{
			public Single Left = default(Single);
			public Single Right = default(Single);
			public Single Bottom = default(Single);
			public Single Top = default(Single);
			public SRect()
			{
			}
			public SRect(SRect Obj_)
			{
				Left = Obj_.Left;
				Right = Obj_.Right;
				Bottom = Obj_.Bottom;
				Top = Obj_.Top;
			}
			public SRect(Single Left_, Single Right_, Single Bottom_, Single Top_)
			{
				Left = Left_;
				Right = Right_;
				Bottom = Bottom_;
				Top = Top_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Left);
				Stream_.Pop(ref Right);
				Stream_.Pop(ref Bottom);
				Stream_.Pop(ref Top);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Left", ref Left);
				Value_.Pop("Right", ref Right);
				Value_.Pop("Bottom", ref Bottom);
				Value_.Pop("Top", ref Top);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Left);
				Stream_.Push(Right);
				Stream_.Push(Bottom);
				Stream_.Push(Top);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Left", Left);
				Value_.Push("Right", Right);
				Value_.Push("Bottom", Bottom);
				Value_.Push("Top", Top);
			}
			public void Set(SRect Obj_)
			{
				Left = Obj_.Left;
				Right = Obj_.Right;
				Bottom = Obj_.Bottom;
				Top = Obj_.Top;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Left) + "," + 
					SEnumChecker.GetStdName(Right) + "," + 
					SEnumChecker.GetStdName(Bottom) + "," + 
					SEnumChecker.GetStdName(Top);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Left, "Left") + "," + 
					SEnumChecker.GetMemberName(Right, "Right") + "," + 
					SEnumChecker.GetMemberName(Bottom, "Bottom") + "," + 
					SEnumChecker.GetMemberName(Top, "Top");
			}
		}
		public class SRectTheta : SRect
		{
			public Single Theta = default(Single);
			public SRectTheta()
			{
			}
			public SRectTheta(SRectTheta Obj_) : base(Obj_)
			{
				Theta = Obj_.Theta;
			}
			public SRectTheta(SRect Super_, Single Theta_) : base(Super_)
			{
				Theta = Theta_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref Theta);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("Theta", ref Theta);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(Theta);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("Theta", Theta);
			}
			public void Set(SRectTheta Obj_)
			{
				base.Set(Obj_);
				Theta = Obj_.Theta;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(Theta);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(Theta, "Theta");
			}
		}
		public class SSector : SCircle
		{
			public Single LeftTheta = default(Single);
			public Single RightTheta = default(Single);
			public SSector()
			{
			}
			public SSector(SSector Obj_) : base(Obj_)
			{
				LeftTheta = Obj_.LeftTheta;
				RightTheta = Obj_.RightTheta;
			}
			public SSector(SCircle Super_, Single LeftTheta_, Single RightTheta_) : base(Super_)
			{
				LeftTheta = LeftTheta_;
				RightTheta = RightTheta_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref LeftTheta);
				Stream_.Pop(ref RightTheta);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("LeftTheta", ref LeftTheta);
				Value_.Pop("RightTheta", ref RightTheta);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(LeftTheta);
				Stream_.Push(RightTheta);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("LeftTheta", LeftTheta);
				Value_.Push("RightTheta", RightTheta);
			}
			public void Set(SSector Obj_)
			{
				base.Set(Obj_);
				LeftTheta = Obj_.LeftTheta;
				RightTheta = Obj_.RightTheta;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(LeftTheta) + "," + 
					SEnumChecker.GetStdName(RightTheta);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(LeftTheta, "LeftTheta") + "," + 
					SEnumChecker.GetMemberName(RightTheta, "RightTheta");
			}
		}
		public class SPointRect : SProto
		{
			public SPoint TopRight = new SPoint();
			public SPoint TopLeft = new SPoint();
			public SPoint BottomLeft = new SPoint();
			public SPoint BottomRight = new SPoint();
			public SPointRect()
			{
			}
			public SPointRect(SPointRect Obj_)
			{
				TopRight = Obj_.TopRight;
				TopLeft = Obj_.TopLeft;
				BottomLeft = Obj_.BottomLeft;
				BottomRight = Obj_.BottomRight;
			}
			public SPointRect(SPoint TopRight_, SPoint TopLeft_, SPoint BottomLeft_, SPoint BottomRight_)
			{
				TopRight = TopRight_;
				TopLeft = TopLeft_;
				BottomLeft = BottomLeft_;
				BottomRight = BottomRight_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref TopRight);
				Stream_.Pop(ref TopLeft);
				Stream_.Pop(ref BottomLeft);
				Stream_.Pop(ref BottomRight);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("TopRight", ref TopRight);
				Value_.Pop("TopLeft", ref TopLeft);
				Value_.Pop("BottomLeft", ref BottomLeft);
				Value_.Pop("BottomRight", ref BottomRight);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(TopRight);
				Stream_.Push(TopLeft);
				Stream_.Push(BottomLeft);
				Stream_.Push(BottomRight);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("TopRight", TopRight);
				Value_.Push("TopLeft", TopLeft);
				Value_.Push("BottomLeft", BottomLeft);
				Value_.Push("BottomRight", BottomRight);
			}
			public void Set(SPointRect Obj_)
			{
				TopRight.Set(Obj_.TopRight);
				TopLeft.Set(Obj_.TopLeft);
				BottomLeft.Set(Obj_.BottomLeft);
				BottomRight.Set(Obj_.BottomRight);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(TopRight) + "," + 
					SEnumChecker.GetStdName(TopLeft) + "," + 
					SEnumChecker.GetStdName(BottomLeft) + "," + 
					SEnumChecker.GetStdName(BottomRight);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(TopRight, "TopRight") + "," + 
					SEnumChecker.GetMemberName(TopLeft, "TopLeft") + "," + 
					SEnumChecker.GetMemberName(BottomLeft, "BottomLeft") + "," + 
					SEnumChecker.GetMemberName(BottomRight, "BottomRight");
			}
		}
		public class SPosTheta : SProto
		{
			public SPoint Pos = new SPoint();
			public Single Theta = default(Single);
			public SPosTheta()
			{
			}
			public SPosTheta(SPosTheta Obj_)
			{
				Pos = Obj_.Pos;
				Theta = Obj_.Theta;
			}
			public SPosTheta(SPoint Pos_, Single Theta_)
			{
				Pos = Pos_;
				Theta = Theta_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Pos);
				Stream_.Pop(ref Theta);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Pos", ref Pos);
				Value_.Pop("Theta", ref Theta);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Pos);
				Stream_.Push(Theta);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Pos", Pos);
				Value_.Push("Theta", Theta);
			}
			public void Set(SPosTheta Obj_)
			{
				Pos.Set(Obj_.Pos);
				Theta = Obj_.Theta;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Pos) + "," + 
					SEnumChecker.GetStdName(Theta);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Pos, "Pos") + "," + 
					SEnumChecker.GetMemberName(Theta, "Theta");
			}
		}
		public class SObjectStraight : SProto
		{
			public Single Time = default(Single);
			public SPosTheta PosTheta = new SPosTheta();
			public Single Speed = default(Single);
			public Single Dist = default(Single);
			public SObjectStraight()
			{
			}
			public SObjectStraight(SObjectStraight Obj_)
			{
				Time = Obj_.Time;
				PosTheta = Obj_.PosTheta;
				Speed = Obj_.Speed;
				Dist = Obj_.Dist;
			}
			public SObjectStraight(Single Time_, SPosTheta PosTheta_, Single Speed_, Single Dist_)
			{
				Time = Time_;
				PosTheta = PosTheta_;
				Speed = Speed_;
				Dist = Dist_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Time);
				Stream_.Pop(ref PosTheta);
				Stream_.Pop(ref Speed);
				Stream_.Pop(ref Dist);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Time", ref Time);
				Value_.Pop("PosTheta", ref PosTheta);
				Value_.Pop("Speed", ref Speed);
				Value_.Pop("Dist", ref Dist);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Time);
				Stream_.Push(PosTheta);
				Stream_.Push(Speed);
				Stream_.Push(Dist);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Time", Time);
				Value_.Push("PosTheta", PosTheta);
				Value_.Push("Speed", Speed);
				Value_.Push("Dist", Dist);
			}
			public void Set(SObjectStraight Obj_)
			{
				Time = Obj_.Time;
				PosTheta.Set(Obj_.PosTheta);
				Speed = Obj_.Speed;
				Dist = Obj_.Dist;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Time) + "," + 
					SEnumChecker.GetStdName(PosTheta) + "," + 
					SEnumChecker.GetStdName(Speed) + "," + 
					SEnumChecker.GetStdName(Dist);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Time, "Time") + "," + 
					SEnumChecker.GetMemberName(PosTheta, "PosTheta") + "," + 
					SEnumChecker.GetMemberName(Speed, "Speed") + "," + 
					SEnumChecker.GetMemberName(Dist, "Dist");
			}
		}
		public class SRectCollider2D : SProto
		{
			public SPoint Size = new SPoint();
			public SPoint Offset = new SPoint();
			public SRectCollider2D()
			{
			}
			public SRectCollider2D(SRectCollider2D Obj_)
			{
				Size = Obj_.Size;
				Offset = Obj_.Offset;
			}
			public SRectCollider2D(SPoint Size_, SPoint Offset_)
			{
				Size = Size_;
				Offset = Offset_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Size);
				Stream_.Pop(ref Offset);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Size", ref Size);
				Value_.Pop("Offset", ref Offset);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Size);
				Stream_.Push(Offset);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Size", Size);
				Value_.Push("Offset", Offset);
			}
			public void Set(SRectCollider2D Obj_)
			{
				Size.Set(Obj_.Size);
				Offset.Set(Obj_.Offset);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Size) + "," + 
					SEnumChecker.GetStdName(Offset);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Size, "Size") + "," + 
					SEnumChecker.GetMemberName(Offset, "Offset");
			}
		}
		public class SStructMove : SProto
		{
			public Boolean IsMoving = default(Boolean);
			public Int32 Direction = default(Int32);
			public Single StoppedDuration = default(Single);
			public SStructMove()
			{
			}
			public SStructMove(SStructMove Obj_)
			{
				IsMoving = Obj_.IsMoving;
				Direction = Obj_.Direction;
				StoppedDuration = Obj_.StoppedDuration;
			}
			public SStructMove(Boolean IsMoving_, Int32 Direction_, Single StoppedDuration_)
			{
				IsMoving = IsMoving_;
				Direction = Direction_;
				StoppedDuration = StoppedDuration_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref IsMoving);
				Stream_.Pop(ref Direction);
				Stream_.Pop(ref StoppedDuration);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("IsMoving", ref IsMoving);
				Value_.Pop("Direction", ref Direction);
				Value_.Pop("StoppedDuration", ref StoppedDuration);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(IsMoving);
				Stream_.Push(Direction);
				Stream_.Push(StoppedDuration);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("IsMoving", IsMoving);
				Value_.Push("Direction", Direction);
				Value_.Push("StoppedDuration", StoppedDuration);
			}
			public void Set(SStructMove Obj_)
			{
				IsMoving = Obj_.IsMoving;
				Direction = Obj_.Direction;
				StoppedDuration = Obj_.StoppedDuration;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(IsMoving) + "," + 
					SEnumChecker.GetStdName(Direction) + "," + 
					SEnumChecker.GetStdName(StoppedDuration);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(IsMoving, "IsMoving") + "," + 
					SEnumChecker.GetMemberName(Direction, "Direction") + "," + 
					SEnumChecker.GetMemberName(StoppedDuration, "StoppedDuration");
			}
		}
		public class SStructMovePosition : SStructMove
		{
			public SPoint LocalPosition = new SPoint();
			public SStructMovePosition()
			{
			}
			public SStructMovePosition(SStructMovePosition Obj_) : base(Obj_)
			{
				LocalPosition = Obj_.LocalPosition;
			}
			public SStructMovePosition(SStructMove Super_, SPoint LocalPosition_) : base(Super_)
			{
				LocalPosition = LocalPosition_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref LocalPosition);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("LocalPosition", ref LocalPosition);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(LocalPosition);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("LocalPosition", LocalPosition);
			}
			public void Set(SStructMovePosition Obj_)
			{
				base.Set(Obj_);
				LocalPosition.Set(Obj_.LocalPosition);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(LocalPosition);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(LocalPosition, "LocalPosition");
			}
		}
		public class SBoxCollider2D : STransform
		{
			public SRectCollider2D RectCollider2D = new SRectCollider2D();
			public SBoxCollider2D()
			{
			}
			public SBoxCollider2D(SBoxCollider2D Obj_) : base(Obj_)
			{
				RectCollider2D = Obj_.RectCollider2D;
			}
			public SBoxCollider2D(STransform Super_, SRectCollider2D RectCollider2D_) : base(Super_)
			{
				RectCollider2D = RectCollider2D_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref RectCollider2D);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("RectCollider2D", ref RectCollider2D);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(RectCollider2D);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("RectCollider2D", RectCollider2D);
			}
			public void Set(SBoxCollider2D Obj_)
			{
				base.Set(Obj_);
				RectCollider2D.Set(Obj_.RectCollider2D);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(RectCollider2D);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(RectCollider2D, "RectCollider2D");
			}
		}
	}
}
