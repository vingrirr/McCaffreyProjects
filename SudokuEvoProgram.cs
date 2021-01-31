using System;
using System.Collections.Generic;
namespace SudokuEvo
{
  class SudokuEvoProgram
  {
    static Random rnd = new Random(0);

    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin solving Sudoku using combinatorial evolution \n");
      Console.WriteLine("\nThe problem is: \n");
      int[][] problem = new int[9][];

      // August 2014 MSDN article problem -- difficult
      // solved using no = 200, me = 5000
      problem[0] = new int[] { 0, 0, 6, 2, 0, 0, 0, 8, 0 };
      problem[1] = new int[] { 0, 0, 8, 9, 7, 0, 0, 0, 0 };
      problem[2] = new int[] { 0, 0, 4, 8, 1, 0, 5, 0, 0 };

      problem[3] = new int[] { 0, 0, 0, 0, 6, 0, 0, 0, 2 };
      problem[4] = new int[] { 0, 7, 0, 0, 0, 0, 0, 3, 0 };
      problem[5] = new int[] { 6, 0, 0, 0, 5, 0, 0, 0, 0 };

      problem[6] = new int[] { 0, 0, 2, 0, 4, 7, 1, 0, 0 };
      problem[7] = new int[] { 0, 0, 3, 0, 2, 8, 4, 0, 0 };
      problem[8] = new int[] { 0, 5, 0, 0, 0, 1, 2, 0, 0 };

      //// http://ieeexplore.ieee.org/stamp/stamp.jsp?tp=&arnumber=5412260
      //// very difficult.
      //// solved using no = 200, me = 9000
      //problem[0] = new int[] { 0, 0, 0, 0, 7, 0, 0, 0, 0 };
      //problem[1] = new int[] { 0, 9, 0, 5, 0, 6, 0, 8, 0 };
      //problem[2] = new int[] { 0, 0, 8, 4, 0, 1, 2, 0, 0 };

      //problem[3] = new int[] { 0, 5, 9, 0, 0, 0, 8, 4, 0 };
      //problem[4] = new int[] { 7, 0, 0, 0, 0, 0, 0, 0, 6 };
      //problem[5] = new int[] { 0, 2, 3, 0, 0, 0, 5, 7, 0 };

      //problem[6] = new int[] { 0, 0, 5, 3, 0, 7, 4, 0, 0 };
      //problem[7] = new int[] { 0, 1, 0, 6, 0, 8, 0, 9, 0 };
      //problem[8] = new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0 };

      //// easy
      //// solved quickly with almost any reasonable no, me
      //problem[0] = new int[] { 0, 0, 7, 0, 0, 2, 9, 3, 0 };
      //problem[1] = new int[] { 0, 8, 1, 0, 0, 0, 0, 0, 5 };
      //problem[2] = new int[] { 9, 0, 4, 7, 0, 0, 1, 6, 0 };

      //problem[3] = new int[] { 0, 1, 0, 8, 0, 0, 0, 0, 6 };
      //problem[4] = new int[] { 8, 4, 6, 0, 0, 0, 5, 9, 2 };
      //problem[5] = new int[] { 5, 0, 0, 0, 0, 6, 0, 1, 0 };

      //problem[6] = new int[] { 0, 9, 2, 0, 0, 8, 3, 0, 1 };
      //problem[7] = new int[] { 4, 0, 0, 0, 0, 0, 6, 5, 0 };
      //problem[8] = new int[] { 0, 6, 5, 4, 0, 0, 2, 0, 0 };

      //// http://elmo.sbs.arizona.edu/sandiway/sudoku/examples.html
      //// EXTREMELY difficult.
      //// solved quickly using no = 100, me = 19,000
      //problem[0] = new int[] { 0, 0, 0, 6, 0, 0, 4, 0, 0 };
      //problem[1] = new int[] { 7, 0, 0, 0, 0, 3, 6, 0, 0 };
      //problem[2] = new int[] { 0, 0, 0, 0, 9, 1, 0, 8, 0 };

      //problem[3] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
      //problem[4] = new int[] { 0, 5, 0, 1, 8, 0, 0, 0, 3 };
      //problem[5] = new int[] { 0, 0, 0, 3, 0, 6, 0, 4, 5 };

      //problem[6] = new int[] { 0, 4, 0, 2, 0, 0, 0, 6, 0 };
      //problem[7] = new int[] { 9, 0, 3, 0, 0, 0, 0, 0, 0 };
      //problem[8] = new int[] { 0, 2, 0, 0, 0, 0, 1, 0, 0 };

