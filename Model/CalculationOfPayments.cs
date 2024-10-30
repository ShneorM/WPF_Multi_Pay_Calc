using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPayCalc.Model;

public static class CalculationOfPayments
{
    public static List<string> calc(List<Person> people)
    {
        if (people == null || people.Count == 0)
            return new List<string>();
        List<string> result = new List<string>();

        // Calculated how much each should pay
        double total = people.Average(person => person.paid);
        foreach (Person person in people)
        {
            person.toReceive = Math.Round(person.paid - total, 2); // Round to 2 decimal places
        }

        // Divide the list into those who need to pay and those who need to receive money
        List<Person> needToPay = new List<Person>();
        foreach (Person person in people)
        {
            if (person.toReceive <= 0)
                needToPay.Add(person);
        }

        List<Person> getMoney = new List<Person>();
        foreach (Person person in people)
        {
            if (person.toReceive > 0)
                getMoney.Add(person);
        }

        // Calculate who should pay and to whom
        string message = "";
        double payment;
        for (int i = 0, j = 0; i < needToPay.Count && j < getMoney.Count;)
        {
            payment = Math.Min(Math.Abs(needToPay[i].toReceive), getMoney[j].toReceive);

            message = $"{needToPay[i].name} -> {getMoney[j].name} : {Math.Round(payment, 2)}";
            if(Math.Round(payment, 2) > 0)
                result.Add(message);

            // Adjust amounts and round to prevent floating-point precision issues
            needToPay[i].toReceive = Math.Round(needToPay[i].toReceive + payment, 2);
            getMoney[j].toReceive = Math.Round(getMoney[j].toReceive - payment, 2);

            // Move to next person if paid in full (or close enough to zero)
            if (Math.Abs(needToPay[i].toReceive) < 0.01) i++;
            if (Math.Abs(getMoney[j].toReceive) < 0.01) j++;
        }

        return result;
    }


}
