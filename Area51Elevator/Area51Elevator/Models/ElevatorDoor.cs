using System;
using System.Collections.Generic;
using System.Text;

namespace Area51Elevator
{
    public sealed class ElevatorDoor
    {
        public Floor CurrentFloor { get; set; }

        public ElevatorDoor(Floor initialFloor)
        {
            CurrentFloor = initialFloor;
        }

        public bool CheckCredentials(SecurityLevel sl, Floor floorToAccess)
        {
            switch (sl)
            {
                case SecurityLevel.Confidential:
                    if(floorToAccess == Floor.GroundFloor)
                    {
                        Console.WriteLine("Access Granted");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Access Denied");
                        return false;
                    }
                case SecurityLevel.Secret:
                    if (floorToAccess == Floor.GroundFloor || floorToAccess == Floor.NuclearWeaponsFloor)
                    {
                        Console.WriteLine("Access Granted");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Access Denied");
                        return false;
                    }
                case SecurityLevel.TopSecret:
                    Console.WriteLine("Access Granted");
                    return true;
                default:
                    Console.WriteLine("Invalid credentials!");
                    return false;
            }
        }
    }
}
