﻿using MauiBankingExercise.Views;

namespace MauiBankingExercise
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("customerSelectionview", typeof(CustomerSelectionView));
        }

        
    }
}
