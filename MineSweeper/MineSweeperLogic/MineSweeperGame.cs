using System;
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
            if (positions[PosX, PosY].IsOpen == true || positions[PosX, PosY].IsFlagged == true)
            {
                positions[PosX, PosY].IsFlagged = false;
            }
            else if (positions[PosX, PosY].IsOpen == false)
            {
                positions[PosX, PosY].IsFlagged = true;
            }
        }

        public void ClickCoordinate()
        {
            if (positions[PosX,PosY].IsOpen == false && positions[PosX,PosY].HasMine == false && positions[PosX, PosY].IsFlagged == false)
            {
                FloodFill(PosX, PosY);
            }
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
                

                int ranX = Bus.Next(SizeX);
                int ranY = Bus.Next(SizeY);

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
                            positions[x + 1, y + 1].NrOfNeighbours += 1;
                        }
                        if (x + 1 <= SizeX - 1 && y - 1 >= 0)
                        {
                            positions[x + 1, y - 1].NrOfNeighbours += 1;
                        }
                        if (x - 1 >= 0 && y + 1 <= SizeY - 1)
                        {
                            positions[x - 1, y + 1].NrOfNeighbours += 1;
                        }
                        if (x - 1 >= 0 && y - 1 >= 0)
                        {
                            positions[x - 1, y - 1].NrOfNeighbours += 1;
                        }
                        if (y + 1 <= SizeY - 1)
                        {
                            positions[x, y + 1].NrOfNeighbours += 1;
                        }
                        if (y - 1 >= 0)
                        {
                            positions[x, y - 1].NrOfNeighbours += 1;
                        }
                        if (x + 1 <= SizeX - 1)
                        {
                            positions[x + 1, y].NrOfNeighbours += 1;
                        }
                        if (x - 1 >= 0)
                        {
                            positions[x - 1, y].NrOfNeighbours += 1;
                        }
                    }

                }
            }

        }

        public void DrawBoard()
        {
            {
                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {

                        if (positions[x, y].IsOpen)
                        {
                            if (positions[x, y].HasMine)
                            {
                                if (x == PosX && y == PosY)
                                {
                                    Bus.Write("X ", ConsoleColor.DarkCyan);
                                }
                                else
                                {
                                    Bus.Write("X ");
                                }
                            }
                            else
                            {
                                if (positions[x, y].NrOfNeighbours > 0 && x == PosX && y == PosY)
                                {

                                    Bus.Write(positions[x, y].NrOfNeighbours + " ", ConsoleColor.DarkCyan);
                                }
                                else if (positions[x, y].NrOfNeighbours > 0)
                                {
                                    Bus.Write(positions[x, y].NrOfNeighbours + " ");
                                }
                                else
                                {
                                    if (x == PosX && y == PosY)
                                    {
                                        Bus.Write(". ", ConsoleColor.DarkCyan);
                                    }
                                    else
                                    {
                                        Bus.Write(". ");
                                    }
                                }
                            }
                        }
                        else if (positions[x, y].IsFlagged)
                        {
                            if (x == PosX && y == PosY)
                            {
                                Bus.Write("! ", ConsoleColor.DarkCyan);
                            }
                            else
                            {
                                Bus.Write("! ");
                            }
                        }
                        if (x == PosX && y == PosY && positions[x, y].IsOpen == false && positions[x, y].IsFlagged == false)
                        {
                            Bus.Write("? ", ConsoleColor.DarkCyan);
                        }
                        else if (positions[x, y].IsOpen == false && positions[x, y].IsFlagged == false)
                        {
                            Bus.Write("? ");
                        }
                    }
                    Console.WriteLine();
                }
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

        private void FloodFill(int x, int y)
        {
            //perform bounds checking X
            if ((x >= SizeX) || (x < 0))
                return; //outside of bounds

            //perform bounds checking Y
            if ((y >= SizeY) || (y < 0))
                return; //ouside of bounds

            //check to see if the node is the target color
            if (positions[x, y].IsOpen)
                return; //return and do nothing
            else
            {
                positions[x, y].IsOpen = true;

                //recurse
                //try to fill one step to the right
                FloodFill(x + 1, y);
                //try to fill one step to the left
                FloodFill(x - 1, y);
                //try to fill one step to the north
                FloodFill(x, y - 1);
                //try to fill one step to the south
                FloodFill(x, y + 1);

                //exit method
                return;
            }
        }
            #endregion
        }

}

