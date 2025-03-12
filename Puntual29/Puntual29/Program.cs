
namespace Puntual29;




class Ejercicio29
{
    static void Main()
    {


        {
            while (true)
            {
                Console.Write("Ingrese el puente (o escriba 'salir' para terminar): ");
                string puente = Console.ReadLine() ?? "";
                if (puente.ToLower() == "salir") break;
                Console.WriteLine(EsValido(puente) ? "VALIDO" : "INVALIDO");
            }
        }
        {
            Console.Write("Ingrese el puente: ");
            string puente = Console.ReadLine() ?? "";
            Console.WriteLine(EsValido(puente) ? "VALIDO" : "INVALIDO");
        }
    }

    public static bool EsValido(string puente)
    {
        int n = puente.Length;
        if (n < 2 || puente[0] != '*' || puente[n - 1] != '*') return false;

        for (int i = 1; i < n - 1; i++)
        {
            char c = puente[i];
            if (c != '*' && c != '=' && c != '+') return false;

            if (i < n - 2 && puente[i] == '=' && puente[i + 1] == '=' && puente[i + 2] == '=')
            {
                if (n % 2 == 0 || i != n / 2 - 1) return false;
            }

            if (i < n - 1 && puente[i] == '=' && puente[i + 1] == '=' && (i == 1 || puente[i - 1] != '+') && (i + 2 == n - 1 || puente[i + 2] != '+'))
            {
                return false;
            }
        }

        for (int i = 1; i < n / 2; i++)
        {
            if (puente[i] != puente[n - i - 1]) return false;
        }

        return true;
    }
}

