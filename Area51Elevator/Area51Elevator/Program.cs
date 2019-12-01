using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Area51Elevator
{
    class Program
    {
        static void Main(string[] args)
        {
            Elevator area51Elevator = new Elevator();

            List<AgentBase> agents = new List<AgentBase>()
            {
                new ConfidentialAgent("Sheldon Cooper", area51Elevator),
                new ConfidentialAgent("Brojack Horseman", area51Elevator),
                new ConfidentialAgent("Tupac Shakur", area51Elevator),
                new SecretAgent("Homer Simpson", area51Elevator),
                new SecretAgent("Peter Griffin", area51Elevator),
                new SecretAgent("Michael Jackson", area51Elevator),
                new TopSecretAgent("Barrack Obama", area51Elevator),
                new TopSecretAgent("Donald Trump", area51Elevator),
                new TopSecretAgent("Osama bin Laden", area51Elevator)
            };

            var threads = agents.Select(a => new Thread(a.ChooseArea51Action)).ToArray();
            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
    }
}
