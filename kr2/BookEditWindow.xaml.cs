using kr2.Context;
using kr2.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace kr2
{
    public partial class BookEditWindow : Window
    {
        private Book _book;
        private AppDbContext _context;

        public BookEditWindow(Book book, AppDbContext context)
        {
            InitializeComponent();
            _book = book;
            _context = context;

            StatusComboBox.ItemsSource = Enum.GetValues(typeof(BookStatus));

            ArticleNumberTextBox.Text = _book.ArticleNumber;
            TitleTextBox.Text = _book.Title;
            GenreTextBox.Text = _book.Genre;
            DescriptionTextBox.Text = _book.Description;
            ReleaseDatePicker.SelectedDate = _book.ReleaseDate;
            StatusComboBox.SelectedItem = _book.Status;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _book.ArticleNumber = ArticleNumberTextBox.Text;
            _book.Title = TitleTextBox.Text;
            _book.Genre = GenreTextBox.Text;
            _book.Description = DescriptionTextBox.Text;
            _book.ReleaseDate = ReleaseDatePicker.SelectedDate ?? DateTime.Now;
            _book.Status = (BookStatus)StatusComboBox.SelectedItem;

            if (_book.Id == 0)
            {
                _context.Books.Add(_book);
            }
            else
            {
                _context.Books.Update(_book);
            }

            _context.SaveChanges();
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}