      //// http://elmo.sbs.arizona.edu/sandiway/sudoku/examples.html
      //// most difficult problem found by Internet search.
      //// solved eventually using no = 100, me = 5,000.
      //// solution when seed = 577 (i.e., 577 attempts ~ 20 min.)
      //problem[0] = new int[] { 0, 2, 0, 0, 0, 0, 0, 0, 0 };
      //problem[1] = new int[] { 0, 0, 0, 6, 0, 0, 0, 0, 3 };
      //problem[2] = new int[] { 0, 7, 4, 0, 8, 0, 0, 0, 0 };

      //problem[3] = new int[] { 0, 0, 0, 0, 0, 3, 0, 0, 2 };
      //problem[4] = new int[] { 0, 8, 0, 0, 4, 0, 0, 1, 0 };
      //problem[5] = new int[] { 6, 0, 0, 5, 0, 0, 0, 0, 0 };

      //problem[6] = new int[] { 0, 0, 0, 0, 1, 0, 7, 8, 0 };
      //problem[7] = new int[] { 5, 0, 0, 0, 0, 9, 0, 0, 0 };
      //problem[8] = new int[] { 0, 0, 0, 0, 0, 0, 0, 4, 0 };

      DisplayMatrix(problem);

      int numOrganisms = 200;
      int maxEpochs = 5000;
      int maxRestarts = 40;
      Console.WriteLine("Setting numOrganisms = " + numOrganisms);
      Console.WriteLine("Setting maxEpochs    = " + maxEpochs);
      Console.WriteLine("Setting maxRestarts  = " + maxRestarts);

      int[][] soln = Solve(problem, numOrganisms, maxEpochs, maxRestarts);
      Console.WriteLine("\nBest solution found: \n");
      DisplayMatrix(soln);

      int err = Error(soln);
      if (err == 0)
        Console.WriteLine("Success \n");
      else
        Console.WriteLine("Did not find optimal solution \n");

      Console.WriteLine("End Sudoku using combinatorial evolution \n");

      Console.ReadLine();

    } // Main()

    public static int[][] Solve(int[][] problem, int numOrganisms, int maxEpochs, int maxRestarts)
    {
      // wrapper for SolveEvo()
      int err = int.MaxValue;
      int seed = 0;
      int[][] best = null;
      int attempt = 0;
      while (err != 0 && attempt < maxRestarts)
      {
        Console.WriteLine("\nseed = " + seed);
        rnd = new Random(seed);
        best = SolveEvo(problem, numOrganisms, maxEpochs);  // things, maxEpochs
        err = Error(best);
        ++seed;
        ++attempt;
      }

      return best;
    } // Solve

    public static void DisplayMatrix(int[][] matrix)
    {
      for (int r = 0; r < 9; ++r)
      {
        if (r == 3 || r == 6) Console.WriteLine("");
        for (int c = 0; c < 9; ++c)
        {
          if (c == 3 || c == 6) Console.Write(" ");
          if (matrix[r][c] == 0)
            Console.Write(" _");
          else
            Console.Write(" " + matrix[r][c]);
        }
        Console.WriteLine("");
      }
      Console.WriteLine("\n");
    }

