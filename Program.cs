using System;
using System.Collections.Generic;

namespace VarmDrinkStation
{
    // Definierar ett interface för varma drycker
    public interface IWarmDrink
    {
        void Consume(); // Metod för att konsumera drycken
    }

    // Implementerar en specifik varm dryck, i detta fall vatten
    internal class Water : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Njut av ditt varma vatten!"); // Utskrift vid konsumtion av vatten
        }
    }

    // Implementerar en specifik varm dryck - kaffe
    internal class Coffee : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Njut av ditt kaffe!"); // Utskrift vid konsumtion av kaffe
        }
    }

    // Implementerar en specifik varm dryck - cappuccino
    internal class Cappuccino : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Njut av din cappuccino!"); // Utskrift vid konsumtion av cappuccino
        }
    }

    // Implementerar en specifik varm dryck - choklad
    internal class HotChocolate : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Njut av din varma choklad!"); // Utskrift vid konsumtion av choklad
        }
    }

    // Definierar ett interface för fabriker som kan skapa varma drycker
    public interface IWarmDrinkFactory
    {
        IWarmDrink Prepare(int total); // Metod för att förbereda drycken med en specifik mängd
    }

    // Implementerar en specifik fabrik som förbereder varmt vatten
    internal class HotWaterFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Häller {total} ml varmt vatten i din kopp"); // Utskrift av mängden vatten som hälls upp
            return new Water(); // Returnerar en ny instans av Water
        }
    }

    // Implementerar fabrik för kaffe
    internal class CoffeeFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Häller {total} ml varmt kaffe i din kopp"); // Utskrift av mängden kaffe som hälls upp
            return new Coffee(); // Returnerar en ny instans av Coffee
        }
    }

    // Implementerar fabrik för cappuccino
    internal class CappuccinoFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Häller {total} ml varm cappuccino i din kopp"); // Utskrift av mängden cappuccino som hälls upp
            return new Cappuccino(); // Returnerar en ny instans av Cappuccino
        }
    }

    // Implementerar fabrik för choklad
    internal class HotChocolateFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Häller {total} ml varm choklad i din kopp"); // Utskrift av mängden choklad som hälls upp
            return new HotChocolate(); // Returnerar en ny instans av HotChocolate
        }
    }

    // Maskin som hanterar skapandet av varma drycker
    public class WarmDrinkMachine
    {
        private readonly List<Tuple<string, IWarmDrinkFactory>> namedFactories; // Lista över fabriker med deras namn

        public WarmDrinkMachine()
        {
            namedFactories = new List<Tuple<string, IWarmDrinkFactory>>(); // Initierar listan över fabriker

            // Registrerar fabriker explicit
            RegisterFactory<HotWaterFactory>("Varmt Vatten"); // Registrerar fabriken för varmt vatten
            RegisterFactory<CoffeeFactory>("Kaffe"); // Registrerar fabriken för kaffe
            RegisterFactory<CappuccinoFactory>("Cappuccino"); // Registrerar fabriken för cappuccino
            RegisterFactory<HotChocolateFactory>("Varm Choklad"); // Registrerar fabriken för choklad
        }

        // Metod för att registrera en fabrik
        private void RegisterFactory<T>(string drinkName) where T : IWarmDrinkFactory, new()
        {
            namedFactories.Add(Tuple.Create(drinkName, (IWarmDrinkFactory)Activator.CreateInstance(typeof(T)))); // Lägger till fabriken i listan
        }

        // Metod för att skapa en varm dryck
        public IWarmDrink MakeDrink()
        {
            string coffeeArt = @"
    {
     {   }
      }_{ __{
   .-{   }   }-.
  (   }     {   )
  |`-.._____..-'|
  |             ;--.
  |            (__  \
  |   Edugrade  | )  )
  |  Java Brew  |/  /
  |             /  /    
  |            (  /
   \            \/
    `-..____..-'";

            Console.WriteLine(coffeeArt);
            Console.WriteLine("Varmt välkmmen till Edugrade Java Brew, vad får det lov att vara idag?");
            for (var index = 0; index < namedFactories.Count; index++)
            {
                var tuple = namedFactories[index];
                Console.WriteLine($"{index}: {tuple.Item1}"); // Skriver ut tillgängliga drycker
            }
            Console.WriteLine("Välj ett nummer för att fortsätta:");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int i) && i >= 0 && i < namedFactories.Count) // Läser och validerar användarens val
                {
                    Console.WriteLine("Välj storlek:");
                    Console.WriteLine("1: Liten (300 ml)");
                    Console.WriteLine("2: Stor (450 ml)");

                    if (int.TryParse(Console.ReadLine(), out int size) && (size == 1 || size == 2))
                    {
                        int total = size == 1 ? 300 : 450; // Bestäm storleken baserat på användarens val
                        return namedFactories[i].Item2.Prepare(total); // Förbereder och returnerar drycken
                    }
                }
                Console.WriteLine("Något gick fel med din inmatning, försök igen."); // Meddelande vid felaktig inmatning
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new WarmDrinkMachine(); // Skapar en instans av WarmDrinkMachine
            IWarmDrink drink = machine.MakeDrink(); // Skapar en dryck
            drink.Consume(); // Konsumerar drycken
        }
    }
}
