﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book
{
    class Program
    {

        static void WriteMenu()
        {

            Console.WriteLine("\n--------------------------------------------\n");
            Console.WriteLine("(1) Dodavanje novog upisa");
            Console.WriteLine("(2) Promjena imena, adrese ili broja");
            Console.WriteLine("(3) Brisanje upisa");
            Console.WriteLine("(4) Pretraga po broju");
            Console.WriteLine("(5) Pretraga po imenu");
            Console.WriteLine("(6) Izlaz iz programa");

        }

        static void Continue()
        {

            Console.Write("\nStisnite 'ENTER' kako bi ste nastavili");
            Console.ReadLine();

        }

        // Phone number

        static string FormatPhoneNumber(string inputedPhoneNumber)
        {

            var phoneNumberAsString = "";
            foreach (var character in inputedPhoneNumber)
                if (character != ' ')
                    phoneNumberAsString += character;

            return phoneNumberAsString;

        }

        static Int64 CheckIfPhoneNumberValid(string formatedPhoneNumber)
        {

            var phoneNumberValid = false;
            var repeatPhoneNumber = "";

            while (phoneNumberValid == false) {

                if (formatedPhoneNumber.Length != 10) { 

                    Console.Write("Broj mobitela je neispravan, ponovite unos (999 9999 999): ");
                    repeatPhoneNumber = Console.ReadLine().Trim();

                    formatedPhoneNumber = FormatPhoneNumber(repeatPhoneNumber);

                }

                else
                    phoneNumberValid = true;

            }

            Int64 phoneNumber = Int64.Parse(formatedPhoneNumber);
            return phoneNumber;

        }

        // Case 1

        static void AddNewContact(Dictionary<Tuple<string, string>, Tuple<string, Int64>> AddressBook)
        {
           
            Console.Write("Unesite ime: ");
            var firstName = Console.ReadLine().Trim();

            Console.Write("Unesite prezime: ");
            var lastName = Console.ReadLine().Trim();

            Console.Write("Unesite adresu: ");
            var address = Console.ReadLine().Trim();

            Console.Write("Unesite broj mobitela: ");
            var inputedPhoneNumber = Console.ReadLine().Trim();
            var formatedPhoneNumber = FormatPhoneNumber(inputedPhoneNumber);
            var phoneNumber = CheckIfPhoneNumberValid(formatedPhoneNumber);
            var taken = false;

            foreach (var pair in AddressBook)
            {
                if (phoneNumber == pair.Value.Item2)
                    taken = true;
            }

            while (taken == true) 
            {

                taken = false;
                Console.Write("Broj mobitela je već iskorišten, ponovite unos: ");
                inputedPhoneNumber = Console.ReadLine().Trim();
                formatedPhoneNumber = FormatPhoneNumber(inputedPhoneNumber);
                phoneNumber = CheckIfPhoneNumberValid(formatedPhoneNumber);

                foreach (var pair in AddressBook)
                {
                    if (phoneNumber == pair.Value.Item2)
                        taken = true;
                }

            } 

            AddressBook.Add(new Tuple<string, string>(firstName, lastName), new Tuple<string, Int64>(address, phoneNumber));

        }

        // Case 2

        static void ChangeContactInfo(Dictionary<Tuple<string, string>, Tuple<string, Int64>> AddressBook)
        {

                Console.Write("Unesite broj mobitela osobe cije podatke zelite izmjeniti: ");
                var inputNumber = Console.ReadLine().Trim();
                var lookForNumber = Int64.Parse(FormatPhoneNumber(inputNumber));
                var CurrentUser = new Tuple<string, string, string, Int64>(null, null, null, 0);

                foreach (var pair in AddressBook)
                {
                    if (pair.Value.Item2 == lookForNumber)
                    {

                        CurrentUser = new Tuple<string, string, string, Int64>(pair.Key.Item1, pair.Key.Item2, pair.Value.Item1, pair.Value.Item2);
                        break;

                    }
                }

               var CurrentKey = new Tuple<string, string>(CurrentUser.Item1, CurrentUser.Item2);

                if (AddressBook.ContainsKey(CurrentKey))
                {
                    Console.WriteLine("Ako želite sačuvati stare podatke unesite '*' (npr. 'Unesite novo ime: *')");
                    Console.Write("Unesite novo ime: ");
                    var firstName = Console.ReadLine().Trim();

                    Console.Write("Unesite novo prezime: ");
                    var lastName = Console.ReadLine().Trim();

                    Console.Write("Unesite novu adresu: ");
                    var address = Console.ReadLine().Trim();

                    Console.Write("Unesite novi broj mobitela: ");
                    var inputedPhoneNumber = Console.ReadLine().Trim();
                    Int64 phoneNumber = 0;
                    if (inputedPhoneNumber != "*")
                    {
                        var formatedPhoneNumber = FormatPhoneNumber(inputedPhoneNumber);
                        phoneNumber = CheckIfPhoneNumberValid(formatedPhoneNumber);
                    }

                    if (firstName == "*") firstName = CurrentUser.Item1;
                    if (lastName == "*") lastName = CurrentUser.Item2;
                    if (address == "*") address = CurrentUser.Item3;
                    if (inputedPhoneNumber == "*") phoneNumber = CurrentUser.Item4;

                    AddressBook.Remove(CurrentKey);
                    AddressBook.Add(new Tuple<string, string>(firstName, lastName), new Tuple<string, Int64>(address, phoneNumber));
                }
                else Console.WriteLine("Osoba nije pronađena!");           
        }
        
        // Case 3

        static void RemoveInput(Dictionary<Tuple<string, string>, Tuple<string, Int64>> AddressBook)
        {
            Console.Write("Unesite broj mobitela osobe cije podatke zelite izbrisati: ");
            var inputNumber = Console.ReadLine().Trim();
            var lookForNumber = Int64.Parse(FormatPhoneNumber(inputNumber));
            var CurrentUser = new Tuple<string, string, string, Int64>(null, null, null, 0);

            foreach (var pair in AddressBook)
            {
                if (pair.Value.Item2 == lookForNumber)
                {

                    CurrentUser = new Tuple<string, string, string, Int64>(pair.Key.Item1, pair.Key.Item2, pair.Value.Item1, pair.Value.Item2);
                    break;

                }
            }

            var CurrentKey = new Tuple<string, string>(CurrentUser.Item1, CurrentUser.Item2);
            if (AddressBook.ContainsKey(new Tuple<string, string>(CurrentUser.Item1, CurrentUser.Item2)))
            {

                AddressBook.Remove(new Tuple<string, string>(CurrentUser.Item1, CurrentUser.Item2));

            }
            else Console.WriteLine("Osoba nije pronađena!");
            Continue();
        }

        // Case 4

        static void LookForNumber(Dictionary<Tuple<string, string>, Tuple<string, Int64>> AddressBook)
        {
            bool ifPersonExists = false;
            Console.Write("Unesite broj tražene osobe: ");
            var inputedPhoneNumber = Console.ReadLine();
            var phoneNumber = Int64.Parse(FormatPhoneNumber(inputedPhoneNumber));
            

            foreach (var pair in AddressBook)
            {
                if (pair.Value.Item2 == phoneNumber)
                {
                    Console.WriteLine($"\nIme: {pair.Key.Item1}\nPrezime: {pair.Key.Item2}\nAdresa: {pair.Value.Item1}\nBroj mobitela: {pair.Value.Item2}");
                    ifPersonExists = true;
                }
            }

            if (ifPersonExists == false)
            {
                Console.WriteLine("\nOsoba nije pronađena!");
            }
        }

        // Case 5

        static void LookForName(Dictionary<Tuple<string, string>, Tuple<string, Int64>> AddressBook)
        {
            var ifPersonExists = false;
            Console.Write("Unesite ime tražene osobe: ");
            var firstName = Console.ReadLine();
            Console.Write("Unesite prezime tražene osobe: ");
            var lastName = Console.ReadLine();

            var tempList = new List<Tuple<string, string>>();

            foreach (var pair in AddressBook)
            {
                if (pair.Key.Item1.Contains(firstName) && pair.Key.Item2.Contains(lastName))
                {
                    tempList.Add(new Tuple<string, string>(pair.Key.Item2, pair.Key.Item1));
                    ifPersonExists = true;
                }
            }

            tempList.Sort();

            foreach (var tempPair in tempList)
            {
                foreach(var pair in AddressBook)
                {
                    if (tempPair.Item1 == pair.Key.Item2 && tempPair.Item2 == pair.Key.Item1)
                    {
                        Console.WriteLine($"\nIme: {pair.Key.Item1}\nPrezime: {pair.Key.Item2}\nAdresa: {pair.Value.Item1}\nBroj mobitela: {pair.Value.Item2}");
                    }
                }
            }

            if (ifPersonExists == false)
            {
                Console.WriteLine("\nOsoba nije pronađena!");
            }
        }

        static void Main(string[] args)
        {
            var option = "";

            Console.Write("Unesite ime: ");
            var firstName = Console.ReadLine().Trim();

            Console.Write("Unesite prezime: ");
            var lastName = Console.ReadLine().Trim();

            Console.Write("Unesite adresu: ");
            var address = Console.ReadLine().Trim();

            Console.Write("Unesite broj mobitela: ");
            var inputedPhoneNumber = Console.ReadLine().Trim();
            var formatedPhoneNumber = FormatPhoneNumber(inputedPhoneNumber);
            Int64 phoneNumber = CheckIfPhoneNumberValid(formatedPhoneNumber);

            var AddressBook = new Dictionary<Tuple<string, string>, Tuple<string, Int64>>();
            AddressBook.Add(new Tuple<string, string>(firstName, lastName), new Tuple<string, Int64>(address, phoneNumber));

            do
            {
                WriteMenu();

                Console.Write("\nOdaberite opciju (unos broja): ");
                option = Console.ReadLine().Trim();

                Console.WriteLine("\n--------------------------------------------\n");

                switch (option)
                {
                    case "1":

                        Console.Write("Jeste li sigurni da želite dodati novi upis? (1) ");
                        option = Console.ReadLine().Trim();

                        if (option == "1")
                        {
                            AddNewContact(AddressBook);
                            Continue();
                        }
                        else option = "0";
                        break;

                    case "2":

                        Console.Write("Jeste li sigurni da želite promjeniti ime, adresu ili broj? (2) ");
                        option = Console.ReadLine().Trim();

                        if (option == "2")
                        {

                            ChangeContactInfo(AddressBook);
                            Continue();

                        }

                        else option = "1";
                        break;

                    case "3":

                        Console.Write("Jeste li sigurni da želite izbrisati upis? (3) ");
                        option = Console.ReadLine().Trim();

                        if (option == "3")
                        {
                            RemoveInput(AddressBook);
                        }

                        else option = "1";
                        break;

                    case "4":

                        LookForNumber(AddressBook);
                        Continue();
                        break;

                    case "5":

                        LookForName(AddressBook);
                        Continue();
                        break;


                    case "6":

                        break;

                    default:

                        Console.WriteLine("Pogrešan unos");
                        break;

                }
            }
            while (option != "6");
        }
    }
}