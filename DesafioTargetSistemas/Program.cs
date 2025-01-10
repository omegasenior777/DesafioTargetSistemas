using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nEscolha o programa a executar:");
            Console.WriteLine("1 - Cálculo de SOMA");
            Console.WriteLine("2 - Sequência de Fibonacci");
            Console.WriteLine("3 - Faturamento Diário");
            Console.WriteLine("4 - Percentual de Representação");
            Console.WriteLine("5 - Inversão de String");
            Console.WriteLine("6 - Sair");

            int escolha;
            if (!int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                continue;
            }

            switch (escolha)
            {
                case 1:
                    Soma.Run();
                    break;
                case 2:
                    Fibonacci.Run();
                    break;
                case 3:
                    Faturamento.Run();
                    break;
                case 4:
                    FaturamentoEstado.Run();
                    break;
                case 5:
                    InverterString.Run();
                    break;
                case 6:
                    Console.WriteLine("Saindo...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}

class Soma
{
    public static void Run()
    {
        int INDICE = 13, SOMA = 0, K = 0;
        while (K < INDICE)
        {
            K = K + 1;
            SOMA = SOMA + K;
        }
        Console.WriteLine($"O valor de SOMA é: {SOMA}"); // Resultado esperado: 91
    }
}

class Fibonacci
{
    public static void Run()
    {
        Console.WriteLine("Informe um número para verificar se pertence à sequência de Fibonacci:");
        int numero = int.Parse(Console.ReadLine());

        bool PertenceAFibonacci(int num)
        {
            int a = 0, b = 1, temp;
            while (a <= num)
            {
                if (a == num) return true;
                temp = a;
                a = b;
                b = temp + b;
            }
            return false;
        }

        if (PertenceAFibonacci(numero))
            Console.WriteLine($"O número {numero} pertence à sequência de Fibonacci.");
        else
            Console.WriteLine($"O número {numero} NÃO pertence à sequência de Fibonacci.");
    }
}

class Faturamento
{
    public class Dados
    {
        [JsonPropertyName("dia")]
        public int Dia { get; set; }
        [JsonPropertyName("valor")]
        public decimal? Valor { get; set; }
    }
    public static void Run()
    {
        string jsonPath = "dados.json";
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            var faturamentoMensal = JsonSerializer.Deserialize<List<Dados>>(jsonContent);

            var diasComFaturamento = faturamentoMensal
                                                  .Where(f => f.Valor.HasValue && f.Valor.Value > 0)
                                                  .ToList();

            decimal menor = diasComFaturamento.Min(f => f.Valor.Value);
            decimal maior = diasComFaturamento.Max(f => f.Valor.Value);
            decimal media = diasComFaturamento.Average(f => f.Valor.Value);
            int diasAcimaDaMedia = diasComFaturamento.Count(f => f.Valor.Value > media);

            Console.WriteLine($"Menor faturamento: {menor:C}");
            Console.WriteLine($"Maior faturamento: {maior:C}");
            Console.WriteLine($"Dias com faturamento acima da média: {diasAcimaDaMedia}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar o arquivo JSON: {ex.Message}");
        }
    }
}

class FaturamentoEstado
{
    public static void Run()
    {
        decimal sp = 67836.43m;
        decimal rj = 36678.66m;
        decimal mg = 29229.88m;
        decimal es = 27165.48m;
        decimal outros = 19849.53m;

        decimal total = sp + rj + mg + es + outros;

        Console.WriteLine($"SP: {sp / total:P2}");
        Console.WriteLine($"RJ: {rj / total:P2}");
        Console.WriteLine($"MG: {mg / total:P2}");
        Console.WriteLine($"ES: {es / total:P2}");
        Console.WriteLine($"Outros: {outros / total:P2}");
    }
}

class InverterString
{
    public static void Run()
    {
        Console.WriteLine("Digite a string a ser invertida:");
        string entrada = Console.ReadLine();

        char[] caracteres = entrada.ToCharArray();
        string invertida = "";

        for (int i = caracteres.Length - 1; i >= 0; i--)
        {
            invertida += caracteres[i];
        }

        Console.WriteLine($"String invertida: {invertida}");
    }
}
