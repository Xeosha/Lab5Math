using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5Math
{
    public class Matrix
    {
        private int matrixSize;
        public int[,] matrix;   // матрица смежности

        public Matrix()
        {
            matrixSize = 10;
            matrix = new int[matrixSize, matrixSize];
        }

        public Matrix(int[,] matrix)
        {
            this.matrixSize = matrix.GetLength(0);
            this.matrix = matrix;
        }

        /// <summary>
        /// Количество компонентов связности
        /// </summary>
        /// <returns></returns>
        public int GetCountComponents()
        {
            bool[] visited = new bool[matrixSize];
            int countComponents = 0;

            for (int i = 0; i < matrixSize; i++)
            {
                if (!visited[i])
                {
                    DFS(i, visited);
                    countComponents++;
                }
            }

            return countComponents;
        }

        /// <summary>
        /// Обход в глубину
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="visited"></param>
        private void DFS(int vertex, bool[] visited) 
        {
            visited[vertex] = true;

            for (int i = 0; i < matrixSize; i++)
            {
                if (matrix[vertex, i] == 1 && !visited[i])
                {
                    DFS(i, visited);
                }
            }
        }

        /// <summary>
        /// Матрица достижимости
        /// </summary>
        /// <param name="adjacencyMatrix"></param>
        /// <returns></returns>
        public int[,] GetReachabilityMatrix()
        {
            int[,] reachabilityMatrix = new int[matrixSize, matrixSize];

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    reachabilityMatrix[i, j] = i == j ? 1 : matrix[i, j];         
                }
            }

            for (int k = 0; k < matrixSize; k++)
            {
                for (int i = 0; i < matrixSize; i++)
                {   
                    for (int j = 0; j < matrixSize; j++)
                    {
                        if (reachabilityMatrix[i, k] == 1 && reachabilityMatrix[k, j] == 1)
                        {
                            reachabilityMatrix[i, j] = 1;
                        }
                    }
                }
            }

            return reachabilityMatrix;
        }

        public void PrintReachabilityMatrix()
        {
            Matrix Matrix = new Matrix(GetReachabilityMatrix());
            Console.WriteLine($"Матрица достижимости:\n{Matrix}");
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int width = matrix.GetLength(1);
            int height = matrix.GetLength(0);
            stringBuilder.AppendLine("┌" + new string('─', width * 4 - 1) + "┐");
            for (int i = 0; i < height; i++)
            {
                stringBuilder.Append("│ ");
                for (int j = 0; j < width; j++)
                {
                    if (matrix[j, i] == int.MaxValue)
                        stringBuilder.Append($"X │ ");
                    else
                        stringBuilder.Append($"{matrix[i, j]} │ ");
                }
                stringBuilder.AppendLine();

                if (i == height - 1)
                {
                    stringBuilder.AppendLine("└" + new string('─', width * 4 - 1) + "┘");
                }
                else
                {
                    stringBuilder.AppendLine("├" + new string('─', width * 4 - 1) + "┤");
                }
            }
            return stringBuilder.ToString();
        }
    }
}
