using Area51Elevator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Area51Elevator
{
    public abstract class AgentBase
    {
        private bool isAtArea51 = true;
        private int baseAction;
        private Floor desiredFloorNumber;
        private Floor currentFloor = Floor.GroundFloor;
        private static Random rand = new Random();
        private Elevator elevator;

        public AgentBase(string name, Elevator elevator)
        {
            Name = name;
            this.elevator = elevator;
        }

        public virtual string Name { get; set; }
        public virtual SecurityLevel SecurityLevel { get; set; }
        public bool WantsToGoHome { get; set; }

        public virtual void ChooseArea51Action()
        {
            while (isAtArea51)
            {
                baseAction = rand.Next(1, 4);

                switch (baseAction)
                {
                    case 1:
                        DoSomeWorkAtCurrentFloor(currentFloor);
                        break;
                    case 2:
                        CallElevator();
                        break;
                    default:
                        WantsToGoHome = true;
                        if (currentFloor == Floor.GroundFloor)
                        {
                            GoHome();
                        }
                        else
                        {
                            CallElevator();
                        }
                        break;
                }
            }
        }

        public void DoSomeWorkAtCurrentFloor(Floor updatedCurrentFloor)
        {
            currentFloor = updatedCurrentFloor;
            Console.WriteLine($"Agent {Name} is currently at {currentFloor} floor.");
            if(WantsToGoHome)
            {
                GoHome();
            }
            Thread.Sleep(5000);
            ChooseArea51Action();
        }

        private void GoHome()
        {
            isAtArea51 = false;
            Console.WriteLine($"Agent {Name} is going home.");
        }

        private void CallElevator()
        {
            Console.WriteLine($"Agent {Name} is waiting for the elevator.");
            if (elevator.ElevatorDoor.CurrentFloor == currentFloor)
            {
                EnterElevator();
            }
            else
            {
                elevator.Move(elevator.ElevatorDoor.CurrentFloor, currentFloor);
                EnterElevator();
            }
        }

        public void EnterElevator()
        {
            desiredFloorNumber = (Floor)rand.Next(1, 5);
            elevator.Enter(this, currentFloor, desiredFloorNumber);
        }
    }
}
