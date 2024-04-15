using System.Collections.Generic;
using System.Linq;

namespace MultiPayCalc.Model;

public static class CalculationOfPayments
{
    public static List<string> calc(List<Person> people)
    {
        List<string> result = new List<string>();
        //נחשב כמה כל אחד צריך לשלם
        double total = people.Average(person => person.paid);
        foreach (Person person in people)
        {
            person.toReceive = person.paid - total;
        }
        //נחלק את הרשימה לכאלה שצריכים לשלם וכאלה שצריכים לקבל כסף
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
        //נחשב מי צריך לשלם למי
        int sum = 0;
        string message = "";
        for (int i = 0, j = 0; i < needToPay.Count && j < getMoney.Count;)
        {
            if (needToPay[i].toReceive + getMoney[j].toReceive < 0)
            {
                message = $"{needToPay[i].name} -> {getMoney[j].name} : {getMoney[j].toReceive}";
                needToPay[i].toReceive += getMoney[j].toReceive;
                j++;
            }
            else if (needToPay[i].toReceive + getMoney[j].toReceive >= 0)
            {
                message = $"{needToPay[i].name} -> {getMoney[j].name} : {-1 * needToPay[i].toReceive}";
                needToPay[i].toReceive -= needToPay[i].toReceive; //0
                i++;//האדם סיים לשלם ונתקדם לאדם הבא
            }
            result.Add(message);
        }
        return result;
    }

}
