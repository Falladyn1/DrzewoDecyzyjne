using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewoDecyzyjne
{
    internal class CV
    {   
        int[] indexTable;
        int k;
        
        //konstruktor
        public CV(int k, int size)
        {
            indexTable = new int[size];
            for (int i = 0; i < size; i++)
                indexTable[i] = i;
            Random.Shared.Shuffle(indexTable);

            this.k = k;
        }

        public List<(int[], int[])> MakeCV()
        {
            List<(int[], int[])> dataIndex = new List<(int[], int[])>();
            double step = (double)indexTable.Length / k;
            double count = 0;

            for (int i = 0; i < k; i++)
            {
                int start = (int)Math.Round(count);
                int end = (int)Math.Round(count + step);
                if (i == k - 1) end = indexTable.Length;

                int[] test = new int[end - start];
                int[] train = new int[indexTable.Length - test.Length];

                int testIdx = 0;
                int trainIdx = 0;

                for (int j = 0; j < indexTable.Length; j++)
                {
                    if (j >= start && j < end)
                    {
                        test[testIdx++] = indexTable[j];
                    }
                    else
                    {
                        train[trainIdx++] = indexTable[j];
                    }
                }

                dataIndex.Add((train, test));
                count += step; 
            }
            return dataIndex;
        }
    }
}
