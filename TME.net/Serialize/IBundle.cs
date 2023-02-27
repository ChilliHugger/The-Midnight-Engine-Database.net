using System.Collections.Generic;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Serialize
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Bundle : Dictionary<string, object?>
    {
    }

    public interface IBundle
    {
        bool Load(IBundleReader bundle);
    }
}