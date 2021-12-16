﻿using Api.Interface;
using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

public class LoginPresenter : BasePresenter<ILoginView>
{
       
    private readonly IAsyncUserApi _userApi;

    public LoginPresenter(IApplicationController controller,ILoginView view, IAsyncUserApi userApi): base(controller, view)
            
    {
            
        _userApi = userApi;
            

        View.LogIn += () => Authenticate(View.Login,View.Password);
    }
        

    private async void Authenticate(string login, string password)
    {
        var credential = new Credential {Login = login, Password = password};
        if (!await _userApi.Authenticate(credential))
        {
            View.ShowError("Invalid login or password");
        }
        else
        {
            Controller.Run<MainMenuPresenter, Credential>(credential);
            View.Close();
        }
    }
}