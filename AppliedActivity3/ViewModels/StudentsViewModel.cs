using AppliedActivity3.Services;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppliedActivity3.ViewModels
{
    internal class StudentsViewModel
    {
        public IDataStore SqliteDataStore => DependencyService.Get<IDataStore>();
        public ObservableRangeCollection<StudentViewModel> Students { get; set; }
        public ICommand PageAppearingCommand { get; set; }
        public ICommand SelectCommand { get; }
        public ICommand AddCommand { get; }

        public StudentsViewModel()
        {
            Students = new ObservableRangeCollection<StudentViewModel>();
            PageAppearingCommand = new AsyncRelayCommand(PageAppearing);
            SelectCommand = new AsyncRelayCommand<StudentViewModel>(SelectAsync);
            AddCommand = new AsyncRelayCommand(AddAsync);
        }

        private async Task SelectAsync(StudentViewModel student)
        {
            if (student != null)
                await Shell.Current.GoToAsync($"{nameof(Views.StudentPage)}?id={student.Id}");
        }

        private async Task AddAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.StudentPage));
        }

        public async Task Refresh()
        {
            var students = await SqliteDataStore.GetStudentsAsync();
            Students.Clear();
            Students.AddRange(new List<StudentViewModel>(students.Select(student => new StudentViewModel(student))));
        }

        public async Task PageAppearing()
        {
            await Refresh();
        }
    }
}
