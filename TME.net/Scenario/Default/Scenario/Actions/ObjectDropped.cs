using System.Linq;
using System.Threading.Tasks;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Scenario.Actions
{
    public class ObjectDropped : BaseAction
    {
        private readonly IDatabase _database;
     
        public ObjectDropped(IDatabase database)
        {
            _database = database;
        }

        //
        // args[0] is object
        //
        protected override Task<IResult> OnExecute(params object[] args)
        {
            if (args.FirstOrDefault() is IThingInternal {CarriedBy: IMappable carrier} thing)
            {
                // get the location details
                var currentLocation = carrier.Location;
                var loc = _database.GameMap.GetAt(currentLocation);

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

                _database.GameMap.SetAt(currentLocation, ref loc);
                
                return Task.FromResult(Success.Default);
            }

            return Task.FromResult(Failure.Default);
        }

        protected override Task<IResult> CanExecute(params object[] args)
        {
            return Task.FromResult(args.FirstOrDefault() is IThingInternal {CarriedBy: IMappable carrier} 
                ? Success.Default 
                : Failure.Default);
        }
    }
}
