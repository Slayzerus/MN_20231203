while(true)
{
    decimal[][] matrix = ReadMatrix();
    if (matrix == null || matrix.Length == 0)
    {
        continue;
    }
    Console.WriteLine("Macierz początkowa:");
    WriteMatrix(matrix);
    Console.WriteLine();
    Console.WriteLine("Wynik eliminacji Gaussa-Jordana:");
    GaussJordanElimination(matrix);
}

decimal[][] ReadMatrix()
{
    Console.Write("Wprowadź rozmiar macierzy (nie licząc kolumny wartości): ");

    string sizeStr = Console.ReadLine() ?? "";
    int size = 0;

    if (!int.TryParse(sizeStr, out size))
    {
        Console.WriteLine($"Niepoprawny rozmiar macierzy: {sizeStr}");
        return new decimal[0][];
    }

    decimal[][] matrix = new decimal[size][];
    for (int y = 0; y < size; y++)
    {
        matrix[y] = new decimal[size + 1];
        for (int x = 0; x < size + 1; x++)
        {
            Console.Write($"M[{y + 1}][{x + 1}]=");
            string valueString = Console.ReadLine() ?? "";
            decimal value;
            while (!decimal.TryParse(valueString, out value))
            {
                Console.WriteLine($"Niepoprawna wartość elementu: {valueString}");
            }
            matrix[y][x] = value;
        }
    }

    return matrix;
}

void WriteMatrix(decimal[][] a, bool onlyLastColumn = false)
{
    for (int y = 0; y < a.Length; y++)
    {
        for (int x = onlyLastColumn ? a[y].Length - 1 : 0; x < a[y].Length; x++)
        {
            Console.Write(a[y][x].ToString("0.00") + "  ");
        }

        Console.WriteLine();
    }
}

void GaussJordanElimination(decimal[][] a)
{
    int size = a.Length;    
    
    for (int k = 0; k < size; k++)
    {
        decimal max = a[k][k];
        int r = k;

        for (int i = k; i < size; i++)
        {
            if (Math.Abs(a[i][k]) > Math.Abs(max))
            {
                max = a[i][k];
                r = i;
            }
        }

        if (max == 0)
        {
            Console.WriteLine("Macierz układu osobliwa");
            return;
        }

        for (int j = k; j < size+1; j++)
        {
            decimal temp = a[r][j];
            a[r][j] = a[k][j];
            a[k][j] = temp;
            a[k][j] = a[k][j] / max;
        }
        
        for (int i = 0 ; i < size; i++)
        {
            if (i == k)
            {
                continue;
            }

            decimal p = a[i][k];
            for (int j = k; j < size + 1; j++)
            {
                a[i][j] = a[i][j] - p * a[k][j];
            }
        }
    }

    WriteMatrix(a, true);
}
