using kr2.Context;
using kr2.DB;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace kr2
{
    public partial class MainWindow : Window
    {
        private AppDbContext _context;
        private User _currentUser;

        public MainWindow(User user, AppDbContext context)
        {
            InitializeComponent();
            _currentUser = user;
            _context = context;

            CurrentUserLabel.Content = $"Пользователь: {user.FullName}";
            LoadBooks();
        }

        private void LoadBooks()
        {
            BooksDataGrid.ItemsSource = _context.Books.Include(b => b.User).ToList();
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            var bookEditWindow = new BookEditWindow(new Book
            {
                ArticleNumber = Guid.NewGuid().ToString().Substring(0, 8),
                Title = "Новая книга",
                Genre = "Не указан",
                Description = "",
                ReleaseDate = DateTime.Now,
                Status = BookStatus.Available
            }, _context);

            if (bookEditWindow.ShowDialog() == true)
            {
                LoadBooks();
            }
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = (Book)BooksDataGrid.SelectedItem;
            if (selectedBook == null) return;

            var bookEditWindow = new BookEditWindow(selectedBook, _context);
            if (bookEditWindow.ShowDialog() == true)
            {
                LoadBooks();
            }
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            var book = (Book)BooksDataGrid.SelectedItem;
            if (book == null) return;

            if (MessageBox.Show($"Удалить книгу '{book.Title}'?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                LoadBooks();
            }
        }

        private void IssueBookButton_Click(object sender, RoutedEventArgs e)
        {
            var book = (Book)BooksDataGrid.SelectedItem;
            if (book == null) return;

            if (book.Status != BookStatus.Available)
            {
                MessageBox.Show("Книга не доступна для выдачи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            book.Status = BookStatus.Issued;
            book.UserId = _currentUser.Id;
            _context.SaveChanges();
            LoadBooks();
        }

        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {
            var book = (Book)BooksDataGrid.SelectedItem;
            if (book == null) return;

            if (book.Status != BookStatus.Issued || book.UserId != _currentUser.Id)
            {
                MessageBox.Show("Вы не можете вернуть эту книгу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            book.Status = BookStatus.Available;
            book.UserId = null;
            _context.SaveChanges();
            LoadBooks();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
    }
}