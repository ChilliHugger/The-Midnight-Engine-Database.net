using System.Linq;
using System.Threading.Tasks;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Types;

namespace TME.Scenario.Default.Actions
{
    public class ObjectDropped : BaseAction
    {
        private readonly IMap _map;
     
        public ObjectDropped(IMap map)
        {
            _map = map;
        }

        //
        // args[0] is object
        //
        protected override Task<IResult> OnExecute(params object[] args)
        {
            if (args.FirstOrDefault() is IThingInternal {IsCarried: true} thing)
            {
                // get the location details
                var currentLocation = thing.CarriedBy!.Location;
                var loc = _map.GetAt(currentLocation);

                // this object is no longer being carried
                thing.UpdateCarriedBy(null);

                // drop the object at the current location
                loc.Thing = (ThingType)thing.RawId;

                // update the map with unique objects
                if (thing.IsUnique && thing is IMappableInternal mappable)
                {
                    loc.HasObject = true;
                    mappable.UpdateLocation(currentLocation);
                }

                _map.SetAt(currentLocation, ref loc);
                
                return Task.FromResult(Success.Default);
            }

            return Task.FromResult(Failure.Default);
        }

        public override Task<IResult> CanExecute(params object[] args)
        {
            return Task.FromResult(args.FirstOrDefault() is IThingInternal {IsCarried: true}
                ? Success.Default 
                : Failure.Default);
        }
    }
}
