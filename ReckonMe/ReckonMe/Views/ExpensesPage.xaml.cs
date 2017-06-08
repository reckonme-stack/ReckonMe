﻿
using ReckonMe.Models;
using ReckonMe.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace ReckonMe.Views
{
    public partial class ExpensesPage : ContentPage
    {
        private readonly ExpensesViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ExpensesPage()
        {
            InitializeComponent();
        }

        public ExpensesPage(ExpensesViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        private async void OnExpenseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var expense = args.SelectedItem as Expense;
            if (expense == null)
                return;

            await Navigation.PushAsync(new ExpenseDetailedPage(new ExpenseViewModel(expense)));
            // Manually deselect item
            ExpensesListView.SelectedItem = null;
        }

        private async void AddExpense_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NewExpensePage());
        }

        // Not working
        private async void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopToRootAsync();
            try
            {
                await _viewModel.DataStore.DeleteItemAsync(_viewModel.Wallet);
            }
            catch(Exception ex)
            {
               await DisplayAlert("Exception", ex.Message, "OK");
                Debug.WriteLine(ex.Message);
            }
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Expenses.Count == 0)
                _viewModel.LoadExpensesCommand.Execute(null);
        }
    }
}
