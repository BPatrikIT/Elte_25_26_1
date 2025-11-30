using StepStats.Model;

namespace StepStats
{
    public partial class MainPage : ContentPage
    {

        #region Private Methods

        private readonly StepStatModel _model;

        #endregion

        #region Constructors

        public MainPage(StepStatModel model)
        {
            InitializeComponent();
            _model = model;
        }

        #endregion

        #region Model Event Handlers

        private void _model_LocationChanged(object sender, EventArgs e)
            => Dispatcher.Dispatch(() => _locationLabel.Text = _model.Location.ToString());
        private void _model_StepsChanged(object sender, EventArgs e)
            => Dispatcher.Dispatch(() => _stepsLabel.Text = $"Steps: {_model.Steps}");
        private void _model_StepStatsChanged(object sender, EventArgs e)
            => Dispatcher.Dispatch(() => _avgDistanceLabel.Text = $"Avg. Step Distance: {_model.AverageStepDistance.ToString("f2")}m");

        #endregion

        #region Protected Methods

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _model.LocationChanged += _model_LocationChanged;
            _model.StepsChanged += _model_StepsChanged;
            _model.StepStatsChanged += _model_StepStatsChanged;
            await _model.StartTracking();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _model.LocationChanged -= _model_LocationChanged;
            _model.StepsChanged -= _model_StepsChanged;
            _model.StepStatsChanged -= _model_StepStatsChanged;
            _model.StopTracking();
        }

        #endregion

    }
}