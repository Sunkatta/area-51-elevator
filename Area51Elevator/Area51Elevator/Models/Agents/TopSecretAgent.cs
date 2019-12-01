using System;
using System.Collections.Generic;
using System.Text;

namespace Area51Elevator
{
    public sealed class TopSecretAgent : AgentBase
    {
        public TopSecretAgent(string name, Elevator elevator)
            :base(name, elevator)
        {
            SecurityLevel = SecurityLevel.TopSecret;
        }
    }
}
