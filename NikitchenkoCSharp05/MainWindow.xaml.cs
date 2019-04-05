using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NikitchenkoCSharp05
{
    public partial class MainWindow
    {
        public MainWindow()
        {
           
            InitializeComponent();
            Kill.IsEnabled = false;
            Open.IsEnabled = false;
        }
      

        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (DataGrid)sender;

            if (listBox.SelectedItems.Count > 0)
            {
                var viewModel = (ViewModel)DataContext;
                viewModel.SelectedProcess = ((ProcessListItem)listBox.SelectedItems[0]).Process;
                Kill.IsEnabled = true;
                Open.IsEnabled = true;
            }
            else if (listBox.SelectedItems.Count == 0)
            {
                Kill.IsEnabled = false;
                Open.IsEnabled = false;
            }
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.UpdateProcesses(sender, e);
        }



        private void KillProcess_OnClick(object sender, RoutedEventArgs e)
        {
          
            var viewModel = (ViewModel)DataContext;
            viewModel.KillSelectedProcess();
        }

        private void OpenFolder_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.OpenFolderSelectedProcess();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var processListItem = (ProcessListItem)checkBox.DataContext;
            processListItem.KeepAlive = true;
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var processListItem = (ProcessListItem)checkBox.DataContext;
            processListItem.KeepAlive = false;
        }
    }
}
