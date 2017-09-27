using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class SudokuSolver
    {
        static int[,] numbers = new int[9,9];
        static int xStartPos = (Console.WindowWidth - 25) / 2, yStartPos = 5;

        static Random rndNr = new Random();

        static void DrawGameTable()
        {

            string[] tableBorder = { "-------------------------",
                                     "|       |       |       |",
                                     "|       |       |       |",
                                     "|       |       |       |",
                                     "-------------------------"};

            //draw grid
            for (int i = 0; i < tableBorder.Length*2; i+=4)
            {
                for (int i2 = 0; i2 < 5; i2++)
                {
                    Console.SetCursorPosition(xStartPos, yStartPos + i+i2);
                    Console.Write(tableBorder[i2]);
                }
            }
        }

        static void PrintNumberArray()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (numbers[x, y] > 0)
                    {
                        Console.SetCursorPosition(xStartPos + 2 + x * 2 + (2 * (x / 3)), yStartPos + 1 + y + 1 * (y / 3));
                        Console.Write(numbers[x, y]);
                    }
                }
            }
        }

        static void PrintNumberArray(int[,] originalInput)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (originalInput[x, y] > 0)
                    {
                        Console.SetCursorPosition(xStartPos + 2 + x * 2 + (2 * (x / 3)), yStartPos + 1 + y + 1 * (y / 3));
                        Console.Write(originalInput[x, y]);
                    }
                }
            }
        }


        //convert inputstring to number array
        static void ConvertString(string inputString)
        {
            int tryParse;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if(int.TryParse(inputString[9 * y + x].ToString(),out tryParse)) numbers[x, y] = tryParse;
                }
            }
        }

        //convert number array to string
        static string ConvertToString()
        {
            string outputString="";
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    outputString += numbers[x, y].ToString();
                }
            }
            return outputString;
        }



        //check if any number is allowed at xpos,ypos and return that number, else return 0
        static int canPlaceNumber(int[,] arrayToCheck, int xPos, int yPos,int nrToCheck)
        {
            bool[] values = new bool[9];
            int freeNumbers = 9;

            for (int i = 0; i < values.Length; i++) values[i] = false;
            //check x-axis for free number
            for (int checkXPos = 0; checkXPos < 9; checkXPos++)
            {
                if (checkXPos != xPos && arrayToCheck[checkXPos, yPos] > 0)
                {
                    values[arrayToCheck[checkXPos, yPos] - 1] = true;
                }
            }
            //check y-axis for free number
            for (int checkYPos = 0; checkYPos < 9; checkYPos++)
            {
                if (checkYPos != yPos && arrayToCheck[xPos, checkYPos] > 0)
                {
                    values[arrayToCheck[xPos, checkYPos] - 1] = true;
                }
            }
            //check square for free number
            for (int boxX = (xPos / 3) * 3; boxX < (xPos / 3) * 3 + 3; boxX++)
            {
                for (int boxY = (yPos / 3) * 3; boxY < (yPos / 3) * 3 + 3; boxY++)
                {
                    if (arrayToCheck[boxX, boxY] > 0)
                    //if (boxX != xPos && boxY != yPos && arrayToCheck[boxX, boxY] > 0)
                    {
                        values[arrayToCheck[boxX, boxY] - 1] = true;
                    }
                }

            }
            //if method is used to check if number is allowed to be placed at position, return -1 if allowed
            if (nrToCheck != -1)
            {
                if (!values[nrToCheck - 1]) return -1;
            }

            //check if new valid number to assign at xpos,ypos has been found
            for (int checkArray = 0; checkArray < values.Length; checkArray++)
            {
                if (values[checkArray]) freeNumbers--;
            }
            if (freeNumbers == 1)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (values[i] == false) return i + 1;
                }
            }
            return 0;
        }



        //checks if sudoku is solved
        static bool IsSolved(int[,] sudokuArray)
        {
            bool[] uniqueNumbersCol = new bool[9];
            bool[] uniqueNumbersRow = new bool[9];
            int chkSumCol= 0, chkSumRow=0;

            //check col & row
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (sudokuArray[x, y] == 0 || sudokuArray[y, x] == 0) return false;     //if any cell equals to 0, s is not solved
                    chkSumCol += sudokuArray[x, y];                                     //add upp col
                    chkSumRow += sudokuArray[y, x];                                     //add up row
                    uniqueNumbersCol[sudokuArray[x, y] - 1] = true;                     //set to true to check if all numbers gets used
                    uniqueNumbersRow[sudokuArray[y, x] - 1] = true;                     // ~-~
                }
                if (chkSumCol != 45) return false;
                if (chkSumRow != 45) return false;
                if (uniqueNumbersCol.Contains(false)) return false;
                if (uniqueNumbersRow.Contains(false)) return false;
                for (int i = 0; i < 9; i++)
                {
                    uniqueNumbersCol[i] = false;
                    uniqueNumbersRow[i] = false;
                }
                chkSumCol = 0;
                chkSumRow = 0;
            }
            
            //check boxes
            for (int boxY = 0; boxY < 3; boxY++)
            {
                for (int boxX = 0; boxX < 3; boxX++)
                {
                    for (int yPos = boxY * 3; yPos < boxY * 3 + 3; yPos++)
                    {
                        for (int xPos = boxX * 3; xPos < boxX * 3 + 3; xPos++)
                        {
                            uniqueNumbersCol[sudokuArray[xPos,yPos]-1] = true;
                            chkSumCol += sudokuArray[xPos, yPos];
                        }
                    }
                    if (chkSumCol != 45) return false;
                    if (uniqueNumbersCol.Contains(false)) return false;
                    chkSumCol = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        uniqueNumbersCol[i] = false;
                    }
                }
            }

            //if program reach this point, sudoku is solved
            return true;
        }



        //random guessing solver
        static int[,] RndGuesser(int digitsToGuess)
        {
            int[,] guessingSudoku = new int[9, 9];
            int amountOfGuesses, rndX, rndY, oneToNine;
            int i=0;

            amountOfGuesses = rndNr.Next(digitsToGuess) + 1;

            guessingSudoku = numbers.Clone() as int[,];

            while(i<= amountOfGuesses)
            {
                rndX = rndNr.Next(9);
                rndY = rndNr.Next(9);
                oneToNine = rndNr.Next(9)+1;
                if (guessingSudoku[rndX, rndY] == 0 && canPlaceNumber(guessingSudoku, rndX, rndY, oneToNine) == -1)
                {
                    guessingSudoku[rndX, rndY] = oneToNine;
                    i++;
                }
            }
            return guessingSudoku;
        }



        //solver
        static int[,] Solver(int[,] sudokuArray)
        {
            bool changeOccured;

            int tmpNumber,lastXPos=0, lastYPos=0;

            do
            {
                changeOccured = false;
                //cell by cell check - search for uniquely placeable numbers and place them
                for (int yPos = 0; yPos < 9; yPos++)
                {
                    for (int xPos = 0; xPos < 9; xPos++)
                    {
                        if (sudokuArray[xPos, yPos] == 0)
                        {
                            tmpNumber = canPlaceNumber(sudokuArray, xPos, yPos, -1);
                            if (tmpNumber > 0)
                            {
                                sudokuArray[xPos, yPos] = tmpNumber;
                                changeOccured = true;
                            }
                        }
                    }
                }


                //box by box search to find if any free number is placeable at only one position
                for (int boxY = 0; boxY < 3; boxY++)
                {
                    for (int boxX = 0; boxX < 3; boxX++)
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            tmpNumber = 0;
                            for (int yPos = boxY * 3; yPos < boxY * 3 + 3; yPos++)
                            {
                                for (int xPos = boxX * 3; xPos < boxX * 3 + 3; xPos++)
                                {
                                    if (sudokuArray[xPos, yPos] == 0)
                                    {
                                        if (canPlaceNumber(sudokuArray, xPos, yPos, i) == -1)
                                        {
                                            tmpNumber++;
                                            lastXPos = xPos;
                                            lastYPos = yPos;
                                        }
                                    }
                                }
                            }
                            //if current number 'i' can only be placed at one position in box, do so
                            if (tmpNumber == 1)
                            {
                                sudokuArray[lastXPos, lastYPos] = i;
                                changeOccured = true;
                            }
                        }
                    }
                }
            } while (changeOccured);
            return sudokuArray;
        }



        static void DigitEditor()
        {
            const int optimalAmountOfDigits = 42;       //amount of digits sudoku table should have before trying to solve

            bool recentEdit = false;
            int cursorXPos = 0, cursorYPos = 0, iteration = 0, timer = 0, countUsedCells = 0;
            int[,] originalInput = new int[9, 9];
            int[,] tmpArray = new int[9,9];
            string sudokuInString = "";

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            ConsoleKeyInfo keyInput;


            do
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"Cursor XPos: {cursorXPos+1}");
                Console.WriteLine($"Cursor YPos: {cursorYPos+1}\n");
                Console.WriteLine("Keys:");
                Console.WriteLine("C: clear all digits");
                Console.WriteLine("0: removes digit");
                Console.WriteLine("1-9: sets digit ");
                Console.WriteLine("F3: enter sudoku string");
                Console.WriteLine("ENTER: try solving");
                Console.WriteLine("ESC: exit program");

                Console.SetCursorPosition(20, 1);
                Console.Write("Number at current position: ");
                if (numbers[cursorXPos, cursorYPos] > 0) Console.Write(numbers[cursorXPos, cursorYPos]);
                else Console.Write(" ");

                Console.SetCursorPosition(0, 22);
                Console.WriteLine("Output string:");
                Console.WriteLine(ConvertToString());

                DrawGameTable();

                if (iteration > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    PrintNumberArray();
                    Console.ResetColor();
                    PrintNumberArray(originalInput);
                }
                else PrintNumberArray();




                if (!recentEdit || numbers[cursorXPos,cursorYPos]==0)
                {
                    Console.SetCursorPosition(xStartPos + 2 + cursorXPos * 2 + (2 * (cursorXPos / 3)), yStartPos + 1 + cursorYPos + 1 * (cursorYPos / 3));
                    Console.Write("-");
                }


                keyInput = Console.ReadKey(true);
                recentEdit = false;

                if (keyInput.KeyChar > 47 && keyInput.KeyChar < 58)
                {
                    numbers[cursorXPos, cursorYPos] = int.Parse(keyInput.KeyChar.ToString());
                    recentEdit = true;
                }

                switch (keyInput.Key)
                {
                    //movement cases
                    case (ConsoleKey.UpArrow):
                        cursorYPos--;
                        if (cursorYPos < 0) cursorYPos = 8;
                        break;

                    case (ConsoleKey.DownArrow):
                        cursorYPos++;
                        if (cursorYPos > 8) cursorYPos = 0;
                        break;

                    case (ConsoleKey.LeftArrow):
                        cursorXPos--;
                        if (cursorXPos < 0) cursorXPos = 8;
                        break;

                    case (ConsoleKey.RightArrow):
                        cursorXPos++;
                        if (cursorXPos > 8) cursorXPos = 0;
                        break;

                    case (ConsoleKey.C):
                        iteration = 0;
                        for (int x = 0; x < 9; x++)
                        {
                            for (int y = 0; y < 9; y++)
                            {
                                numbers[x, y] = 0;
                            }
                        }
                        break;

                    case (ConsoleKey.F3):
                        do
                        {
                            Console.Clear();
                            Console.SetCursorPosition(2, 2);
                            Console.WriteLine("Enter string containing entire sudoku:");
                            sudokuInString = Console.ReadLine();
                        } while (!(sudokuInString.Length == 0 || sudokuInString.Length == 81));
                        if (sudokuInString.Length > 0) ConvertString(sudokuInString);
                        Console.Clear();
                    break;

                    case (ConsoleKey.Enter):
                        originalInput = numbers.Clone() as int[,];

                        iteration=1;
                        timer = 0;
                        Solver(numbers);
                        if (!IsSolved(numbers)) {
                            countUsedCells = 0;
                            for (int x = 0; x < 9; x++)
                            {
                                for (int y = 0; y < 9; y++)
                                {
                                    if (numbers[x, y] > 0) countUsedCells++;
                                }
                            }
                            if (countUsedCells > optimalAmountOfDigits) countUsedCells = optimalAmountOfDigits;
                            stopwatch.Start();
                            do
                            {
                                tmpArray = Solver(RndGuesser(optimalAmountOfDigits-countUsedCells)).Clone() as int[,];
                                iteration++;
                                if (stopwatch.ElapsedMilliseconds > 1000)
                                {
                                    stopwatch.Restart();
                                    timer++;
                                    Console.SetCursorPosition(60, 1);
                                    Console.Write("Elapsed time: {0}",timer);
                                    Console.SetCursorPosition(80, 1);
                                    Console.Write("Amount of tries: {0}", iteration);
                                }
                            } while (!IsSolved(tmpArray));
                            Console.SetCursorPosition(80, 1);
                            Console.Write("Amount of tries: {0}", iteration);
                            stopwatch.Stop();
                            stopwatch.Reset();
                            numbers = tmpArray.Clone() as int[,];
                        }
                        break;

                    default:
                        break;
                }



            } while (keyInput.Key != ConsoleKey.Escape);
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            DrawGameTable();
            DigitEditor();
        }
    }
}