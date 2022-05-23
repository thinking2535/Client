using System;

namespace rso
{
    namespace core
    {
        class CVector<TData>
        {
            TData[] _Datas = new TData[1];
            Int32 _Size = 0;

            public TData[] Data
            {
                get
                {
                    return _Datas;
                }
            }
            public Int32 Size
            {
                get
                {
                    return _Size;
                }
            }
            public TData this[Int32 Index_]
            {
                get
                {
                    return _Datas[Index_];
                }
                set
                {
                    _Datas[Index_] = value;
                }
            }
            public void Set(CVector<TData> Obj_)
            {
                if (Obj_._Size > _Size)
                    _Datas = new TData[Obj_._Size];

                _Size = Obj_._Size;

                for (Int32 i = 0; i < Obj_._Size; ++i)
                    _Datas[i] = Obj_._Datas[i];
            }
            public void PushBack(TData Data_)
            {
                if (_Size == _Datas.Length)
                {
                    TData[] NewDatas = new TData[_Datas.Length * 2];
                    Array.Copy(_Datas, NewDatas, _Datas.Length);
                    _Datas = NewDatas;
                }

                _Datas[_Size] = Data_;
                ++_Size;
            }
            public void PopBack()
            {
                if (_Size == 0)
                    return;

                --_Size;
            }
            public void Resize(Int32 Size_)
            {
                if (Size_ > _Datas.Length)
                {
                    TData[] NewDatas = new TData[Size_];
                    Array.Copy(_Datas, NewDatas, _Datas.Length);
                    _Datas = NewDatas;
                }
                _Size = Size_;
            }
            public void Clear()
            {
                _Size = 0;
            }
        }
    }
}