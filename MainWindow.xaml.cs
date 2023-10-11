using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp1;
public partial class MainWindow : Window
{
    private ObservableCollection<User> users = new ObservableCollection<User>();
    private ObservableCollection<Book> books = new ObservableCollection<Book>();
    private ObservableCollection<Loan> loans = new ObservableCollection<Loan>();

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Year { get; set; }
        public int Count { get; set; }
    }

    public class Loan
    {
        public User User { get; set; }
        public Book Book { get; set; }
    }

    public MainWindow()
    {
        InitializeComponent();
        userListView.ItemsSource = users;
        bookListView.ItemsSource = books;
        issuedBooksListView.ItemsSource = loans;

        users.Add(new User { Id = 1, Name = "Карим", Family = "Агано" });
        users.Add(new User { Id = 2, Name = "Иван", Family = "Костенко" });
        users.Add(new User { Id = 3, Name = "Ксения", Family = "Иванова" });
        users.Add(new User { Id = 4, Name = "Валерия", Family = "Романова" });

        books.Add(new Book { Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Year = DateTime.Now, Count = 5 });
        books.Add(new Book { Title = "Преступление и наказание", Author = "Федор Достоевский", Year = DateTime.Now, Count = 4 });
        books.Add(new Book { Title = "Маленький принц", Author = "Антуан де Сент-Экзюпери", Year = DateTime.Now, Count = 3 });
        books.Add(new Book { Title = "Собачье сердце", Author = "Михаил Булгаков", Year = DateTime.Now, Count = 6 });
    }

    private void IssueBookButton_Click(object sender, RoutedEventArgs e)
    {
        User selectedUser = (User)userListView.SelectedItem;
        Book selectedBook = (Book)bookListView.SelectedItem;

        if (selectedUser != null && selectedBook != null && selectedBook.Count > 0)
        {
            Loan newLoan = new Loan { User = selectedUser, Book = selectedBook };
            loans.Add(newLoan);
            selectedBook.Count--;
            CollectionViewSource.GetDefaultView(books).Refresh();
        }
        else
        {
            MessageBox.Show("Выберите пользователя и книгу для выдачи.");
        }
    }

    private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
    {
        Loan selectedLoan = (Loan)issuedBooksListView.SelectedItem;

        if (selectedLoan != null)
        {
            selectedLoan.Book.Count++;
            loans.Remove(selectedLoan);
            CollectionViewSource.GetDefaultView(books).Refresh();
        }
        else
        {
            MessageBox.Show("Выберите книгу для возврата");
        }
    }
}