    public static int[][] SolveEvo(int[][] problem, int numOrganisms, int maxEpochs)
    {
      // initialize combinatorial Organisms
      int numWorker = (int)(numOrganisms * 0.90);
      int numExplorer = numOrganisms - numWorker;
      Organism[] hive = new Organism[numOrganisms];

      int bestError = int.MaxValue;
      int[][] bestMatrix = null;

      int organismType = -1;
      for (int i = 0; i < numOrganisms; ++i)
      {
        if (i < numWorker)
          organismType = 0;  // worker
        else
          organismType = 1;  // explorer

        int[][] rndMatrix = RandomMatrix(problem);
        int err = Error(rndMatrix);

        hive[i] = new Organism(organismType, rndMatrix, err, 0);

        if (err < bestError)
        {
          bestError = err;
          bestMatrix = DuplicateMatrix(rndMatrix);  // by value
        }
      }

      // main loop
      int epoch = 0;
      while (epoch < maxEpochs)
      {
        if (epoch % 1000 == 0)
        {
          Console.Write("epoch = " + epoch);
          Console.WriteLine(" best error = " + bestError);
        }

        if (bestError == 0)  // solution found
          break;

        // process each organism
        for (int i = 0; i < numOrganisms; ++i)
        {
          if (hive[i].type == 0) // worker
          {
            int[][] neighbor = NeighborMatrix(problem, hive[i].matrix);
            int err = Error(neighbor);

            double p = rnd.NextDouble();
            if (err < hive[i].error || p < 0.001)  // better, or a mistake
            {
              hive[i].matrix = DuplicateMatrix(neighbor);  // by value
              hive[i].error = err;
              if (err < hive[i].error) hive[i].age = 0;

              if (err < bestError)
              {
                bestError = err;
                bestMatrix = DuplicateMatrix(neighbor);
              }
            }
            else  // neighbor is not better
            {
              hive[i].age++;
              if (hive[i].age > 1000)  // die
              {
                int[][] m = RandomMatrix(problem);
                hive[i] = new Organism(0, m, Error(m), 0);
              }

            }
          } // worker
          else if (hive[i].type == 1) // explorer
          {
            int[][] rndMatrix = RandomMatrix(problem);
            hive[i].matrix = DuplicateMatrix(rndMatrix);
            hive[i].error = Error(rndMatrix);

            if (hive[i].error < bestError)
            {
              bestError = hive[i].error;
              bestMatrix = DuplicateMatrix(rndMatrix);
            }
          }

        } // each organism

        // merge best worker with best explorer into worst worker
        int bestwi = 0;  // index of best worker
        int smallestWorkerError = hive[0].error;
        for (int i = 0; i < numWorker; ++i)
        {
          if (hive[i].error < smallestWorkerError)
          {
            smallestWorkerError = hive[i].error;
            bestwi = i;
          }
        }

        int bestei = numWorker;  // index of best explorer
        int smallestExplorerError = hive[numWorker].error;
        for (int i = numWorker; i < numOrganisms; ++i)
        {
          if (hive[i].error < smallestExplorerError)
          {
            smallestExplorerError = hive[i].error;
            bestei = i;
          }
        }

        int worstwi = 0;  // index of worst worker
        int largestWorkerError = hive[0].error;
        for (int i = 0; i < numWorker; ++i)
        {
          if (hive[i].error > largestWorkerError)
          {
            largestWorkerError = hive[i].error;
            worstwi = i;
          }
        }

        int[][] merged = MergeMatrices(hive[bestwi].matrix, hive[bestei].matrix);

        hive[worstwi] = new Organism(0, merged, Error(merged), 0);
        if (hive[worstwi].error < bestError)
        {
          bestError = hive[worstwi].error;
          bestMatrix = DuplicateMatrix(merged);
        }

        ++epoch;
      }  // while

      return bestMatrix;

    } // SolveEvo

    public static int[][] RandomMatrix(int[][] problem)
    {
      // fill each 3x3 block with 1-9
      int[][] result = DuplicateMatrix(problem);

      for (int block = 0; block < 9; ++block)
      {
        int[] corners = Corner(block);
        List<int> vals = new List<int>(9);
        for (int i = 1; i <= 9; ++i)
          vals.Add(i);

        // shuffle
        for (int k = 0; k < vals.Count; ++k)
        {
          int ri = rnd.Next(k, vals.Count);
          int tmp = vals[k];
          vals[k] = vals[ri];
          vals[ri] = tmp;
        }

        // walk thru block and remove from list starting numbers in problem
        int r = corners[0]; int c = corners[1];
        for (int i = r; i < r + 3; ++i)
        {
          for (int j = c; j < c + 3; ++j)
          {
            int v = problem[i][j];
            if (v != 0) // a fixed starting number
              vals.Remove(v);
          }
        }

        // walk thru block and add values
        int ptr = 0; // pointer into List
        for (int i = r; i < r + 3; ++i)
        {
          for (int j = c; j < c + 3; ++j)
          {
            if (result[i][j] == 0) // not occupied
            {
              int v = vals[ptr];  // get value from List
              result[i][j] = v;
              ++ptr; // move to next value in List
            }
          }
        }

      } // each block, k

      return result;
    } // RandomMatrix

    public static int[] Corner(int block)
    {
      int r = -1, c = -1;

      if (block == 0 || block == 1 || block == 2)
        r = 0;
      else if (block == 3 || block == 4 || block == 5)
        r = 3;
      else if (block == 6 || block == 7 || block == 8)
        r = 6;

      if (block == 0 || block == 3 || block == 6)
        c = 0;
      else if (block == 1 || block == 4 || block == 7)
        c = 3;
      else if (block == 2 || block == 5 || block == 8)
        c = 6;

      return new int[] { r, c };
    }

    public static int Block(int r, int c)
    {
      if (r >= 0 && r <= 2 && c >= 0 && c <= 2)
        return 0;
      else if (r >= 0 && r <= 2 && c >= 3 && c <= 5)
        return 1;
      else if (r >= 0 && r <= 2 && c >= 6 && c <= 8)
        return 2;
      else if (r >= 3 && r <= 5 && c >= 0 && c <= 2)
        return 3;
      else if (r >= 3 && r <= 5 && c >= 3 && c <= 5)
        return 4;
      else if (r >= 3 && r <= 5 && c >= 6 && c <= 8)
        return 5;
      else if (r >= 6 && r <= 8 && c >= 0 && c <= 2)
        return 6;
      else if (r >= 6 && r <= 8 && c >= 3 && c <= 5)
        return 7;
      else if (r >= 6 && r <= 8 && c >= 6 && c <= 8)
        return 8;
      else
        throw new Exception("Unable to find Block()");
    }

