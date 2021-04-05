﻿using HamsterParadise.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI
{
    public class UserInterface
    {
        private static Random random = new Random();
        private bool isTimerStopped;
        private int totalDaysToSim = 3; // default-värden 3
        private int ticksPerSecond = 2; // default-värden 2
        private static UserPrint userPrint = new UserPrint();

        public void RunMainUI()
        {
            MainMenu();
        }
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(120, 0); Console.Write($"Days to simulate: {totalDaysToSim}");
                Console.SetCursorPosition(120, 1); Console.Write($"Ticks per second: {ticksPerSecond}");

                string title = @"Some big title I will add later

(Use the arrow keys to cycle through options and press enter to select an option.)";
                string[] options = new string[] { "Start Simulation", "Change Values (default are already set)",
                                                  "Look at specific simulation","Show Credits", "Exit the program" };
                
                UserMenu mainMenu = new UserMenu(title, options, 0, 3);
                int selectedIndex = mainMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        StartSimulation();
                        break;
                    case 1:
                        totalDaysToSim = DaysToSim();
                        ticksPerSecond = TicksPerSecond();
                        break;
                    case 2:
                        SimulationMenu();
                        break;
                    case 3:
                        ShowCredits();
                        break;
                    case 4:
                        Console.WriteLine("\n\nShutting the program off..");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void StartSimulation()
        {
            Console.WriteLine("\n\nStarting simulation..");
            Console.ReadKey();
            Console.Clear();

            CareHouseSimulation careHouseSimulation = new CareHouseSimulation(ticksPerSecond, totalDaysToSim);

            careHouseSimulation.SendTickInfo += userPrint.PrintTickInfo;
            careHouseSimulation.SendDayInfo += userPrint.PrintDayInfo;
            careHouseSimulation.SendSimulationSummary += userPrint.PrintSimulationSummary;
            careHouseSimulation.SendSimulationSummary += StopControlOfTimer;

            ControlTimer(careHouseSimulation);

            careHouseSimulation.SendTickInfo -= userPrint.PrintTickInfo;
            careHouseSimulation.SendDayInfo -= userPrint.PrintDayInfo;
            careHouseSimulation.SendSimulationSummary -= userPrint.PrintSimulationSummary;
            careHouseSimulation.SendSimulationSummary -= StopControlOfTimer;
        }
        private void ControlTimer(CareHouseSimulation careHouseSimulation)
        {
            isTimerStopped = false;
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    careHouseSimulation.ManipulateTimer();
                }
            } while (isTimerStopped != true);
        }
        public async void StopControlOfTimer(object sender, SimulationSummaryEventArgs e)
        {
            await Task.Run(() => { isTimerStopped = true; });
        }

        private int DaysToSim()
        {
            while (true)
            {
                Console.Clear();
                string title = @"Some big title I will add here later";
                // (Use the arrow keys to cycle through options and press enter to select an option.)
                string[] options = new string[] { "  Default (3)  ", "      (1)      ", "      (2)      ",
                                                "      (3)      ", "      (4)      ", "      (5)      ", "      (6)      ",
                                                "      (7)      ", "      (8)      ", "      (9)      ", "Random (10->30)" };

                UserMenu daysToSimMenu = new UserMenu(title, options, 20, 40);
                int selectedIndex = daysToSimMenu.Run();

                switch (selectedIndex)
                {
                    case 0: case 3:
                        return 3;
                    case 1: case 2: case 4: case 5: case 6: case 7: case 8: case 9:
                        return selectedIndex;
                    case 10:
                        return random.Next(10, 31);
                }
            }
        }
        private int TicksPerSecond()
        {
            while (true)
            {
                Console.Clear();
                string title = @"Some big title I will add here later";
                // (Use the arrow keys to cycle through options and press enter to select an option.)
                string[] options = new string[] { "  Default (2)  ", "      (1)      ", "      (2)      ",
                                                "      (3)      ", "      (4)      ", "      (5)      " };

                UserMenu ticksPerSecondMenu = new UserMenu(title, options, 20, 40);
                int selectedIndex = ticksPerSecondMenu.Run();

                switch (selectedIndex)
                {
                    case 0: case 2:
                        return 2;
                    case 1: case 3: case 4: case 5:
                        return selectedIndex;
                }
            }
        }

        private void ShowCredits()
        {
            
        }
        private void SimulationMenu()
        {

        }
    }
}
