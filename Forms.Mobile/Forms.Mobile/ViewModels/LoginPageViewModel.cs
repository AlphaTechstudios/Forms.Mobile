using Forms.Mobile.Models;
using Forms.Mobile.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Text.RegularExpressions;

namespace Forms.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IAuthenticationService authenticationService;

        private string login;
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }
        private string pwd;
        public string Pwd
        {
            get => pwd;
            set => SetProperty(ref pwd, value);
        }

        private bool isLoginInValid;

        public bool IsLoginInValid
        {
            get => isLoginInValid;
            set => SetProperty(ref isLoginInValid, value);
        }

        private bool isPwdInValid;

        public bool IsPwdInValid
        {
            get => isPwdInValid;
            set => SetProperty(ref isPwdInValid, value);
        }

        private bool isLoginErrorVisible;

        public bool IsLoginErrorVisible
        {
            get => isLoginErrorVisible;
            set => SetProperty(ref isLoginErrorVisible, value);
        }

        public DelegateCommand SignInCommand { get; private set; }
        public DelegateCommand ValidateLoginCommand { get; private set; }
        public DelegateCommand ValidatePwdCommand { get; private set; }

        public LoginPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
            : base(navigationService)
        {
            SignInCommand = new DelegateCommand(SignIp, CanExecuteSignIn);
            ValidateLoginCommand = new DelegateCommand(ValidateLogin, () => Login != null);
            ValidatePwdCommand = new DelegateCommand(ValidatePwd, () => Login != null);
            this.authenticationService = authenticationService;
        }

        private bool CanExecuteSignIn()
        {
            if (Login == null || Pwd == null)
            {
                return false;
            }

            if (!IsLoginInValid && !IsPwdInValid)
            {
                return true;
            }
            return false;
        }

        private void ValidateLogin()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Login);

            IsLoginInValid = !match.Success;
            SignInCommand.RaiseCanExecuteChanged();
        }

        private void ValidatePwd()
        {
            IsPwdInValid = string.IsNullOrEmpty(Pwd);
            SignInCommand.RaiseCanExecuteChanged();
        }

        private async void SignIp()
        {
            // Call Service for login.
            var user = await authenticationService.Login(new LoginModel { Email = Login, Password = Pwd });
            if (user != null)
            {

                await NavigationService.NavigateAsync("MainPage");
            }
            else
            {
                // Show login error;
                IsLoginErrorVisible = true;
            }
        }
    }
}
