using System;
using System.Collections.Generic;
using System.Text;

namespace Area51Elevator
{
    public sealed class SecretAgent : AgentBase
    {
        public SecretAgent(string name, Elevator elevator)
            :base(name, elevator)
        {
            SecurityLevel = SecurityLevel.Secret;
        }
    }
}
