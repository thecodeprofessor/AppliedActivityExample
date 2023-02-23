using AppliedActivity3.Services;
using AppliedActivity3.Models;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using MvvmHelpers;

namespace AppliedActivity3.ViewModels
{
    internal class CourseViewModel : ObservableObject, IQueryAttributable
    {
        private Course _course;

        public int Id { get => _course.Id; set => _course.Id = value; }

        public string Code { get => _course.Code; set => _course.Code = value; }
        public string CRN { get => _course.CRN; set => _course.CRN = value; }
        public string Name { get => _course.Name; set => _course.Name = value; }

        public IDataStore SqliteDataStore => DependencyService.Get<IDataStore>();
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CourseViewModel ()
        {
            _course = new Course();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public CourseViewModel(Course course)
        {
            _course = course;
        }

        async void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (!query.ContainsKey("id"))
                return;

            var id = Convert.ToInt32(query["id"].ToString());
            _course = await SqliteDataStore.GetCourseAsync(id);
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Code));
            OnPropertyChanged(nameof(CRN));
            OnPropertyChanged(nameof(Name));
        }

        public async Task Save()
        {
            await SqliteDataStore.SaveCourseAsync(_course);

            await Shell.Current.GoToAsync($"..");
        }

        public async Task Delete()
        {
            await SqliteDataStore.DeleteCourseAsync(_course);

            await Shell.Current.GoToAsync($"..");
        }

    }
}
