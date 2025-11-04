using GyorsHir.Model;
using GyorsHir.Model.RSS;
using Newtonsoft.Json;

namespace GyorsHir;

public partial class PortalNewsPage : ContentPage
{

    #region Fields

    private GyorsHirModel _model;

    #endregion

    #region Constructors

    public PortalNewsPage(GyorsHirModel model)
    {
        InitializeComponent();

        _model = model;

        if (_model.NewsChannel != null && _model.NewsChannel.Items != null)
        {
            _titleLabel.Text = _model.NewsChannel.Title;

            foreach (RSSItem item in _model.NewsChannel.Items)
            {
                _newsList.Children.Add(new Button()
                {
                    Text = item.Title,
                });
            }
        }
    }

    #endregion

}
