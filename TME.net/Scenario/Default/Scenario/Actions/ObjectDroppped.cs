using System;
using System.Linq;
using System.Threading.Tasks;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Scenario.Actions
{
    public class ObjectDroppped : IAction
    {
        private readonly IDatabase _database;
     
        public ObjectDroppped(IDatabase database)
        {
            _database = database;
        }

        public Task<IResult> Run(params object[] args)
        {
            if (args.FirstOrDefault() is IThing thing
                && thing.CarriedBy is IMappable carrier)
            {
                // get the location details
                var currentLocation = carrier.Location;
                var loc = _database.GameMap.GetAt(currentLocation);

                // this object is no longer being carried
                thing.CarriedBy = null;

                // drop the object at the current location
                loc.Thing = (ThingType)thing.RawId;

                // update the map with unique objects
                if (thing.IsUnique)
                {
                    loc.HasObject = true;
                    thing.Location = currentLocation;
                }

                return Task.FromResult(Success.Default);
            }

            return Task.FromResult(Failure.Default);
        }
    }
}
