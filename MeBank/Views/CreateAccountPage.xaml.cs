﻿using System.ComponentModel;
using MeBank.ViewModels;

namespace MeBank.Views
{
    [DesignTimeVisible(false)]
    public partial class CreateAccountPage
    {
        public CreateAccountPage()
        {
            InitializeComponent();
            ((CreateAccountViewModel) BindingContext).NavigationContext = Navigation;
        }
    }
}