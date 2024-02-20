

using System.Diagnostics;

namespace Lab5Math
{
    public class Program
    {
        static int[,] ReadMatrixFromFile(string filename)
        {
            int[,] matrix = new int[10, 10];
            string[] lines = File.ReadAllLines(filename);

            for (int i = 0; i < 10; i++)
            {
                string[] values = lines[i].Split(' ');
                for (int j = 0; j < 10; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }
            }

            return matrix;
        }

        public static void Main()
        {
            var fileName = "g1";
            for(int i = 1; i <= 4; i++)
            {
                var fileFullName = fileName + i + ".txt";
                var matrix = new Matrix(ReadMatrixFromFile(fileFullName));

                Console.WriteLine($"\t\t>>{fileFullName}<<");
                Console.WriteLine("Компонента матрицы: " + matrix.GetCountComponents());
                matrix.PrintReachabilityMatrix();
                fun(matrix.matrix, fileName + i);
            }
            Console.ReadKey(true);
        }
        static void fun(int[,] adjacencyMatrix, string name)
        {

            string dotFilePath = name + "graph.dot";
            string imageFilePath = name + "graph.png";

            // Создаем файл .dot для описания графа
            using (StreamWriter sw = new StreamWriter(dotFilePath))
            {
                sw.WriteLine("graph G {");
                for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                {
                    for (int j = i; j < adjacencyMatrix.GetLength(1); j++)
                    {
                        if (adjacencyMatrix[i, j] == 1)
                        {
                            sw.WriteLine($"{i} -- {j};");
                        }
                    }
                }
                sw.WriteLine("}");
            }

            // Запускаем Graphviz для создания изображения
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dot",
                Arguments = $"-Tpng {dotFilePath} -o {imageFilePath}",
                UseShellExecute = true
            };
            Process.Start(startInfo);

            Console.WriteLine($"Граф создан и сохранен как {imageFilePath}");
        }
    }
}