using System;
using System.Collections.Generic;
using System.Linq;

namespace FullSudoku
{
    

    internal class Game
    {
        // Private field
        private string dataIn;
        private int gameSize = 9;               // size of the game chart   
        private GameChart initChart;           // Resetting my istance of the game        
        private int candidateNr = 0;
        private int hitsNr = 0;                   
        private List<int> allNumbers = new List<int>();        // Contains all numbers in row,col,block. Needs to be initialize for each GAME chart! 
        private List<int> hitsCollection = new List<int>(); // Contains in each element an array of HIT numbers for a specific cell  

        public Stack<Game> backupList = new Stack<Game>();
        

        // Constructor for Game MUST BE PUBLIC with 1 parameter
        // used in case of object direct initialization
        // Ex. Game ob1 = new Game("abcd")

        public Game(string inputData)
        {
            // Aquiring the Sudoku initial layout
            dataIn = inputData;              // Contains the String in input
        }

       
        public Game()
        {

        }

        public Game SaveCurrentGame()
        {
            // dest is the current game
            // create a new istance as backup

            var dest = new Game
            {
                hitsCollection = new List<int>(hitsCollection),  // backup hits collection
                allNumbers = new List<int>(allNumbers), // Backup numbers around the cell (i,j)
                initChart = new GameChart(initChart),  // Questo non ha alcun senso in questo contesto!! ATTENZIONE
                candidateNr = candidateNr,
                hitsNr = hitsNr
            };
            return dest;
        }


        public void RetrievePreviousGame(Game backup)
        {
            hitsCollection = backup.hitsCollection.ToList();
            allNumbers = backup.allNumbers.ToList();
            initChart = backup.initChart;
            candidateNr = backup.candidateNr;
            hitsNr = backup.hitsNr;
        }


        public void Solve()
        {
            Console.Clear();
            Initialize();
            Console.WriteLine("--- Starting Sudoku ---\n");
            Show(initChart);
            Console.WriteLine();
            if (Play())
            {
                Console.WriteLine("--- Solved Sudoku ---\n");
                Show(initChart);
                Show2(initChart);
            }
            else
            {
                Console.WriteLine("--- End Sudoku ---\n");

                Show(initChart);
                Console.WriteLine("Impossible solution");

            }

        }


        private void Initialize()
        {
            // Define the size of the Game chart
            // and fill the data structure with input data

            initChart = new GameChart(gameSize, dataIn);
                    
        }

        private void Show2(GameChart c)
        {
            Console.WriteLine("Output: "+ initChart.ConvertToString());


        }


