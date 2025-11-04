namespace QuickNews
{
    public partial class App : Application
    {

        public NavigationPage NavPage { get; private set; }
        public App()
        {
            InitializeComponent();

            NavPage = new NavigationPage(new MainPage());
            MainPage = NavPage;
        }
    }
}
