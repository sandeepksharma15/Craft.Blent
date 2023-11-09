using Craft.Blent.Contracts;
using Craft.Blent.Enums;

namespace Craft.Blent.Fluid;

public class FluidOverflow : IFluidOverflow, IFluidOverflowSecondRule
{
    private OverflowType overflowType;
    private OverflowType secondOverflowType;

    private bool dirty = true;
    private string classNames;

    public string Class(IClassProvider classProvider)
    {
        if (dirty)
        {
            if (overflowType != OverflowType.Default)
                classNames = classProvider.Overflow(overflowType, secondOverflowType);

            dirty = false;
        }

        return classNames;
    }

    private void Dirty()
        => dirty = true;

    public IFluidOverflowSecondRule WithOverflow(OverflowType overflowType)
    {
        this.overflowType = overflowType;
        Dirty();

        return this;
    }

    public IFluidOverflowSecondRule WithSecondOverflow(OverflowType overflowType)
    {
        secondOverflowType = overflowType;
        Dirty();

        return this;
    }

    public IFluidOverflow Visible => WithSecondOverflow(OverflowType.Visible);
    public IFluidOverflow Hidden => WithSecondOverflow(OverflowType.Hidden);
    public IFluidOverflow Scroll => WithSecondOverflow(OverflowType.Scroll);
    public IFluidOverflow Auto => WithSecondOverflow(OverflowType.Auto);
}
