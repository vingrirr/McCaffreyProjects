using System;
using System.Collections.Generic;
//using System.IO;

namespace ConsensusClassification
{
  class ConsensusProgram
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin consensus classification demo");
      Console.WriteLine("Goal is predict political party based on 16 votes \n");
      //string[][] allData = LoadData("..\\..\\VotingRaw.txt");
      string[][] allData = new string[100][];
      allData[0] = new string[] { "n", "y", "y", "n", "y", "y", "n", "n", "n", "n", "n", "n", "y", "y", "y", "y", "democrat" };
      allData[1] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "y", "republican" };
      allData[2] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[3] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[4] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "y", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[5] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "n", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[6] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[7] = new string[] { "y", "n", "n", "y", "y", "n", "y", "y", "y", "n", "n", "y", "y", "y", "n", "y", "republican" };
      allData[8] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[9] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "n", "republican" };
      allData[10] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "y", "n", "n", "y", "n", "y", "y", "democrat" };
      allData[11] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "y", "republican" };
      allData[12] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[13] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "n", "republican" };
      allData[14] = new string[] { "y", "y", "n", "y", "y", "y", "n", "n", "n", "n", "n", "n", "y", "y", "n", "y", "republican" };
      allData[15] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "n", "republican" };
      allData[16] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "y", "n", "y", "y", "democrat" };
      allData[17] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "n", "y", "democrat" };
      allData[18] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[19] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "n", "y", "democrat" };
      allData[20] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "n", "n", "n", "n", "n", "y", "n", "y", "democrat" };
      allData[21] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[22] = new string[] { "y", "y", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "n", "republican" };
      allData[23] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "n", "republican" };
      allData[24] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "y", "republican" };
      allData[25] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "y", "republican" };
      allData[26] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "y", "republican" };
      allData[27] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "n", "republican" };
      allData[28] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "y", "n", "n", "n", "n", "y", "democrat" };
      allData[29] = new string[] { "y", "y", "n", "y", "y", "y", "y", "n", "n", "n", "n", "y", "y", "y", "n", "y", "republican" };
      allData[30] = new string[] { "n", "y", "n", "y", "y", "y", "y", "n", "n", "n", "y", "y", "y", "y", "n", "y", "republican" };
      allData[31] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "n", "republican" };
      allData[32] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[33] = new string[] { "y", "y", "y", "y", "n", "n", "y", "y", "y", "y", "y", "n", "n", "y", "n", "y", "republican" };
      allData[34] = new string[] { "y", "n", "y", "y", "y", "n", "y", "n", "y", "y", "n", "n", "y", "y", "n", "y", "republican" };
      allData[35] = new string[] { "y", "n", "y", "n", "n", "y", "y", "y", "y", "y", "y", "n", "n", "y", "y", "y", "democrat" };
      allData[36] = new string[] { "n", "y", "y", "y", "y", "y", "n", "n", "n", "y", "y", "n", "y", "y", "n", "n", "democrat" };
      allData[37] = new string[] { "n", "y", "y", "y", "y", "y", "n", "y", "y", "y", "y", "y", "y", "y", "n", "y", "democrat" };
      allData[38] = new string[] { "y", "y", "y", "n", "y", "y", "n", "n", "n", "y", "y", "n", "y", "y", "n", "y", "democrat" };
      allData[39] = new string[] { "n", "n", "n", "y", "y", "n", "n", "n", "n", "y", "n", "y", "y", "y", "n", "n", "republican" };
      allData[40] = new string[] { "n", "n", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "y", "republican" };
      allData[41] = new string[] { "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "n", "republican" };
      allData[42] = new string[] { "n", "n", "y", "n", "y", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "y", "democrat" };
      allData[43] = new string[] { "n", "n", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "n", "republican" };
      allData[44] = new string[] { "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "n", "republican" };
      allData[45] = new string[] { "n", "y", "y", "n", "y", "y", "y", "n", "y", "y", "y", "n", "y", "y", "n", "y", "democrat" };
      allData[46] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "y", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[47] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[48] = new string[] { "y", "n", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[49] = new string[] { "y", "n", "y", "n", "y", "y", "n", "n", "n", "n", "n", "n", "n", "n", "n", "y", "democrat" };
      allData[50] = new string[] { "y", "n", "n", "n", "y", "y", "y", "n", "n", "y", "y", "n", "n", "y", "n", "y", "democrat" };
      allData[51] = new string[] { "y", "y", "y", "n", "n", "y", "y", "y", "y", "y", "n", "n", "n", "n", "n", "y", "democrat" };
      allData[52] = new string[] { "y", "n", "n", "n", "y", "y", "n", "n", "n", "n", "y", "y", "n", "y", "n", "y", "democrat" };
      allData[53] = new string[] { "y", "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "n", "n", "y", "n", "y", "democrat" };
      allData[54] = new string[] { "y", "y", "y", "n", "n", "n", "n", "y", "y", "n", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[55] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "y", "republican" };
      allData[56] = new string[] { "n", "n", "y", "n", "n", "n", "y", "y", "y", "y", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[57] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "n", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[58] = new string[] { "y", "y", "y", "y", "y", "n", "y", "n", "n", "n", "n", "y", "y", "y", "n", "y", "republican" };
      allData[59] = new string[] { "n", "y", "y", "n", "n", "n", "n", "y", "y", "y", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[60] = new string[] { "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "n", "republican" };
      allData[61] = new string[] { "n", "n", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "n", "y", "n", "y", "republican" };
      allData[62] = new string[] { "n", "n", "y", "n", "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "democrat" };
      allData[63] = new string[] { "n", "n", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "y", "republican" };
      allData[64] = new string[] { "n", "n", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "n", "republican" };
      allData[65] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "y", "y", "y", "n", "n", "y", "republican" };
      allData[66] = new string[] { "n", "n", "y", "n", "n", "y", "y", "y", "y", "y", "n", "n", "n", "y", "n", "y", "democrat" };
      allData[67] = new string[] { "y", "n", "y", "n", "n", "y", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[68] = new string[] { "n", "n", "n", "y", "n", "n", "y", "y", "y", "y", "n", "n", "y", "y", "n", "y", "republican" };
      allData[69] = new string[] { "n", "n", "n", "y", "y", "y", "y", "y", "y", "y", "n", "y", "y", "y", "n", "y", "republican" };
      allData[70] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "y", "republican" };
      allData[71] = new string[] { "n", "n", "n", "n", "n", "n", "y", "y", "y", "y", "n", "y", "y", "y", "y", "y", "democrat" };
      allData[72] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "y", "republican" };
      allData[73] = new string[] { "n", "n", "y", "n", "n", "n", "y", "y", "y", "y", "n", "n", "y", "n", "y", "y", "democrat" };
      allData[74] = new string[] { "y", "y", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "y", "republican" };
      allData[75] = new string[] { "n", "y", "y", "n", "n", "y", "n", "y", "y", "y", "y", "n", "y", "n", "y", "y", "democrat" };
      allData[76] = new string[] { "n", "n", "y", "n", "n", "y", "y", "y", "y", "y", "y", "n", "y", "y", "n", "y", "democrat" };
      allData[77] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "n", "n", "republican" };
      allData[78] = new string[] { "y", "y", "n", "y", "y", "y", "y", "n", "n", "n", "n", "y", "y", "y", "n", "n", "republican" };
      allData[79] = new string[] { "n", "y", "n", "y", "y", "y", "n", "n", "n", "y", "n", "y", "y", "y", "n", "n", "republican" };
      allData[80] = new string[] { "n", "y", "n", "n", "y", "y", "n", "n", "n", "n", "n", "y", "y", "y", "y", "y", "democrat" };
      allData[81] = new string[] { "n", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "y", "y", "y", "n", "y", "democrat" };
      allData[82] = new string[] { "n", "y", "y", "n", "y", "y", "y", "n", "n", "n", "y", "y", "y", "y", "n", "y", "democrat" };
      allData[83] = new string[] { "n", "y", "n", "y", "y", "y", "y", "n", "n", "n", "n", "y", "y", "y", "n", "y", "republican" };
      allData[84] = new string[] { "y", "n", "y", "y", "y", "y", "y", "y", "n", "y", "n", "y", "n", "y", "y", "y", "republican" };
      allData[85] = new string[] { "y", "n", "y", "y", "y", "y", "y", "y", "n", "y", "y", "y", "n", "y", "y", "y", "republican" };
      allData[86] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "n", "y", "n", "y", "democrat" };
      allData[87] = new string[] { "n", "n", "n", "n", "y", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "y", "democrat" };
      allData[88] = new string[] { "n", "y", "y", "n", "n", "n", "y", "y", "y", "y", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[89] = new string[] { "n", "n", "y", "y", "n", "n", "y", "y", "y", "y", "n", "n", "n", "y", "y", "y", "republican" };
      allData[90] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "y", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[91] = new string[] { "n", "n", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[92] = new string[] { "n", "n", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[93] = new string[] { "n", "y", "y", "n", "n", "n", "y", "y", "y", "y", "y", "n", "n", "n", "y", "y", "democrat" };
      allData[94] = new string[] { "y", "n", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[95] = new string[] { "n", "n", "n", "n", "n", "y", "y", "y", "y", "n", "y", "n", "n", "y", "y", "y", "democrat" };
      allData[96] = new string[] { "n", "n", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[97] = new string[] { "n", "n", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "y", "y", "y", "democrat" };
      allData[98] = new string[] { "y", "y", "y", "n", "n", "n", "y", "y", "y", "n", "n", "n", "n", "n", "y", "y", "democrat" };
      allData[99] = new string[] { "y", "n", "y", "n", "n", "y", "y", "y", "y", "y", "y", "n", "n", "n", "y", "y", "democrat" };

      Console.WriteLine("All data: \n");
      ShowData(allData, 5, true);

      Console.WriteLine("Creating 80-20 train-test data");
      string[][] trainData;
      string[][] testData;
      MakeTrainTest(allData, 0, out trainData, out testData); // 0 = seed

      Console.WriteLine("Training data: \n");
      ShowData(trainData, 3, true);

      Console.WriteLine("Test data: \n");
      ShowData(testData, 3, true);

      int numConditions = 5; // conditions per rule
      int maxNumRules = 500;
      double minAccuracy = 0.90; // min % rule accuracy

      Console.WriteLine("Setting number conditions per rule = " + numConditions);
      Console.WriteLine("Setting max number simple rules    = " + maxNumRules);
      Console.WriteLine("Setting simple rule min accuracy   = " + minAccuracy.ToString("F2"));
      ConsensusClassifier cc = new ConsensusClassifier(numConditions, maxNumRules);

      Console.WriteLine("\nStarting training");
      cc.Train(trainData, minAccuracy);
      Console.WriteLine("Done");
      Console.WriteLine("Created " + cc.RuleCount() + " simple rules");

      double trainAcc = cc.Accuracy(trainData);
      Console.WriteLine("\nAccuracy on train data = " + trainAcc.ToString("F4"));

      int numCorrect, numWrong, numUnknown;
      double testAcc = cc.Accuracy(testData, out numCorrect, out numWrong, out numUnknown);
      Console.WriteLine("\nAccuracy on test data  = " + testAcc.ToString("F4"));
      Console.WriteLine("Number correct = " + numCorrect);
      Console.WriteLine("Number wrong   = " + numWrong);
      Console.WriteLine("Number unknown = " + numUnknown);

      //string[] newData = new string[] { "y", "y", "y", "y", "y", "y", "y", "y", "n", "n", "n", "n", "n", "n", "n", "n" }; // LR gives dem (0)
      //int party = cc.ComputeOutput(newData);
      //Console.WriteLine("Predicted = " + party);

      Console.WriteLine("\nEnd consensus classification demo\n");
      Console.ReadLine();
    }

    //static string[][] LoadData(string catDataFile)
    //{
    //  // republican,n,y,n,y,y,y,n,n,n,n,n,y,y,y,n,?
    //  // democrat,?,y,y,?,y,y,n,n,n,n,y,n,y,y,n,n

    //  // prelim scan
    //  FileStream ifs = new FileStream(catDataFile, FileMode.Open);
    //  StreamReader sr = new StreamReader(ifs);
    //  string line = "";
    //  int numRows = 0;
    //  while ((line = sr.ReadLine()) != null)
    //  {
    //    //if (line.IndexOf('?') < 0) // usually -1 means not found
    //      ++numRows;
    //  }
    //  sr.Close();
    //  ifs.Close();

    //  string[][] result = new string[numRows][];

    //  ifs = new FileStream(catDataFile, FileMode.Open);
    //  sr = new StreamReader(ifs);

    //  string[] tokens = null;

    //  int i = 0;
    //  while ((line = sr.ReadLine()) != null)
    //  {
    //    //if (line.IndexOf('?') >= 0)
    //    //  continue;
    //    //Console.WriteLine("allData[" + i + " ] = new string[] { \"" + line + "\" };");

    //    tokens = line.Split(',');
    //    int numCols = tokens.Length;
    //    result[i] = new string[numCols]; // data file has 17 cols
    //    result[i][numCols - 1] = tokens[0]; // put party in last col
    //    for (int j = 1; j < numCols; ++j)
    //    {
    //      if (tokens[j] == "?")
    //        tokens[j] = "n";
    //      result[i][j - 1] = tokens[j]; // 'y', 'n', not '?' (missing)
          
    //    }

    //    Console.Write("allData[" + i + " ] = new string[] { \"");
    //    for (int j = 0; j < result[i].Length; ++j)
    //      Console.Write(result[i][j] + ",");
    //    Console.WriteLine("\" };");

    //    ++i;
    //  }

    //  sr.Close(); ifs.Close();
    //  return result; ;
    //} // LoadData

    static void MakeTrainTest(string[][] allData, int seed, out string[][] trainData, out string[][] testData)
    {
      Random rnd = new Random(seed);
      int totRows = allData.Length;
      int numTrainRows = (int)(totRows * 0.80); // 80-20 hard-coded split
      int numTestRows = totRows - numTrainRows;
      trainData = new string[numTrainRows][];
      testData = new string[numTestRows][];

      string[][] copy = new string[allData.Length][]; // ref copy of all data
      for (int i = 0; i < copy.Length; ++i)
        copy[i] = allData[i];

      for (int i = 0; i < copy.Length; ++i) // scramble order
      {
        int r = rnd.Next(i, copy.Length);
        string[] tmp = copy[r];
        copy[r] = copy[i];
        copy[i] = tmp;
      }
      for (int i = 0; i < numTrainRows; ++i)
        trainData[i] = copy[i];

      for (int i = 0; i < numTestRows; ++i)
        testData[i] = copy[i + numTrainRows];
    } // MakeTrainTest

    static void ShowData(string[][] rawData, int numRows, bool indices)
    {
      for (int i = 0; i < numRows; ++i)
      {
        if (indices == true)
          Console.Write("[" + i.ToString().PadLeft(3) + "]  ");
        for (int j = 0; j < rawData[i].Length; ++j)
        {
          string s = rawData[i][j];
          Console.Write(s.PadLeft(2) + " ");
        }
        Console.WriteLine("");
      }
      Console.WriteLine(". . .");
      int lastRow = rawData.Length - 1;
      if (indices == true)
        Console.Write("[" + lastRow.ToString().PadLeft(3) + "]  ");
      for (int j = 0; j < rawData[lastRow].Length; ++j)
      {
        string s = rawData[lastRow][j];
        Console.Write(s.PadLeft(2) + " ");
      }
      Console.WriteLine("\n");
    }
  } // Program

  public class ConsensusClassifier
  {
    private int numConditions; // num if..then conditions in a rule
    private int maxNumRules; // if..then..result
    private List<int[]> ruleList; // [3 0 2 5 0] means if feature 3 = 0 and feature 2 = 5 then y = 0
    private Dictionary<string, int>[] stringToInt; // [feature/col]["value"]
    private static Random rnd;

    public ConsensusClassifier(int numConditions, int maxNumRules)
    {
      this.numConditions = numConditions;
      this.maxNumRules = maxNumRules;
      ruleList = new List<int[]>();
      rnd = new Random(0);
    }

    public int RuleCount()
    {
      return this.ruleList.Count;
    }

    public int ComputeOutput(string[] dataItem)
    {
      // dataItem may have Y (train data) or not (x data)
      // number of votes determines 0 or 1 output
      int numZeroVotes = 0;
      int numOneVotes = 0;
      for (int ri = 0; ri < ruleList.Count; ++ri) // each rule
      {
        int[] rule = ruleList[ri];
        if (IsApplicable(rule, dataItem) == false)
          continue;
        if (rule[rule.Length - 1] == 0)
          ++numZeroVotes;
        else
          ++numOneVotes;
      }

      if (numZeroVotes > numOneVotes)
        return 0;
      else if (numOneVotes > numZeroVotes)
        return 1;
      else
        return -1; // tie
    }

    private bool IsApplicable(int[] rule, string[] dataItem)
    {
      // does rule apply to data item?
      // data item either includes Y or not
      for (int i = 0; i < rule.Length / 2; ++i) // each feature-value pair
      {
        int rFeature = rule[i * 2]; // rule feature
        int rValue = rule[i * 2 + 1]; // rule value of the feature
        string dsValue = dataItem[rFeature]; // value (as string) of data item feature
        int dValue = this.stringToInt[rFeature][dsValue]; // data item value as int
        if (rValue != dValue) // if any rule value != data item value, rule not applicable
          return false;
      }
      return true;
    }

    // --- create rule routines

    private void CreateLookups(string[][] trainData)
    {
      int numRows = trainData.Length;
      int numCols = trainData[0].Length;

      this.stringToInt = new Dictionary<string, int>[numCols]; // one dict per column including Y

      for (int c = 0; c < numCols; ++c) // include Y
      {
        stringToInt[c] = new Dictionary<string, int>(); // dict for this column
        int idx = 0; // 'answer' int
        for (int r = 0; r < numRows; ++r)
        {
          string s = trainData[r][c];
          if (stringToInt[c].ContainsKey(s) == false)
            stringToInt[c].Add(s, idx++);
        }
      }
    } // CreateLookups

    private void CreateRules(string[][] trainData, double minAccuracy)
    {
      int numRows = trainData.Length;
      int numCols = trainData[0].Length;

      int maxTrials = this.maxNumRules * 1000;
      int trial = 0;

      while (this.ruleList.Count < this.maxNumRules && trial <= maxTrials)
      {
        ++trial;

        int[] candidate = new int[numConditions * 2 + 1]; // feature=value -> Y
        int randomRow = rnd.Next(0, numRows); // select a random row

        // pick some distinct features (columns) from the row
        int[] features = RandomIndices(numConditions, numCols - 1); // sorted. not Y

        for (int i = 0; i < numConditions; ++i) // create the if..a=b's from random row
        {
          int feature = features[i]; // a column of training data
          string sValue = trainData[randomRow][feature]; // like "n"
          int iValue = this.stringToInt[feature][sValue]; // like 0
          candidate[i * 2] = features[i];
          candidate[i * 2 + 1] = iValue;
        }
        // create the then part from the random row
        string sDependent = trainData[randomRow][numCols - 1]; // like 'republican'
        int iDependent = stringToInt[numCols - 1][sDependent];  // like 0
        candidate[numConditions * 2] = iDependent; // last cell

        if (IsRuleInList(candidate) == true) // is candidate rule already in list?
          continue; // skip candidate

        if (RuleAccuracy(candidate, trainData) < minAccuracy) // is candidate accurate more than xx% of the time?
          continue;

        //if (Support(candidate, trainData) < minSupport) // does candidate apply to enough items?
        //  continue;

        // candidate rule is good!
        int[] newRule = new int[candidate.Length];
        Array.Copy(candidate, newRule, candidate.Length);

        this.ruleList.Add(newRule);
      } // while
    } // CreateRules

    private double RuleAccuracy(int[] rule, string[][] trainData)
    {
      int numRows = trainData.Length;
      int numCols = trainData[0].Length;

      int numCorrect = 0;
      int numWrong = 0;
      for (int i = 0; i < numRows; ++i)
      {
        if (IsApplicable(rule, trainData[i]) == false)
          continue; // rule not appliable

        int ruleY = rule[rule.Length - 1]; // Y part of rule
        string trainY = trainData[i][numCols - 1];
        int Y = this.stringToInt[numCols - 1][trainY]; // Y part of training item
        if (ruleY == Y)
          ++numCorrect;
        else
          ++numWrong;
      }
      if (numCorrect + numWrong == 0)
        return 0.0;
      return (numCorrect * 1.0) / (numCorrect + numWrong);
    }

    //private double Support(int[] rule, string[][] trainData)
    //{
    //  // percent of training items that rule applies to
    //  int numRows = trainData.Length;

    //  int numApplicable = 0;
    //  int numNotApplicable = 0;
    //  for (int i = 0; i < numRows; ++i)
    //  {
    //    if (IsApplicable(rule, trainData[i]) == true)
    //      ++numApplicable;
    //    else
    //      ++numNotApplicable;
    //  }
    //  return (numApplicable * 1.0) / (numApplicable + numNotApplicable);
    //}

    // ------------------------------------

    private bool IsRuleInList(int[] rule)
    {
      for (int i = 0; i < ruleList.Count; ++i) // each existing rule 
      {
        int[] currRuleInList = ruleList[i];
        if (AreEqual(rule, currRuleInList) == true)
          return true;
      }
      return false;
    }

    private static bool AreEqual(int[] rule1, int[] rule2)
    {
      for (int i = 0; i < rule1.Length; ++i)
        if (rule1[i] != rule2[i])
          return false;
      return true;
    }

    // ---------------------------------------------

    private static int[] RandomIndices(int n, int range)
    {
      int maxBruteAttempts = range * 2;
      int[] result;
      result = Brute(n, range, maxBruteAttempts);
      if (result == null)
        result = Reservoir(n, range); // guaranteed to succeed
      return result;
    }

    private static int[] Brute(int n, int range, int maxAttempts)
    {
      int[] result = new int[n];

      int attempt = 0;
      while (attempt < maxAttempts)
      {
        for (int i = 0; i < n; ++i)
          result[i] = rnd.Next(0, range);
        Array.Sort(result);
        if (HasDups(result) == false)
          return result;
        ++attempt;
      }
      return null;
    }

    private static bool HasDups(int[] array)
    {
      for (int i = 0; i < array.Length - 1; ++i)
        if (array[i] == array[i + 1])
          return true;
      return false;
    }

    private static int[] Reservoir(int n, int range)
    {
      // select n random indices between [0, range)
      // helper for CreateRules()
      int[] result = new int[n];
      for (int i = 0; i < n; ++i)
        result[i] = i;

      for (int t = n; t < range; ++t)
      {
        int m = rnd.Next(0, t + 1);
        if (m < n)
          result[m] = t;
      }
      Array.Sort(result);
      return result;
    }

    // ----------------------------------------

    public void Train(string[][] trainData, double minAccuracy)
    {
      CreateLookups(trainData); // create string-to-int lookups for feature-value
      CreateRules(trainData, minAccuracy);
      if (ruleList.Count == 0)
        throw new Exception("Unable to create any valid rules");
    }

    public double Accuracy(string[][] testData, out int numCorrect, out int numWrong, out int numUnknown)
    {
      int numRows = testData.Length;
      int numCols = testData[0].Length;

      numCorrect = 0;
      numWrong = 0;
      numUnknown = 0;
      for (int i = 0; i < numRows; ++i) // each data
      {
        int computedY = ComputeOutput(testData[i]); // 0 or 1
        if (computedY == -1) // indeterminate
        {
          ++numUnknown;
          continue;
        }
        string sDesired = testData[i][numCols - 1]; // ex: 'republican'
        int iDesired = this.stringToInt[numCols - 1][sDesired]; // 0 or 1
        if (computedY == iDesired)
          ++numCorrect;
        else
          ++numWrong;
      }

      if (numCorrect + numWrong == 0)
        return 0.0;
      return (1.0 * numCorrect) / (numCorrect + numWrong);
    }

    public double Accuracy(string[][] testData) // simpler interface
    {
      int numCorrect, numWrong, numUnknown;
      return Accuracy(testData, out numCorrect, out numWrong, out numUnknown);
    }


  } // ConsensusClassifier

} // ns
