using System;


namespace YandexTaxiTests
{
    public static class MatrixSpiralFilling
    {        
        /// <summary>
        /// Composed according moving directions interleaving. We are going to fill matrix in starting moving up, then left, down, right and up again.
        /// </summary>
        enum Direction
        {
            Up = 1, Left, Down, Right = 4
        }


        /// <summary>
        /// Advances the coordinates.
        /// </summary>
        /// <param name="x">Matrix column zero-based position</param>
        /// <param name="y">Matrix row zero-based position</param>
        /// <param name="dir">Where to move to</param>
        static void MakeStep (ref int x, ref int y, Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    y--; break;
                case Direction.Left:
                    x--; break;
                case Direction.Right:
                    x++; break;
                default: //down
                    y++; break;
            }
        }

        /// <summary>
        /// Changes movement according our rules Up-->Left-->Down-->Right and recalculates next cell coordinates.
        /// It is supposed, that we already made a step towards current direction, but found out, that we needed to step in a different direction.
        /// So, it deducts 1 from current direction and adds 1 towards new direction. For instance, if we are moving left we already added -1 to x,
        /// so we need to add 1 to x and to move down. For moving down we need to add 1 to y.
        /// </summary>
        /// <param name="x">Matrix column zero-based position</param>
        /// <param name="y">Matrix row zero-based position</param>
        /// <param name="dir">Where to move to</param>
        static void ChangeDirection (ref int x, ref int y, ref Direction dir)
        {
            if (dir == Direction.Left)
            {
                x++; y++;
                dir = Direction.Down;
            }
            else if (dir == Direction.Right)
            {
                x--; y--;
                dir = Direction.Up;
            }
            else if (dir == Direction.Up)
            {
                y++; x--;
                dir = Direction.Left;
            }
            else if (dir == Direction.Down)
            {
                y--; x++;
                dir = Direction.Right;
            }
        }

        /// <summary>
        /// Valiadtes whether (x, y) are valid cell coordinates for the matrix of size N. 
        /// Does not validate all the bounds, instead, it takes in account moving direction and valiadates only one border condition, which may be met on this circumstance.
        /// </summary>
        /// <param name="N">Matrix size</param>
        /// <param name="x">Matrix column zero-based position</param>
        /// <param name="y">Matrix row zero-based position</param>
        /// <param name="dir">Where to move to</param>
        /// <returns>true - if x, y are valid cell coordinates inside on the matrix</returns>
        static bool InBounds (int N, int x, int y, Direction dir)
        {
            return !(dir == Direction.Left && x < 0 ||
                     dir == Direction.Right && x >= N ||
                     dir == Direction.Up && y < 0 ||
                     dir == Direction.Down && y >= N);
        }

        /// <summary>
        /// Fills the matrix in using whole numbers strating from 1. Moves from the matrix center in spiral direction up-->left-->down-->right.
        /// </summary>
        /// <param name="N">Matrix size.</param>
        /// <returns>Filled in matrix.</returns>
        public static int[,] FillMatrix (int N)
        {
            if (N < 1)
                throw new Exception($"Invalid matrix size {N}. Should be 1 or greater.");
            if (N == 1)
                return new int[1, 1] { { 1 } };

            var m = new int[N, N];

            var active = true; //Used for stopping. We are going to stop when we hit any non-zero cell
            var step = 1; //Number of steps in one direction. When step becomes zero we change the direction
            var dir = Direction.Up; //Movement direction
            var currentCount = 1; //A counter to keep current number to put into a cell

            int centerX = N / 2, centerY = N / 2;            
            m[centerY, centerX] = 1; //Filling in the centeral cell. We do it manually to align the step, as it needs step = 2, but next movement needs step = 1

            for (int x = centerX, y = centerY - 1, sideCount = 1/*passed edges number*/; active; ++sideCount)
            {
                var startDir = dir;

                for (int i = step; i > 0; --i)
                {
                    //Console.WriteLine($"{x} : {y}, {step}, {dir} = {currentCount + 1}, s={step}");
                    if (m[y, x] != 0)
                    {
                        active = false; //we heat a filled in cell, so we stop as there are no any cells left
                        break;
                    }
                    m[y, x] = ++currentCount;

                    MatrixSpiralFilling.MakeStep(ref x, ref y, dir);
                    if (!MatrixSpiralFilling.InBounds(N, x, y, dir)) //if we are out of the matrix we have to change the direction
                        MatrixSpiralFilling.ChangeDirection(ref x, ref y, ref dir);
                }

                //Each time, when we haven't reached the border, we need to change direction too
                if (startDir == dir)                
                    MatrixSpiralFilling.ChangeDirection(ref x, ref y, ref dir);                

                //We need to increase the movement size on each even number of passed edges
                if (sideCount % 2 == 0)
                    step++;
            }

            return m;
        }

        /// <summary>
        /// A helper to print matrix out on the console.
        /// </summary>
        /// <param name="m">A matrix</param>
        /// <param name="pad">Cell size (the number of characters). Use negative values to align numbers left.</param>
        public static void PrintMatrix(int[,] m, int pad)
        {
            var pattern = $"{{0, {pad}}} ";

            for (int i = 0; i < m.GetLength(0); ++i)
            {
                for (int j = 0; j < m.GetLength(0); ++j)
                    Console.Write(pattern, m[i, j]);
                Console.WriteLine();
            }
        }
    }    
}
