using System.Windows;
using MultiPayCalc.ViewModel;

namespace MultiPayCalc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    MultiPayCalcViewModel viewModel;
    public MainWindow()
    {
        InitializeComponent();
        viewModel=new MultiPayCalcViewModel();
        this.DataContext = viewModel;
    }
}
