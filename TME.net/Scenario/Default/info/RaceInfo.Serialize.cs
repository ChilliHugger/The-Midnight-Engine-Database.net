using TME.Serialize;

namespace TME.Scenario.Default.info
{
    public partial class RaceInfo
    {
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            DefaultSoldiersName = ctx.Reader.String();
            Success = ctx.Reader.UInt32();
            InitialMovement = ctx.Reader.UInt32();
            DiagonalModifier = ctx.Reader.UInt32();
            RidingMultiplier = ctx.Reader.UInt32();
            MovementMax = ctx.Reader.UInt32();
            BaseRestAmount = ctx.Reader.UInt32();
            StrongholdStartups = ctx.Reader.UInt32();
            MistTimeAffect = ctx.Reader.Int32();
            MistDespondencyAffect = ctx.Reader.Int32();
            BaseEnergyCost = ctx.Reader.Int32();
            BaseEnergyCostHorse = ctx.Reader.Int32();

            // TODO: Fix database error correctly
            if (IsSymbol("RA_MORKIN"))
            {
                BaseEnergyCost = 2;
                BaseEnergyCostHorse = 4;
            }
            
            if ( ctx.IsDatabase )
            {
                (BaseEnergyCost, BaseEnergyCostHorse) = (BaseEnergyCostHorse, BaseEnergyCost);
            }
            //
            
            return true;
        }

        public override bool Load(IBundleReader bundle)
        {
            if(!base.Load(bundle)) return false;

            DefaultSoldiersName = bundle.String(nameof(DefaultSoldiersName));
            Success = bundle.UInt32(nameof(Success));
            InitialMovement = bundle.UInt32(nameof(InitialMovement));
            DiagonalModifier = bundle.UInt32(nameof(DiagonalModifier));
            RidingMultiplier = bundle.UInt32(nameof(RidingMultiplier));
            MovementMax = bundle.UInt32(nameof(MovementMax));
            BaseRestAmount = bundle.UInt32(nameof(BaseRestAmount));
            StrongholdStartups = bundle.UInt32(nameof(StrongholdStartups));
            MistTimeAffect = bundle.Int32(nameof(MistTimeAffect));
            MistDespondencyAffect = bundle.Int32(nameof(MistDespondencyAffect));
            BaseEnergyCost = bundle.Int32(nameof(BaseEnergyCost));
            BaseEnergyCostHorse = bundle.Int32(nameof(BaseEnergyCostHorse));
       
            return true;
        }
    }
}