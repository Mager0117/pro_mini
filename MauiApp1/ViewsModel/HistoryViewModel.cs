using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using MauiApp1.Services;
using System.Linq;
using MauiApp2.Model;

namespace MauiApp1.ViewModel
{
	public partial class HistoryViewModel : ObservableObject
	{
		private readonly HistoryService _historyService;

		[ObservableProperty]
		private ObservableCollection<EnrollmentRecord> _history = new();

		[ObservableProperty]
		private ObservableCollection<EnrollmentRecord> _filteredHistory = new();

		[ObservableProperty]
		private string _searchText;

		[ObservableProperty]
		private bool _isLoading;

		[ObservableProperty]
		private bool _isRefreshing;

		[ObservableProperty]
		private bool _isEmpty;

		[ObservableProperty]
		private string _userId;

		public HistoryViewModel(HistoryService historyService)
		{
			_historyService = historyService;
			LoadHistoryCommand = new RelayCommand(async () => await LoadHistoryAsync());
		}

		public ICommand LoadHistoryCommand { get; }

		partial void OnSearchTextChanged(string value)
		{
			FilterHistory();
		}

		private void FilterHistory()
		{
			if (string.IsNullOrWhiteSpace(SearchText))
			{
				FilteredHistory = new ObservableCollection<EnrollmentRecord>(History);
			}
			else
			{
				var filtered = History.Where(h =>
					h.CourseId.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
					h.Action.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
					.ToList();

				FilteredHistory = new ObservableCollection<EnrollmentRecord>(filtered);
			}

			IsEmpty = FilteredHistory.Count == 0;
		}

		public async Task LoadHistoryAsync()
		{
			if (string.IsNullOrEmpty(UserId))
			{
				IsEmpty = true;
				return;
			}

			IsLoading = true;
			try
			{
				var allRecords = await _historyService.LoadHistoryAsync();

				History = new ObservableCollection<EnrollmentRecord>(
					allRecords
						.Where(r => r.StudentId == UserId)
						.OrderByDescending(r => r.Timestamp)  // เรียงจากใหม่ไปเก่า
				);

				FilterHistory();
			}
			finally
			{
				IsLoading = false;
				IsRefreshing = false;
			}
		}
	}
}