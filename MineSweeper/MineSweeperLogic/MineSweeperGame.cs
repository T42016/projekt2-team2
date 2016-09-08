﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperLogic
{
    public class MineSweeperGame
    {
        private int posX;
        private int posY;
        private int sizeX;
        private int sizeY;
        private int numberOfMines;
        private GameState state;
        private IServiceBus bus;
        
        public MineSweeperGame(int sizeX, int sizeY, int numberOfMines, IServiceBus bus)
        {
            PosX = posX;
            PosY = posY;
            SizeX = sizeX;
            SizeY = sizeY;
            NumberOfMines = numberOfMines;
            State = state;
            Bus = bus;
            positions = new PositionInfo[SizeX, SizeY];

            ResetBoard();
        }

        private PositionInfo[,] positions;
        public ConsoleColor DarkCyan { get; set; }
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        public GameState State { get; private set; }
        public IServiceBus Bus { get; }

        public PositionInfo GetCoordinate(int x, int y)
        {
            return positions[x, y];
        }

        public void FlagCoordinate()
        {
            if (positions[PosX,PosY].IsOpen == true || positions[PosX,PosY].IsFlagged == true)
            {
                positions[PosX, PosY].IsFlagged = false;
            }
            else if (positions[PosX,PosY].IsOpen == false)
            {
                positions[PosX, PosY].IsFlagged = true;
            }
        }

        public void ClickCoordinate()
        {
            positions[PosX, PosY] = new PositionInfo();
            positions[PosX, PosY].IsOpen = true;
        }

        public void ResetBoard()
        {
            State = GameState.Playing;

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    positions[x, y] = new PositionInfo();
                    positions[x, y].IsOpen = false;
                    positions[x, y].HasMine = false;
                    positions[x, y].IsFlagged = false;
                    positions[x, y].NrOfNeighbours = 0;
                    positions[x, y].X = x;
                    positions[x, y].Y = y;

                }
            }

            for (int i = 0; i < NumberOfMines; i++)
            {
                Random Ran = new Random();

                int ranX = Ran.Next(0, SizeX);
                int ranY = Ran.Next(0, SizeY);

                if (positions[ranX, ranY].HasMine == false)
                {
                    positions[ranX, ranY].HasMine = true;
                }
                else
                {
                    i--;
                }
            }

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    if (positions[x, y].HasMine)
                    {
                        if (x + 1 <= SizeX - 1 && y + 1 <= SizeY - 1)
                        {
                            positions[x + 1, y + 1].NrOfNeighbours +=1;
                        }
                        if (x + 1 <= SizeX - 1 && y - 1 >= 0)
                        {
                            positions[x + 1, y - 1].NrOfNeighbours +=1;
                        }
                        if (x - 1 >= 0 && y + 1 <= SizeY - 1)
                        {
                            positions[x - 1, y + 1].NrOfNeighbours +=1;
                        }
                        if (x - 1 >= 0 && y - 1 >= 0)
                        {
                            positions[x - 1, y - 1].NrOfNeighbours +=1;
                        }
                        if (y + 1 <= SizeY - 1)
                        {
                            positions[x, y + 1].NrOfNeighbours +=1;
                        }
                        if (y - 1 >= 0)
                        {
                            positions[x, y - 1].NrOfNeighbours +=1;
                        }
                        if (x + 1 <= SizeX - 1)
                        {
                            positions[x + 1, y].NrOfNeighbours +=1;
                        }
                        if (x - 1 >= 0)
                        {
                            positions[x - 1, y].NrOfNeighbours +=1;
                        }
                    }
                    
                }
            }

        }

        public void DrawBoard()
        {
            positions[PosX, PosY] = new PositionInfo();
            {
                for (int x = 0; x < SizeX; x++)
                {
                    if (positions[x,y].IsOpen)
                    {
                        if (positions[x,y].HasMine)
                        {
                            Bus.Write("X ");
                        }
                        else
                        {
                            if (positions[x, y].NrOfNeighbours > 0)
                            {
                                if (positions[x, y].NrOfNeighbours == 1)
                                {
                                    Bus.Write("1 ");
                                }
                                if (positions[x, y].NrOfNeighbours == 2)
                                {
                                    Bus.Write("2 ");
                                }
                                if (positions[x, y].NrOfNeighbours == 3)
                                {
                                    Bus.Write("3 ");
                                }
                                if (positions[x, y].NrOfNeighbours == 4)
                                {
                                    Bus.Write("4 ");
                                }
                                if (positions[x, y].NrOfNeighbours == 5)
                                {
                                    Bus.Write("5 ");
                                }
                                if (positions[x, y].NrOfNeighbours == 6)
                                {
                                    Bus.Write("6 ");
                                }
                                if (positions[x, y].NrOfNeighbours == 7)
                                {
                                    Bus.Write("7 ");
                                }
                                if (positions[x, y].NrOfNeighbours == 8)
                                {
                                    Bus.Write("8 ");
                                }
                            }
                            else
                            {
                                Bus.Write(". ");
                            }
                        }
                    }
                    else if(positions[x,y].IsFlagged)
                    {
                        Bus.Write("! ");
                    }
                    
                    if (x == PosX && y == PosY)
                    {
                        Bus.Write("? ", ConsoleColor.DarkCyan);
                    }
                    else if (positions[PosX, PosY].IsOpen.Equals(true))
                    {
                        Bus.Write("! ");
                    }
                    else
                    {
                        Bus.Write("? ");
                    }

                }
                Console.WriteLine();
                
            }
          

        }

        #region MoveCursor Methods

        public void MoveCursorUp()
        {
            if (PosY > 0)
            {
                PosY -= 1;
            }
        }

        public void MoveCursorDown()
        {
            if (PosY < SizeY - 1)
            {
                PosY += 1;
            }
        }

        public void MoveCursorLeft()
        {
            if (PosX > 0)
            {
                PosX -= 1;
            }
        }

        public void MoveCursorRight()
        {
            if (PosX < SizeX - 1)
            {
                PosX += 1;
            }
        }

        #endregion

    }
}
