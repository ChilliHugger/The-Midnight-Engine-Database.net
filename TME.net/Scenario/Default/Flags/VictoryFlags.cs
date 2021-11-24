using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum VictoryFlags : uint
    {           
        GameOver = 1 << 0,    // Triggers Game Over
        Enabled =  1 << 1,    // Is Enabled
        Complete = 1 << 2     // 
    }
}