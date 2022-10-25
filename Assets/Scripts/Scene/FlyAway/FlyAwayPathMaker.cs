using bb;
using rso.gameutil;
using System;
using UnityEngine;

/// <summary>
/// ���� �������� ��ġ�� �ܺο��� ���
/// ���� ������ �ּ�, �ִ� ���� �Է¹ް�
/// �� ������ ���η� (mainLevel * subLevel) �� ������
/// ������ ������ yStep���� �ϰ�
/// ������ �� level �� ������ y�� �����ϰ�
/// �� �������� �Լ� ȣ��� �ּ� ������ yStep�� ������ �� ���� ��ȯ
/// ��, ���, �Ʒ� 3������ ���� ���� �����ϵ� ���� ������ ���õ��� �ʵ��� %2 �� ������ ����
/// ������ �����Ǹ� �� �������� ������ �� �ִ� level �� �̳��� ���� �������� ����
/// ������ �߾��̶�� mainLevel * subLevel �� �ִ� �� �̳��� ���� �������� �����ؼ� �ݺ�
/// </summary>
public class FlyAwayPathMaker
{
    readonly Single _minY;
    readonly Single _maxY;
    readonly Int32 _mainLevelCount;
    readonly Int32 _subLevelCount;
    public Single verticalStep { get; }

    CFixedRandom32 _fixedRandom;
    public FlyAwayPathMakerState state { get; } // currentCount ��,�� ����� �� Ⱦ������ ���� ������ ���� �� ������ �ξ� ��,�� ���⿡�� _destCount ���� �ݺ��Ͽ� ��ǥ ������ üũ

    public FlyAwayPathMaker(Single minY, Single maxY, Int32 mainLevelCount, Int32 subLevelCount, CFixedRandom32 fixedRandom) :
        this(
            minY,
            maxY,
            mainLevelCount,
            subLevelCount,
            fixedRandom,
            new FlyAwayPathMakerState(
                (mainLevelCount / 2) * subLevelCount,
                (((Int32)fixedRandom.Get()) % 3) - 1,
                mainLevelCount / 2,
                0,
                0))
    {
    }
    public FlyAwayPathMaker(Single minY, Single maxY, Int32 mainLevelCount, Int32 subLevelCount, CFixedRandom32 fixedRandom, FlyAwayPathMakerState state)
    {
        Debug.Assert(mainLevelCount > 2);
        Debug.Assert(subLevelCount > 0);

        _minY = minY;
        _maxY = maxY;
        _mainLevelCount = mainLevelCount;
        _subLevelCount = subLevelCount;
        verticalStep = (_maxY - _minY) / (mainLevelCount * subLevelCount);

        _fixedRandom = fixedRandom;
        this.state = state;
    }
    public Single getNextY()
    {
        if (state.currentCount == state.destCount)
        {
            state.currentCount = 0;

            // ��θ� �����ų ����(newLevelDirection) ����
            Int32 newLevelDirection;
            if (state.levelDirection == 0 && state.destMainLevel == 0)
            {
                newLevelDirection = 1;
            }
            else if (state.levelDirection == 0 && state.destMainLevel == _mainLevelCount - 1)
            {
                newLevelDirection = -1;
            }
            else
            {
                newLevelDirection = (((Int32)_fixedRandom.Get()) % 2) - 1;
                if (newLevelDirection >= state.levelDirection) ++newLevelDirection; // _lastDirection �� ���Ͽ� ������ ����
            }

            // ������ �������� �ݺ��� ȸ��(_destCount) ����
            if (newLevelDirection == 0) // mid
            {
                // ��ü level ���� �̳��� ������ ��ŭ �ݺ�
                state.destCount = ((((Int32)_fixedRandom.Get()) % _mainLevelCount) + 1) * _subLevelCount;
            }
            else
            {
                if (newLevelDirection == -1) // down
                {
                    // ���� _destMainLevel �Ʒ� ����(_destMainLevel �� ũ��)���� ������ changedMainLevel �� _subLevelCount �� ���� �� ��ŭ �ݺ�
                    var changedMainLevel = (((Int32)_fixedRandom.Get()) % state.destMainLevel) + 1;
                    state.destCount = changedMainLevel * _subLevelCount;
                    state.destMainLevel -= changedMainLevel;
                }
                else // up
                {
                    // ���� _destMainLevel �� ����(_mainLevelCount - _destMainLevel)���� ������ changedMainLevel �� _subLevelCount �� ���� �� ��ŭ �ݺ�
                    var changedMainLevel = (((Int32)_fixedRandom.Get()) % ((_mainLevelCount - 1) - state.destMainLevel)) + 1;
                    state.destCount = changedMainLevel * _subLevelCount;
                    state.destMainLevel += changedMainLevel;
                }
            }

            state.levelDirection = newLevelDirection;
        }

        ++state.currentCount;
        state.currentLevel += state.levelDirection;

        return getCurrentY();
    }
    public Single getCurrentY()
    {
        return _minY + verticalStep * state.currentLevel;
    }
}
