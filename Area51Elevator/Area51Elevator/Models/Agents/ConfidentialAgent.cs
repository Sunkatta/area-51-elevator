using System;
using System.Collections.Generic;
using System.Text;

namespace Area51Elevator
{
    public sealed class ConfidentialAgent : AgentBase
    {
        public ConfidentialAgent(string name, Elevator elevator)
            :base(name, elevator)
        {
            SecurityLevel = SecurityLevel.Confidential;
        }
    }
}
