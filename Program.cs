

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
            }
        }
    }
}