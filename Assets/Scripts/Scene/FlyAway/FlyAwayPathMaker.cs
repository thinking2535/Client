using bb;
using rso.gameutil;
using System;
using UnityEngine;

/// <summary>
/// 섬과 아이템의 배치는 외부에서 담당
/// 세로 영역의 최소, 최대 값을 입력받고
/// 이 구간을 세로로 (mainLevel * subLevel) 로 나누고
/// 나눠진 구간을 yStep으로 하고
/// 목적지 는 level 로 나눠진 y로 설정하고
/// 값 가져오는 함수 호출시 최소 값에서 yStep의 정수배 한 값을 반환
/// 위, 가운데, 아래 3가지로 랜덤 방향 선택하되 직전 방향은 선택되지 않도록 %2 로 랜덤값 취함
/// 방향이 결정되면 그 방향으로 움직일 수 있는 level 수 이내의 값을 랜덤으로 결정
/// 방향이 중앙이라면 mainLevel * subLevel 의 최대 값 이내의 값을 랜덤으로 결정해서 반복
/// </summary>
public class FlyAwayPathMaker
{
    readonly Single _minY;
    readonly Single _maxY;
    readonly Int32 _mainLevelCount;
    readonly Int32 _subLevelCount;
    public Single verticalStep { get; }

    CFixedRandom32 _fixedRandom;
    public FlyAwayPathMakerState state { get; } // currentCount 상,하 방향과 와 횡방향의 로직 통일을 위해 이 변수를 두어 상,하 방향에도 _destCount 까지 반복하여 목표 도달을 체크

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

            // 경로를 진행시킬 방향(newLevelDirection) 결정
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
                if (newLevelDirection >= state.levelDirection) ++newLevelDirection; // _lastDirection 를 피하여 랜덤값 설정
            }

            // 결정된 방향으로 반복할 회수(_destCount) 결정
            if (newLevelDirection == 0) // mid
            {
                // 전체 level 개수 이내의 랜덤값 만큼 반복
                state.destCount = ((((Int32)_fixedRandom.Get()) % _mainLevelCount) + 1) * _subLevelCount;
            }
            else
            {
                if (newLevelDirection == -1) // down
                {
                    // 직전 _destMainLevel 아래 영역(_destMainLevel 의 크기)에서 결정된 changedMainLevel 에 _subLevelCount 를 곱한 값 만큼 반복
                    var changedMainLevel = (((Int32)_fixedRandom.Get()) % state.destMainLevel) + 1;
                    state.destCount = changedMainLevel * _subLevelCount;
                    state.destMainLevel -= changedMainLevel;
                }
                else // up
                {
                    // 직전 _destMainLevel 위 영역(_mainLevelCount - _destMainLevel)에서 결정된 changedMainLevel 에 _subLevelCount 를 곱한 값 만큼 반복
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
