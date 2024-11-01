﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using MultiPayCalc.Model;

namespace MultiPayCalc.ViewModel;

public class MultiPayCalcViewModel : INotifyPropertyChanged
{
    public MultiPayCalcViewModel()
    {
        AddButton = new relayCommand(execute => addUser());
        ClearButton = new relayCommand(execute => Clear());
        CalcButton = new relayCommand(execute => Calc());
        payList = new ObservableCollection<Person>();
    }



    private void addUser()
    {
        PayList.Add(new Person { name = Name, paid = Paid });
        Clean();
    }
    private void Clear()
    {
        PayList.Clear();
    }
    private void Clean()
    {
        Name = "";
        Paid = 0;
    }
    private void Calc()
    {
        if(PayList.Count <= 0)
        {
            MessageBox.Show("Enter payments");
            return;
        }
        var calcResult = CalculationOfPayments.calc(PayList.ToList());
        string res = "";
        foreach (var user in calcResult)
        {
            res += user + '\n';
        }
        if(res == "")
            MessageBox.Show("No debts");
        else
            MessageBox.Show(res);
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    private void onPropertyChanged(string propertyName)
    {
        if (propertyName != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }


    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; onPropertyChanged("Name"); }
    }
    private double paid;
    public double Paid
    {
        get { return paid; }
        set { paid = value; onPropertyChanged("Paid"); }
    }

    private ObservableCollection<Person> payList;
    public ObservableCollection<Person> PayList
    {
        get { return payList; }
        set { payList = value; onPropertyChanged("PayList"); }
    }


    //RelayCommands
    public relayCommand AddButton { set; get; }
    public relayCommand ClearButton { set; get; }
    public relayCommand CalcButton { set; get; }


}
