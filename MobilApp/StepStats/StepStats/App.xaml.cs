using StepStats.Model;

namespace StepStats
{
    public partial class App : Application
    {

        #region Fields

        private StepStatModel _stepStatModel;

        #endregion

        #region Constructors

        public App()
        {
            InitializeComponent();

            _stepStatModel = new StepStatModel();

            MainPage = new MainPage(_stepStatModel);
        }

#endregion

    }
}