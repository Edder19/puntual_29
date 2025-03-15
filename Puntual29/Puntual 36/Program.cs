namespace Puntual36;

class Program
{
    static Dictionary<string, (int, int)> moves = new Dictionary<string, (int, int)>
    {
        { "UL", (-2, -1) }, { "UR", (-2, 1) }, { "LU", (-1, -2) }, { "LD", (1, -2) },
        { "RU", (-1, 2) }, { "RD", (1, 2) }, { "DL", (2, -1) }, { "DR", (2, 1) }
    };

    static (int, int) ParsePosition(string pos)
    {
        if (string.IsNullOrEmpty(pos) || pos.Length < 2) return (-1, -1);
        int col = pos[0] - 'A';
        int row = 8 - (pos[1] - '0');
        return (row, col);
    }

    static Dictionary<(int, int), char> ParseFruits(string input)
    {
        Dictionary<(int, int), char> fruits = new Dictionary<(int, int), char>();
        if (string.IsNullOrWhiteSpace(input)) return fruits;
        string[] items = input.Split(',');

        foreach (string item in items)
        {
            if (item.Length < 3) continue; // Validación para evitar errores
            string pos = item.Substring(0, 2);
            char fruit = item[2];
            (int row, int col) = ParsePosition(pos);
            if (row >= 0 && col >= 0)
                fruits[(row, col)] = fruit;
        }
        return fruits;
    }

    static (int, int)? MoveKnight((int, int) position, string move)
    {
        if (moves.ContainsKey(move))
        {
            (int dr, int dc) = moves[move];
            int newRow = position.Item1 + dr;
            int newCol = position.Item2 + dc;
            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                return (newRow, newCol);
        }
        return null;
    }

    static string MainLogic(string fruitInput, string startPos, string[] movesInput)
    {
        Dictionary<(int, int), char> fruits = ParseFruits(fruitInput);
        (int, int) knightPos = ParsePosition(startPos);
        if (knightPos.Item1 == -1 || knightPos.Item2 == -1) return "Posición inicial inválida.";
        List<char> collectedFruits = new List<char>();

        foreach (string move in movesInput)
        {
            if (string.IsNullOrWhiteSpace(move)) continue;
            var newPos = MoveKnight(knightPos, move);
            if (newPos != null)
            {
                knightPos = newPos.Value;
                if (fruits.TryGetValue(knightPos, out char fruit))
                {
                    collectedFruits.Add(fruit);
                    fruits.Remove(knightPos);
                }
            }
        }

        return collectedFruits.Count > 0 ? string.Join("", collectedFruits) : "";

    }

    static void Main()
    {
        Console.Write("Ingrese ubicación de los frutos: ");
        string fruitInput = Console.ReadLine() ?? "";

        Console.Write("Ingrese posición inicial del caballo: ");
        string startPos = (Console.ReadLine() ?? "").ToUpper();

        Console.Write("Ingrese los movimientos del caballo (separados por coma): ");
        string inputMoves = Console.ReadLine() ?? "";
        string[] movesInput = inputMoves.ToUpper().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        string resultado = MainLogic(fruitInput, startPos, movesInput);
        if (!string.IsNullOrEmpty(resultado))
            Console.WriteLine("Los frutos recogidos son: " + resultado);

        Console.WriteLine(MainLogic(fruitInput, startPos, movesInput));
    }
}



