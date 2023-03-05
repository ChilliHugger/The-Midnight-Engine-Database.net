using TME.Interfaces;
using TME.Scenario.Default.Actions.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Types;

namespace TME.Scenario.Default.Actions
{
    public class ObjectDroppedAction : IObjectDroppedAction
    {
        private readonly IMap _map;
     
        public ObjectDroppedAction(IMap map)
        {
            _map = map;
        }

        //
        // args[0] is object
        //
        public IResult Execute(IObject arg)
        {
            if (arg is not Object thing || !CanExecute(thing))
            {
                return Failure.Default;
            }
            
            // get the location details
            var currentLocation = thing.CarriedBy!.Location;
            var loc = _map.GetAt(currentLocation);

            // this object is no longer being carried
            thing.CarriedBy = null;

            // drop the object at the current location
            loc.Thing = (ThingType)thing.RawId;

            // update the map with unique objects
            if (thing.IsUnique && thing is IMappableInternal mappable)
            {
                loc.HasObject = true;
                mappable.UpdateLocation(currentLocation);
            }

            _map.SetAt(currentLocation, ref loc);
                
            return Success.Default;

        }

        public bool CanExecute(IObject arg) => arg.IsCarried;
        
    }
}
