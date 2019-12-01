using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Area51Elevator
{
    public sealed class Elevator
    {
        private static Random rand = new Random();

        public Elevator()
        {
            ElevatorDoor = new ElevatorDoor(Floor.GroundFloor);
            Semaphore = new Semaphore(1, 1);
        }

        public ElevatorDoor ElevatorDoor { get; set; }
        public Semaphore Semaphore { get; set; }
        public AgentBase Passenger { get; set; }

        public void Enter(AgentBase a, Floor currentFloorNumber, Floor desiredFloorNumber)
        {
            Semaphore.WaitOne();
            Passenger = a;
            Console.WriteLine($"Agent {Passenger.Name} entered the elevator");
            SelectFloor(currentFloorNumber);
        }

        public void SelectFloor(Floor currentFloorNumber)
        {
            Floor desiredFloorNumber = (Floor)rand.Next(1, 5);

            if (Passenger != null && Passenger.WantsToGoHome)
            {
                desiredFloorNumber = Floor.GroundFloor;
                Move(currentFloorNumber, desiredFloorNumber);
            }
            
            if (desiredFloorNumber == currentFloorNumber)
            {
                SelectFloor(currentFloorNumber);
            }
            else
            {
                if(Passenger != null)
                {
                    Console.WriteLine($"Agent {Passenger.Name} is going to {desiredFloorNumber}");
                }
                Move(currentFloorNumber, desiredFloorNumber);
            }
        }

        public void Move(Floor currentFloorNumber, Floor desiredFloorNumber)
        {
            Thread.Sleep(CalculateMoveTime(currentFloorNumber, desiredFloorNumber));
            ElevatorDoor.CurrentFloor = desiredFloorNumber;
            if (Passenger != null)
            {
                if (ElevatorDoor.CheckCredentials(Passenger.SecurityLevel, desiredFloorNumber))
                {
                    Leave(Passenger);
                }
                else
                {
                    SelectFloor(ElevatorDoor.CurrentFloor);
                }
            }
        }

        private static int CalculateMoveTime(Floor currentFloorNumber, Floor desiredFloorNumber)
        {
            byte greaterFloor = Math.Max((byte)currentFloorNumber, (byte)desiredFloorNumber);
            byte lesserFloor = Math.Min((byte)currentFloorNumber, (byte)desiredFloorNumber);

            return (greaterFloor - lesserFloor) * 1000;
        }

        public void Leave(AgentBase a)
        {
            Passenger = null;
            Semaphore.Release();
            Console.WriteLine($"Agent {a.Name} left the elevator.");
            a.DoSomeWorkAtCurrentFloor(ElevatorDoor.CurrentFloor);
        }
    }
}
