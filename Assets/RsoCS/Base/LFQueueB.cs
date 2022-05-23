namespace rso
{
    namespace Base
    {
        public class CLFQueueB<TData> where TData : new()
        {
            class _SNode
            {
                public volatile bool EndNode = true;
                public TData pData = default(TData);
                public _SNode pNext = null;
            }

            // 헤드와 테일 사이에 데이터가 있는 노드가 자리하게 됨. ( 헤드와 테일은 데이터 포인터가 0 )
            // 이런 구조를 가지는 이유는 모든 노드를 Pop 하더라도 헤드와 테일이 겹치지 않도록 하여 서로 다른 스레드가 헤드, 테일 모두를 액세스 하지 않도록 하기 위함
            _SNode _pPoppedHead = null;
            _SNode _pPoppedTail = null;
            _SNode _pPushedHead = null;
            _SNode _pPushedTail = null;
            _SNode _pNewPushNode = null;

            public CLFQueueB()
            {
                _pPoppedHead = new _SNode();
                _pPoppedTail = new _SNode();
                _pPushedHead = new _SNode();
                _pPushedTail = new _SNode();
                _pPoppedHead.pNext = _pPoppedTail;
                _pPushedHead.pNext = _pPushedTail;
            }
            ~CLFQueueB()
            {
                Dispose();
            }
            public void Dispose()
            {
                for (var pNode = _pPoppedHead;
                    pNode != null;)
                {
                    if (pNode.pData != null)
                        pNode.pData = default(TData);

                    var pDelNode = pNode;
                    pNode = pNode.pNext;
                    pDelNode = null;
                }

                for (var pNode = _pPushedHead;
                    pNode != null;)
                {
                    if (pNode.pData != null)
                        pNode.pData = default(TData);

                    var pDelNode = pNode;
                    pNode = pNode.pNext;
                    pDelNode = null;
                }

                _pPoppedHead = null;
                _pPoppedTail = null;
                _pPushedHead = null;
                _pPushedTail = null;

                if (_pNewPushNode != null)
                    _pNewPushNode = null;
            }
            public TData GetPushBuf()
            {
                if (_pNewPushNode != null)
                    return _pNewPushNode.pData;

                if (!_pPoppedHead.pNext.EndNode)
                {
                    _pNewPushNode = _pPoppedHead.pNext;
                    _pPoppedHead.pNext = _pPoppedHead.pNext.pNext;
                }
                else
                {
                    try
                    {
                        _pNewPushNode = new _SNode();

                        try
                        {
                            _pNewPushNode.pData = new TData();
                        }
                        catch
                        {
                            _pNewPushNode = null;
                            throw;
                        }
                    }
                    catch
                    {
                        return default(TData);
                    }
                }

                return _pNewPushNode.pData;
            }
            public void Push()
            {
                if (_pNewPushNode == null)
                    return;

                // 새로운 공간은 Tail 뒤에 붙이되 Tail 에 새로운 공간의 값을 복사, Prev 노드의 pNext 를 수정하지 않아도 되도록 하기 위함 ( 스레드 안전 )
                _pPushedTail.pData = _pNewPushNode.pData;
                _pPushedTail.pNext = _pNewPushNode;
                _pNewPushNode.EndNode = true;
                _pNewPushNode.pData = default(TData);
                _pNewPushNode.pNext = null;
                _pPushedTail.EndNode = false;
                _pPushedTail = _pNewPushNode;
                _pNewPushNode = null;
            }
            public TData GetPopBuf()
            {
                if (_pPushedHead.pNext.EndNode)
                    return default(TData);

                return _pPushedHead.pNext.pData;
            }
            public void Pop()
            {
                if (_pPushedHead.pNext.EndNode)
                    return;

                // 노드의 링크를 조작하지 않고, 노드의 내용을 복사하는이유는 CLFQueueB 의 Pop계열이 Push 계열과 Thread Safe 하도록 하기 위함
                var pNewNode = _pPushedHead.pNext;
                _pPushedHead.pNext = _pPushedHead.pNext.pNext;
                _pPoppedTail.pData = pNewNode.pData;
                _pPoppedTail.pNext = pNewNode;
                pNewNode.EndNode = true;
                pNewNode.pData = default(TData);
                pNewNode.pNext = null;
                _pPoppedTail.EndNode = false;
                _pPoppedTail = pNewNode;
            }
            public void Clear()    // no thread safe
            {
                for (var pBuf = GetPopBuf();
                    pBuf != null;
                    pBuf = GetPopBuf())
                    Pop();
            }
        }
    }
}
