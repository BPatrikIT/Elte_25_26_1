using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace StepStats.Model
{
    public class StepStatModel : IDisposable
    {

        #region Fields

        private bool _isDisposed;

        private Location? _location;
        private double _distanceToMeasure = 0.0;
        private Timer? _gpsEmulatorTimer;

        private IPedometer _pedometer;
        private int _steps;
        private int _stepsToMeasure;
        
        private double _measuredStepDistanceSum = 0.0;
        private int _distanceMeasureCount;

        #endregion

        #region Properties

        public Location? Location => _location;
        public int Steps => _steps;

        public double AverageStepDistance => _distanceMeasureCount > 0 ? _measuredStepDistanceSum / _distanceMeasureCount : 0.0;

        #endregion

        #region Events

        public event EventHandler? LocationChanged;
        public event EventHandler? StepsChanged;
        public event EventHandler? StepStatsChanged;

        #endregion

        #region Constructors

        public StepStatModel(IPedometer pedometer)
        {
            _pedometer = pedometer;
        }
        ~StepStatModel()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }


        #endregion

        #region Private Methods

        private void Geolocation_LocationChanged(object? sender, GeolocationLocationChangedEventArgs e)
        {
            Location newLocation = e.Location;
            //if (newLocation.Accuracy <= 25)
            {
                if (_location != null && _location != newLocation)
                    _distanceToMeasure += _location.CalculateDistance(newLocation, DistanceUnits.Kilometers) * 1000.0;

                _location = newLocation;
                LocationChanged?.Invoke(this, EventArgs.Empty);

                MeasureSteps();
            }
        }

        private void _pedometer_StepChanged(object? sender, StepsEventArgs e)
        {
            _steps = e.Steps;
            StepChanged?.Invoke(this, EventArgs.Empty);

        }
        private void MeasureSteps()
        {
            if (_stepsToMeasure > 0 && _distanceToMeasure > 0.0)
            {
                _measuredStepDistanceSum += _distanceToMeasure / _stepsToMeasure;
                ++_distanceMeasureCount;
                StepStatsChanged?.Invoke(this, EventArgs.Empty);

                _distanceToMeasure = 0.0;
                _stepsToMeasure = 0;
            }
        }

        #endregion

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                StopTracking();

                if (disposing)
                {
                    // dispose managed state (managed objects)
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null

                _isDisposed = true;
            }
        }

        #endregion

        #region Public Methods

        public async Task StartTracking()
        {
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            Geolocation.LocationChanged += Geolocation_LocationChanged;
            await Geolocation.StartListeningForegroundAsync(new GeolocationListeningRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(2)));

            //GPS emulation with timer
            _gpsEmulatorTimer = new Timer((s) =>
            {
                Location newLocation = _location != null ? new Location(_location) : new Location(47.47224430185252, 19.061630000531096);
                newLocation.Latitude += 0.000008999; //~1 meter, very inaccurate
                Geolocation_LocationChanged(this, new GeolocationLocationChangedEventArgs(newLocation));
            }, null, 0, 2000);
            _pedometer.StepsChanged += Pedometer_StepsChanged;
            await _pedometer.StartTrackingAsync();
        }

        public void StopTracking()
        {
            Geolocation.StopListeningForeground();
            Geolocation.LocationChanged -= Geolocation_LocationChanged;

            if (_gpsEmulatorTimer != null)
            {
                _gpsEmulatorTimer.Dispose();
                _gpsEmulatorTimer = null;
            }

            _pedometer.StopTracking();
            _pedometer.StepsChanged -= Pedometer_StepsChanged;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
