namespace QuickNews;

public partial class SearchResultsPage : ContentPage
{
	public SearchResultsPage(DTO.FeedlySearchResult result)
	{
		InitializeComponent();
        _titleLabel.Text = result.Title;
	}
}