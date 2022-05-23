using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace rso
{
    namespace Base
    {
        public class CListEnum<TData> : IEnumerator
        {
            CList<TData>.SNode _NewedHead;
            CList<TData>.SNode _Current = null;

            public CListEnum(CList<TData>.SNode NewedHead_)
            {
                _NewedHead = NewedHead_;
            }
            public bool MoveNext()
            {
                if (_Current == null)
                    _Current = _NewedHead;
                else
                    _Current = _Current.Next;

                return (_Current != null);
            }
            public void Reset()
            {
            }
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
            public TData Current
            {
                get
                {
                    return _Current.Data;
                }
            }
        }
        public class CList<TData> : IEnumerable
        {
            public struct SIterator
            {
                public SNode Node;
                public Int32 Index
                {
                    get
                    {
                        return Node.Index;
                    }
                }
                public TData Data
                {
                    get
                    {
                        return Node.Data;
                    }
                    set
                    {
                        Node.Data = value;
                    }
                }
                public SIterator(SNode Node_ = null)
                {
                    Node = Node_;
                }
                public bool MoveNext()
                {
                    Node = Node.Next;
                    return (Node != null);
                }
                public static implicit operator bool(SIterator it_)
                {
                    return (it_.Node != null && it_.Node.Data != null);
                }
            }
            public class SNode
            {
                public Int32 Index;
                public SNode Next;
                public SNode Prev;
                public TData Data;

                public SIterator Iterator
                {
                    get
                    {
                        return new SIterator(this);
                    }
                }
            }

            List<SNode> _Nodes = new List<SNode>();
            SNode _DeletedHead = null;
            SNode _DeletedTail = null;
            SNode _NewedHead = null;
            SNode _NewedTail = null;
            Int32 _Size = 0;

            void _AttachToNewed(SNode Node_)
            {
                Node_.Next = null;

                if (_NewedTail == null)
                {
                    Node_.Prev = null;
                    _NewedHead = _NewedTail = Node_;
                }
                else
                {
                    Node_.Prev = _NewedTail;
                    _NewedTail.Next = Node_;
                    _NewedTail = Node_;
                }
            }
            void _AttachToDeleted(SNode Node_)
            {
                Node_.Next = null;

                if (_DeletedTail == null)
                {
                    Node_.Prev = null;
                    _DeletedHead = _DeletedTail = Node_;
                }
                else
                {
                    Node_.Prev = _DeletedTail;
                    _DeletedTail.Next = Node_;
                    _DeletedTail = Node_;
                }
            }
            void _Detach(SNode Node_)
            {
                if (Node_.Prev != null)
                    Node_.Prev.Next = Node_.Next;
                if (Node_.Next != null)
                    Node_.Next.Prev = Node_.Prev;
            }
            void _DetachFromDeleted(SNode Node_)
            {
                _Detach(Node_);

                if (Node_.Prev == null)
                    _DeletedHead = Node_.Next;
                if (Node_.Next == null)
                    _DeletedTail = Node_.Prev;
            }
            public TData this[Int32 Index_]
            {
                get
                {
                    return _Nodes[Index_].Data;
                }
            }
            public SIterator Get(Int32 Index_)
            {
                if (Index_ < 0 ||
                     Index_ >= _Nodes.Count)
                    return new SIterator();

                return _Nodes[Index_].Iterator;
            }
            public SIterator Begin()
            {
                if (_NewedHead == null)
                    return End();

                return _NewedHead.Iterator;
            }
            public SIterator End()
            {
                return new SIterator();
            }
            public TData First()
            {
                return _NewedHead.Data;
            }
            public TData Last()
            {
                return _NewedTail.Data;
            }
            public Int32 Count
            {
                get
                {
                    return _Size;
                }
            }
            public Int32 NewIndex
            {
                get
                {
                    if (_DeletedHead != null)
                        return _DeletedHead.Index;
                    else
                        return _Nodes.Count;
                }
            }
            public SIterator Add(TData Data_)
            {
                SNode Node = null;

                // DetachFromDeleted ////////////////////////////////////
                if (_DeletedHead != null)
                {
                    _DeletedHead.Data = Data_;
                    Node = _DeletedHead;
                    _DetachFromDeleted(Node);
                }
                else
                {
                    _Nodes.Add(new SNode());
                    _Nodes.Last().Data = Data_;

                    Node = _Nodes.Last();
                    Node.Index = _Nodes.Count - 1;
                }

                _AttachToNewed(Node);

                ++_Size;

                return Node.Iterator;
            }
            public SIterator AddAt(Int32 Index_, TData Data_)
            {
                SNode Node = null;

                // _Nodes 범위 이내 이면
                if (Index_ < _Nodes.Count)
                {
                    if (_Nodes[Index_].Data != null)
                        throw new Exception();

                    _Nodes[Index_].Data = Data_;
                    Node = _Nodes[Index_];
                }
                else
                {
                    var LastSize = _Nodes.Count;

                    for (var i = LastSize; i < Index_ + 1; ++i)
                    {
                        _Nodes.Add(new SNode());
                        _Nodes.Last().Index = _Nodes.Count - 1;
                        _AttachToDeleted(_Nodes.Last());
                    }

                    _Nodes.Last().Data = Data_;
                    Node = _Nodes.Last();
                }

                _DetachFromDeleted(Node);
                _AttachToNewed(Node);

                ++_Size;

                return Node.Iterator;
            }
            bool _Remove(SNode Node_)
            {
                if (Node_.Data == null)
                    return false;

                _Detach(Node_);

                if (Node_.Prev == null)
                    _NewedHead = Node_.Next;
                if (Node_.Next == null)
                    _NewedTail = Node_.Prev;

                // AttachToDeleted ////////////////////
                _AttachToDeleted(Node_);

                --_Size;

                Node_.Data = default(TData);

                return true;
            }
            public bool Remove(SIterator It_)
            {
                return _Remove(It_.Node);
            }
            public bool Remove(Int32 Index_)
            {
                if (Index_ < 0 ||
                    Index_ >= _Nodes.Count)
                    return false;

                return _Remove(_Nodes[Index_]);
            }
            public bool RemoveLast()
            {
                if (_NewedTail == null)
                    return false;

                return _Remove(_NewedTail);
            }
            public void Clear()
            {
                while (_NewedHead != null)
                    Remove(_NewedHead.Index);
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public CListEnum<TData> GetEnumerator()
            {
                return new CListEnum<TData>(_NewedHead);
            }
            public Int32 RemoveAll(Predicate<TData> match)
            {
                Int32 OldCount = Count;
                for (var it = Begin(); it;)
                {
                    var itCheck = it;
                    it.MoveNext();

                    if (match(itCheck.Data))
                        Remove(itCheck);
                }

                return (OldCount - Count);
            }
        }
    }
}