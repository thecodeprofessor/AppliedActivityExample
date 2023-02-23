using AppliedActivity3.Services;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppliedActivity3.ViewModels
{
    internal class CoursesViewModel
    {
        public IDataStore SqliteDataStore => DependencyService.Get<IDataStore>();
        public ObservableRangeCollection<CourseViewModel> Courses { get; set; }
        public ICommand PageAppearingCommand { get; set; }
        public ICommand SelectCommand { get; }
        public ICommand AddCommand { get; }

        public CoursesViewModel()
        {
            Courses = new ObservableRangeCollection<CourseViewModel>();
            PageAppearingCommand = new AsyncRelayCommand(PageAppearing);
            SelectCommand = new AsyncRelayCommand<CourseViewModel>(SelectAsync);
            AddCommand = new AsyncRelayCommand(AddAsync);
        }

        private async Task SelectAsync(CourseViewModel course)
        {
            if (course != null)
                await Shell.Current.GoToAsync($"{nameof(Views.CoursePage)}?id={course.Id}");
        }

        private async Task AddAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.CoursePage));
        }

        public async Task Refresh()
        {
            var courses = await SqliteDataStore.GetCoursesAsync();
            Courses.Clear();
            Courses.AddRange(new List<CourseViewModel>(courses.Select(course => new CourseViewModel(course))));
        }

        public async Task PageAppearing()
        {
            await Refresh();
        }
    }
}
