using AppliedActivity3.Services;
using AppliedActivity3.Models;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using MvvmHelpers;

namespace AppliedActivity3.ViewModels
{
    internal class StudentViewModel : ObservableObject, IQueryAttributable
    {
        private Student _student;

        public int Id { get => _student.Id; set => _student.Id = value; }

        public string Number { get => _student.Number; set => _student.Number = value; }
        public string FirstName { get => _student.FirstName; set => _student.FirstName = value; }
        public string LastName { get => _student.LastName; set => _student.LastName = value; }

        public IDataStore SqliteDataStore => DependencyService.Get<IDataStore>();
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CapturePhotoCommand { get; set; }

        public ImageSource Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                OnPropertyChanged(nameof(Photo));
            }
        }

        private ImageSource photo;


        public StudentViewModel ()
        {
            _student = new Student();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
            CapturePhotoCommand = new AsyncRelayCommand(CapturePhoto);
        }

        public StudentViewModel(Student student)
        {
            _student = student;
        }

        async void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (!query.ContainsKey("id"))
                return;

            var id = Convert.ToInt32(query["id"].ToString());
            _student = await SqliteDataStore.GetStudentAsync(id);
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Number));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
        }


        public async Task CapturePhoto()
        {
            //FileResult photo = await MediaPicker.PickPhotoAsync();
            FileResult photo = await MediaPicker.CapturePhotoAsync();

            Stream stream = await photo.OpenReadAsync();

            MemoryStream memory = new MemoryStream();
            stream.CopyTo(memory);

            Photo = ImageSource.FromStream(() => new MemoryStream(memory.ToArray()));
        }

        public async Task Save()
        {
            await SqliteDataStore.SaveStudentAsync(_student);

            await Shell.Current.GoToAsync($"..");
        }

        public async Task Delete()
        {
            await SqliteDataStore.DeleteStudentAsync(_student);

            await Shell.Current.GoToAsync($"..");
        }

    }
}
