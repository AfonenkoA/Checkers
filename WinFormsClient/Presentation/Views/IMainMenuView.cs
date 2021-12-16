﻿using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IMainMenuView :IView
{
    event Action ShowProfile;
    event Action ShowShop;
    event Action ShowCollection;
    //event Action LogOut;
}