        private void Show(GameChart initChart)
        {
            // I must show per columns and NOT per row as when I scan an array
            Console.WriteLine("----------------------");
            for (int i = 0; i < gameSize; i++)
            {

                for (int j = 0; j < gameSize; j++)
                {
                    Console.Write(initChart.GetElement(i, j) + " ");
                    if (j == 2 || j == 5)
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine();
                if (i == 2 || i == 5 || i == 8)
                {
                    Console.WriteLine("----------------------");
                }
            }

        }


        // This Method will scan the matrix to detect the first 
        // empty cell which can be solved without guessing
        
        private bool Play()
        {
            // Reset the new Game
            bool readyForGuessing = false;
            bool impossible = false;
            bool continueSolve = true;
             while (continueSolve)         // A new Matrix scan is requested 
            {

                // Scan the chart as long as there are '0' and I have ONLY one possibility to choose
                while (initChart.StillCellsToFill() && continueSolve)
                {
                    continueSolve = false;

                    for (int i = 0; i < gameSize; i++)
                    {
                        for (int j = 0; j < gameSize; j++)
                        {
                            // I must initialize the collection of all sorrounding numbers 
                            //  each time I examine a new cell (i,j) in the chart game

                            allNumbers.Clear();

                            // I must initialize the collection of the potential hits
                            // each time I examine a new cell

                            hitsCollection.Clear();

                            if (initChart.GetElement(i, j) == 0)
                            {
                                // I found an empty cell and I can collect the numbers 
                                // of column, row and block in List allNumbers

                                allNumbers = initChart.getAllNumbers(i, j);

                                // Now I check if all the numbers collected in the List allNumbers
                                // are missing ONLY one value from 1 to 9.
                                
                                hitsNr = 0;     // Nr of possible alternatives for a specific cell (i,j) of value 0
                                for (int k = 1; k <= gameSize; k++)
                                {
                                    if (!allNumbers.Contains(k)) // True if 'k' is NOT in the Collected Numbers
                                    {
                                        hitsCollection.Add(k); // Update the Collection of Hits/guesses for the ROW
                                        hitsNr++;              // if current value is not in the list, increase the counter and go on
                                        candidateNr = k;       // candidateNr is the potential value to fill the position in the chart
                                    }
                                }
                                // Here: hitsCollection contains ALL the alternatives to fill the 0 and hitsNr = how many alternatives I have!

                                // I found the FIRST candidate in the current ROW (i) to fill position (i,j) in myChart
                                // UPDATING THE CHART HERE!!
                                if (hitsNr == 1)
                                {
                                    readyForGuessing = false;
                                    continueSolve = true;         // Try a new scan of the chart after update
                                    initChart.SetElement(i, j, candidateNr);  // Update chart and exit

                                    // Console.WriteLine($"Found element with value {candidateNr} to fit in position [{i},{j}]");
                                    // Console.ReadKey();
                                   //  Console.Clear();
                                    // Show(initChart);
                                }
                                else if (hitsNr > 0) // More than ONE candidate in the current ROW (i)
                                {
                                    if (readyForGuessing == true) // It means that I am still in guessing mode
                                    {
                                        //Console.ReadKey();
                                        //Console.Clear();
                                        //Show(initChart);
                                        //Console.WriteLine($"Found {hitsNr} values to fit in position [{i},{j}]");

                                        for (int m = 0; m < hitsNr; m++)
                                        {
                                            // trying set the element in the chart and the Solve
                                            // this must survive along MANY nested calls to Play()
                                            // Saving the current matrix before changing the 0 with one "guess"
                                            // in a List internal to initChart object
                                            // Before starting solving the new chart


                                            initChart.SaveChartInList(initChart);

                                            continueSolve = true;

                                            // ************************************
                                            Game current = SaveCurrentGame();
                                            // Add now the current game in a global list!
                                            backupList.Push(current);

                                            //*******************************************


                                            // Here I update the chart we want to try to solve with "one guess"
                                            //  Console.ReadKey();

                                            initChart.SetElement(i, j, hitsCollection[m]);  // Update chart and exit

                                            //Console.Write("[");
                                            //foreach (int k in hitsCollection)
                                            //{
                                            //    Console.Write(k + ",");
                                            //}
                                            //Console.WriteLine("]");
                                            //Console.WriteLine($"Trying {hitsCollection[m]} in position ({i},{j})");
                                            //Console.WriteLine("NR. Alternatives: " + hitsNr);
                                            //Show(initChart);

                                            if (Play())           // shelf call to Play. True if I got it solved with current choice of value in hitsCollections
                                            {
                                                // Console.WriteLine("PLAY IS TrUE !!!!! Game Solved");
                                                continueSolve = false;
                                                //  Console.ReadKey();
                                                return true;     // Return if following tries are solved                                                
                                            }
                                            // Console.WriteLine("I have tried " + hitsCollection[m] + ". I will try with next value");
                                        }

                                        // Í checked ALL numbers in one position (x,y) and the game was not solved with any of them
                                        // I need to return to previous state and try other numbers.
                                        if (backupList.Count != 0)
                                        {
                                            RetrievePreviousGame(backupList.Pop());
                                            return false;
                                        }
                                    }   // Still in guessing mode  (readyGuessing == true ?)
                                }
                                else // hitsNr == 0 I have not possible solutions for the cell! Need to go back to previous level!
                                {
                                    impossible = true;
                                    if (backupList.Count != 0)
                                    {
                                        RetrievePreviousGame(backupList.Pop());
                                        return false;
                                    }

                                }
                            }
                            // else if myChart element is NOT empty I continue my search in the matrix                        
                        }
                    }
                    // I get here when I complete an entire scan of the Matrix

                } //  If get here if ContinueSolve = false OR no more 0s in matrix

                // This is always true if I DON´T HAVE any single solutions in the matrix and I get stuck!
                readyForGuessing = initChart.StillCellsToFill() && !impossible;  // readyForGuessing =...

                if (readyForGuessing)
                {
                    // Console.WriteLine("I FOUND and solved all SINGLE CHOICES. NOW I can check the MULTICHOICES");
                    continueSolve = true;  // Set TRUE if I want to try guesses
                    // Console.ReadKey();
                }
                else
                {
                    continueSolve = false;
                }
            } // sudoku solved with the specific sequence of multichoices!


            return true;
        }
    }
}

