using bb;
using System;

public class ResourceTypeData : SResourceTypeData
{
    public ResourceTypeData()
    {
    }
    public ResourceTypeData(SResourceTypeData super) :
        base(super)
    {
    }
    public ResourceTypeData(EResource type, Int32 data) :
        base(type, data)
    {
    }
    public static ResourceTypeData operator -(ResourceTypeData self)
    {
        var ret = new ResourceTypeData(self);
        ret.Data = -ret.Data;
        return ret;
    }
    public static ResourceTypeData operator -(ResourceTypeData self, SResourceTypeData value)
    {
        var ret = new ResourceTypeData(self);
        ret.Data -= value.Data;
        return ret;
    }
}
