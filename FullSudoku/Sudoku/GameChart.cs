using System;
using System.Collections.Generic;

namespace FullSudoku
{
    class GameChart
    {
        // GameChart can implement ANY data structure
        // for the game without influencing the other 
        // parts of the code.
        // Incapsuation of the underying data structure

        // Fields
        private int size = 9;
        private string dataIn;
        public int[,] chart;
        
        // List of charts where I save the games when I get to the "guessing" 
        // fase
        static List<int[,]> chartList = new List<int[,]>();

        // For each Game I need a List for the numbers around the cell to solve 
        private List<int> cellNumbers = new List<int>();


        //
        // save the char in the List chartList
        //
        public void SaveChartInList(GameChart gchart)
        {
            // save the 
            chartList.Add(gchart.chart);
        }

        // Free the Backup List from on Chart
        //
        public void RemoveChartFromList(GameChart gchart)
        {
            chartList.Remove(gchart.chart);

        }

        // -----------
        // Constructors
        // -----------

        // Copy constructor
        // This is USED
        public GameChart(GameChart c)
        {
            size = c.size;
            dataIn = c.dataIn;
            chart = new int[size, size];
            Array.Copy(c.chart, chart, size*size);
            
        }


        public GameChart(int size, string dataIn)
        {
            this.size = size;
            this.dataIn = dataIn;

            // New istance of the chart
            chart = new int[size, size];


            // Filling the chart with data from initialization
            int row = 0;
            int col = 0;
            for (int i = 0; i < dataIn.Length; i++)
            {
                row = i / size;  // indexed by 0
                col = i % size;  // indexed by 0

                //NOTE dataIn[i] is converted in ASCII automatically by 
                // implicit (int) casting!
                chart[row, col] = dataIn[i] - '0';  // Offset from ASCI code '0'
            }

        }


        public string ConvertToString()
        {
            string msg ="";
            foreach (var item in chart)
            {
                msg += item.ToString();
                

            }
            return msg;
        }

        // To get access to a specific position in the chart, i need a Public method!!

        public int GetElement(int x, int y)
        {
            // If we try to go outside the boudaries then ..
            if (x < 0 || x >= size || y < 0 || y >= size) return 0;

            return chart[x, y]; // return the content in position (x,y) of the array
        }


        public void SetElement(int x, int y, int newValue)
        {
            chart[x, y] = newValue;
        }


        // This Method only check whether there are still 
        // '0' (i.e. cells to fill) in the chart
        public bool StillCellsToFill()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (chart[i, j] == 0)
                    {
                        return true; // Game is still incomplete
                    }
                }
            }
            return false; // Game completed
        }


        public List<int> getAllNumbers(int x, int y)
        {
            GetNumInColumn(y);
            GetNumInRow(x);
            GetNumInBlock(x, y);
            return cellNumbers;
        }


        private void GetNumInColumn(int y)
        {
            int value;
            for (int i = 0; i < size; i++)
            {
                value = GetElement(i, y);
                if (value != 0)
                {
                    // numbers.Add(value); // Add to the list numbers in same column

                    cellNumbers.Add(value);
                }
            }
        }


        private void GetNumInRow(int x)
        {
            int value;
            for (int j = 0; j < size; j++)
            {
                value = GetElement(x, j);
                if (value != 0)
                {
                    // numbers.Add(value); // Add to the list numbers in same row

                    cellNumbers.Add(value);
                }
            }

        }


        private void GetNumInBlock(int x, int y)
        {
            // I can virtualize myChart 9x9 in a Matrix 3x3 made of blocks!
            // where each element is a 3x3 Block. For an element (x,y) in myChart
            // I locate the Block with coordinate (x/3, y/3) in the virtual matrix
            // Detect the position of the block 
            // in the virtual matrix 3x3 blocks!

            int virX = x / 3; // it will return 0, 1, 2
            int virY = y / 3; // it will return 0, 1, 2
            ScanBlock(virX, virY);
        }


        private void ScanBlock(int virX, int virY)
        {
            // Find Top Left cell (x,y) real coordinate inside the 3x3 block
            int cornerX = 3 * virX;
            int cornerY = 3 * virY;
            int sizeBlk = size / 3;
            int value;

            for (int i = 0; i < sizeBlk; i++)
            {
                for (int j = 0; j < sizeBlk; j++)
                {
                    value = GetElement(i + cornerX, j + cornerY);
                    if (value != 0)
                    {
                        // blkNumbers.Add(value); // Add to the list numbers in same column

                        cellNumbers.Add(value);
                    }
                }
            }
        }



    }
}