    public static int[][] NeighborMatrix(int[][] problem, int[][] matrix)
    {
      // pick a random 3x3 block,
      // pick two random cells in block
      // swap values

      int[][] result = DuplicateMatrix(matrix);

      int block = rnd.Next(0, 9);  // [0,8]
      //Console.WriteLine("block = " + block);
      int[] corners = Corner(block);
      //Console.WriteLine("corners = " + corners[0] + " " + corners[1]);
      List<int[]> cells = new List<int[]>();
      for (int i = corners[0]; i < corners[0] + 3; ++i)
      {
        for (int j = corners[1]; j < corners[1] + 3; ++j)
        {
          //Console.WriteLine("problem " + i + " " + j + " = " + problem[i][j]);
          if (problem[i][j] == 0)  // a non-fixed value
          {
            cells.Add(new int[] { i, j });
          }
        }
      }

      if (cells.Count < 2)
      {
        Console.WriteLine("Cells count = " + cells.Count);
        throw new Exception("block " + block + " doesn't have two values to swap!");
      }

      // pick two. suppose there are 4 possible cells 0,1,2,3
      int k1 = rnd.Next(0, cells.Count);  // 0,1,2,3
      int inc = rnd.Next(1, cells.Count);  // 1,2,3
      int k2 = (k1 + inc) % cells.Count;
      //Console.WriteLine("k1 k2 = " + k1 + " " + k2);

      int r1 = cells[k1][0];
      int c1 = cells[k1][1];
      int r2 = cells[k2][0];
      int c2 = cells[k2][1];

      //Console.WriteLine("r1 c1 = " + r1 + " " + c1);
      //Console.WriteLine("r2 c2 = " + r2 + " " + c2);

      int tmp = result[r1][c1];
      result[r1][c1] = result[r2][c2];
      result[r2][c2] = tmp;

      return result;

    } // NeighborMatrix

    public static int[][] MergeMatrices(int[][] m1, int[][] m2)
    {
      int[][] result = DuplicateMatrix(m1);

      for (int block = 0; block < 9; ++block)
      {
        double pr = rnd.NextDouble();
        if (pr < 0.50)
        {
          int[] corners = Corner(block);
          for (int i = corners[0]; i < corners[0] + 3; ++i)
            for (int j = corners[1]; j < corners[1] + 3; ++j)
              result[i][j] = m2[i][j];
        }
      }
      return result;
    }

    public static int Error(int[][] matrix)
    {
      int err = 0;
      // assumes blocks are OK (one each 1-9)

      // rows error
      for (int i = 0; i < 9; ++i)  // each row
      {
        int[] counts = new int[9];  // [0] = count of 1s, [1] = count of 2s
        for (int j = 0; j < 9; ++j)  // walk down column of curr row
        {
          int v = matrix[i][j];  // 1 to 9
          ++counts[v - 1];
        }

        for (int k = 0; k < 9; ++k)  // number missing 
        {
          if (counts[k] == 0)
            ++err;
        }

      }  // each row

      // columns error
      for (int j = 0; j < 9; ++j)  // each column
      {
        int[] counts = new int[9];  // [0] = count of 1s, [1] = count of 2s

        for (int i = 0; i < 9; ++i)  // walk down 
        {
          int v = matrix[i][j];  // 1 to 9
          ++counts[v - 1];  // counts[0-8]
        }

        for (int k = 0; k < 9; ++k)  // number missing in the colum
        {
          if (counts[k] == 0)
            ++err;
        }
      } // each column

      return err;
    } // Error

    public static int[][] DuplicateMatrix(int[][] matrix)
    {
      int[][] result = CreateMatrix(9);
      for (int i = 0; i < 9; ++i)
        for (int j = 0; j < 9; ++j)
          result[i][j] = matrix[i][j];
      return result;
    }

    public static int[][] CreateMatrix(int n)
    {
      int[][] result = new int[n][];
      for (int i = 0; i < n; ++i)
        result[i] = new int[n];
      return result;
    }

  } // SudokuEvoProgram

  public class Organism
  {
    public int type;  // 0 = worker, 1 = explorer
    public int[][] matrix;
    public int error;
    public int age;

    public Organism(int type, int[][] m, int error, int age)
    {
      this.type = type;
      this.matrix = SudokuEvoProgram.DuplicateMatrix(m);
      this.error = error;
      this.age = age;
    }
  }

  
} // ns
