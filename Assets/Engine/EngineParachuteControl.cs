using bb;
using rso.physics;
using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

//  vel     scale   on			    off             clear

//  +	    +m      n/a			    n/a             n/a
//  +	    c	    err		    	ok(v=-1)        ok(vel=0, scale=0, off)
//  +	    -0	    err		    	ok(v=0, off)    ok(vel=0, off)

//  0	    +m	    err			    ok(v=-1)        ok(scale=0, off)
//  0	    c	    n/a		    	n/a             n/a
//  0	    -0	    ok(v=1, on)		err             nothing to do

//  -	    +m	    ok(v=0)			err             ok(vel=0, scale=0, off)
//  -	    c	    ok(v=1)			err             ok(vel=0, scale=0, off)
//  -	    -0	    n/a			    n/a             n/a

public class CEngineParachuteControl
{
    const float c_Velocity = 1.0f;
    public delegate void FOn(bool On_);

    FOn _fOn;
    SParachuteInfo _ParachuteInfo = null;
    public CEngineParachuteControl(FOn fOn_, SParachuteInfo ParachuteInfo_)
    {
        _fOn = fOn_;
        _ParachuteInfo = ParachuteInfo_;
    }
    public void On()
    {
        if (_ParachuteInfo.Velocity > 0.0f ||
            (_ParachuteInfo.Velocity == 0.0f && _ParachuteInfo.Scale >= global.c_ParachuteLocalScale))
            return;

        if (_ParachuteInfo.Scale < global.c_ParachuteLocalScale)
        {
            _ParachuteInfo.Velocity = c_Velocity;

            if (_ParachuteInfo.Scale <= 0.0f)
                _fOn(true);
        }
        else // _ParachuteInfo.Scale == global.c_ParachuteLocalScale
        {
            _ParachuteInfo.Velocity = 0.0f;
        }
    }
    public void Off()
    {
        if (_ParachuteInfo.Velocity < 0.0f ||
            (_ParachuteInfo.Velocity == 0.0f && _ParachuteInfo.Scale <= 0.0f))
            return;

        if (_ParachuteInfo.Scale > 0.0f)
        {
            _ParachuteInfo.Velocity = -c_Velocity;
        }
        else // _ParachuteInfo.Scale == 0.0f
        {
            _ParachuteInfo.Velocity = 0.0f;
            _fOn(false);
        }
    }
    public bool FixedUpdate()
    {
        if (_ParachuteInfo.Velocity == 0.0f)
            return false;

        _ParachuteInfo.Scale += (_ParachuteInfo.Velocity * CEngine.DeltaTime);

        if (_ParachuteInfo.Velocity > 0.0f)
        {
            if (_ParachuteInfo.Scale < global.c_ParachuteLocalScale)
                return true;

            _ParachuteInfo.Scale = global.c_ParachuteLocalScale;
            _ParachuteInfo.Velocity = 0.0f;
        }
        else
        {
            if (_ParachuteInfo.Scale > 0.0f)
                return true;

            _ParachuteInfo.Scale = 0.0f;
            _ParachuteInfo.Velocity = 0.0f;
            _fOn(false);
        }

        return true;
    }
    public void Clear()
    {
        bool Call = (_ParachuteInfo.Scale > 0.0f || _ParachuteInfo.Velocity > 0.0f);
        
        _ParachuteInfo.Scale = 0.0f;
        _ParachuteInfo.Velocity = 0.0f;

        if (Call)
            _fOn(false);
    }
}
