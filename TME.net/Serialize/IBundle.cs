using System.Collections.Generic;

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