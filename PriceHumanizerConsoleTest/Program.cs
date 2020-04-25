using System;
using Interviews.VM.PriceHumanizer.Logic;

namespace Interviews.VM.PriceHumanizer.Client
{
    class ConsoleTestProgram
    {
        static void Main(string[] args)
        {
            IIntergerHumanizer _integerHumanaizer = new IntegerHumanizer();
            IReadableBuilder _currencyBuilder = new CurrencyReadableBuilder(_integerHumanaizer);
            var ch = new CurrencyHumanizer(_currencyBuilder, new CurrencyFormatParser());

            Console.WriteLine(ch.Humanize("2 000 000,22"));
            Console.WriteLine(ch.Humanize("000 222 000,22"));
            Console.WriteLine(ch.Humanize("207 222 000,22"));
            Console.WriteLine(ch.Humanize("022 222 222,22"));
            Console.WriteLine(ch.Humanize("2 000 222,22"));
            Console.WriteLine(ch.Humanize("222 222,22"));
            Console.WriteLine(ch.Humanize("2 222"));
            Console.WriteLine(ch.Humanize("222 222"));
            Console.WriteLine(ch.Humanize("0,22"));
            Console.WriteLine(ch.Humanize("1,01"));
            Console.WriteLine(ch.Humanize("1"));
            Console.WriteLine(ch.Humanize("0,01"));
            Console.WriteLine(ch.Humanize("1,00"));

            try
            {
                Console.WriteLine(ch.Humanize("44442 000 000,22"));
            }
            catch(Exception exc)
            {
                Console.WriteLine("Host Exception: {0}", exc.Message);
            }

            Console.ReadKey();
        }
    }
}



