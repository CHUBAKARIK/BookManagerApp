using System;
using System.Collections.Generic; // ДОБАВЬ ЭТУ СТРОКУ
using System.Windows.Forms;
using BookManagerApp.Logic;
using BookManagerApp.Model;

namespace BookManagerApp.WinFormsUI
{
    public partial class MainForm : Form
    {
        private BookManager _bookManager;
        private GiverManager _giverManager;

        public MainForm()
        {
            InitializeComponent();

            // создаем менеджеры правильно
            _bookManager = new BookManager();
            _giverManager = new GiverManager(_bookManager);

            InitializeForm();
            lblField5.Visible = false;
            txtField5.Visible = false;
        }

        private void InitializeForm()
        {
            // Инициализация выпадающего списка
            cmbEntityType.Items.Add("Книги");
            cmbEntityType.Items.Add("Дарители");
            cmbEntityType.SelectedIndex = 0;
            cmbEntityType.SelectedIndexChanged += cmbEntityType_SelectedIndexChanged;

            // Настройка кнопок
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnBusiness1.Click += btnBusiness1_Click;
            btnBusiness2.Click += btnBusiness2_Click;

            // Настройка DataGridView
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            // Загрузка начальных данных
            LoadBooks();
        }

        private void cmbEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (cmbEntityType.SelectedItem.ToString() == "Книги")
            {
                LoadBooks();
                UpdateBookControls();
            }
            else
            {
                LoadGivers();
                UpdateGiverControls();
            }
        }

        private void UpdateBookControls()
        {
            lblField1.Text = "Название:";
            lblField2.Text = "Автор:";
            lblField3.Text = "Способность:";
            lblField4.Text = "Год:";

            // Показываем только первые 4 поля для книг
            lblField1.Visible = true;
            txtField1.Visible = true;
            lblField2.Visible = true;
            txtField2.Visible = true;
            lblField3.Visible = true;
            txtField3.Visible = true;
            lblField4.Visible = true;
            txtField4.Visible = true;

            // Скрываем поле команды (поле 5) для книг
            lblField5.Visible = false;
            txtField5.Visible = false;
        }

        private void UpdateGiverControls()
        {
            // для дарителей теперь другие поля
            lblField1.Text = "Имя:";
            lblField2.Text = "ID книги:"; // Теперь вводим ID книги, а не название
            lblField3.Text = "Очки Силы:";
            lblField4.Text = "Команда:";

            // Скрываем поле 5 для дарителей тоже
            lblField5.Visible = false;
            txtField5.Visible = false;

            // Показываем только 4 поля для дарителей
            lblField1.Visible = true;
            txtField1.Visible = true;
            lblField2.Visible = true;
            txtField2.Visible = true;
            lblField3.Visible = true;
            txtField3.Visible = true;
            lblField4.Visible = true;
            txtField4.Visible = true;
        }

        private void LoadBooks()
        {
            var books = _bookManager.GetAllBooks();
            dataGridView1.DataSource = books;
        }

        private void LoadGivers()
        {
            // Создаем список для отображения
            var giversDisplay = new List<GiverDisplay>();
            var givers = _giverManager.GetAllGivers();

            foreach (var giver in givers)
            {
                var book = _bookManager.GetBookById(giver.BookId);
                giversDisplay.Add(new GiverDisplay
                {
                    Id = giver.Id,
                    Name = giver.Name,
                    BookId = giver.BookId,
                    BookTitle = book?.Title ?? "Не найдена",
                    BookAbility = book?.AbilitiesOfTheBook ?? "Нет способности",
                    PowerCount = giver.YearOfCreation,
                    Team = giver.Team
                });
            }

            dataGridView1.DataSource = giversDisplay;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbEntityType.SelectedItem.ToString() == "Книги")
            {
                AddBook();
            }
            else
            {
                AddGiver();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (cmbEntityType.SelectedItem.ToString() == "Книги")
                {
                    UpdateBook();
                }
                else
                {
                    UpdateGiver();
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для обновления!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (cmbEntityType.SelectedItem.ToString() == "Книги")
                {
                    DeleteBook();
                }
                else
                {
                    DeleteGiver();
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления!");
            }
        }

        private void AddBook()
        {
            try
            {
                _bookManager.AddBook(txtField1.Text, txtField2.Text, txtField3.Text, int.Parse(txtField4.Text));
                LoadBooks();
                ClearFields();
                MessageBox.Show("Книга добавлена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void UpdateBook()
        {
            var selectedBook = (Book)dataGridView1.SelectedRows[0].DataBoundItem;
            try
            {
                _bookManager.UpdateBook(selectedBook.Id, txtField1.Text, txtField2.Text, txtField3.Text, int.Parse(txtField4.Text));
                LoadBooks();
                ClearFields();
                MessageBox.Show("Книга обновлена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void DeleteBook()
        {
            var selectedBook = (Book)dataGridView1.SelectedRows[0].DataBoundItem;
            if (_bookManager.DeleteBook(selectedBook.Id))
            {
                LoadBooks();
                MessageBox.Show("Книга удалена!");
            }
            else
            {
                MessageBox.Show("Ошибка при удалении книги!");
            }
        }

        private void AddGiver()
        {
            try
            {
                if (!int.TryParse(txtField2.Text, out int bookId))
                {
                    MessageBox.Show("Введите корректный ID книги!");
                    return;
                }

                if (!int.TryParse(txtField3.Text, out int year))
                {
                    MessageBox.Show("Введите корректный год!");
                    return;
                }

                _giverManager.AddGiver(txtField1.Text, bookId, year, txtField4.Text);
                LoadGivers();
                ClearFields();
                MessageBox.Show("Даритель добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void UpdateGiver()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedGiverDisplay = (GiverDisplay)dataGridView1.SelectedRows[0].DataBoundItem;
                try
                {
                    int newBookId = selectedGiverDisplay.BookId;
                    if (!string.IsNullOrWhiteSpace(txtField2.Text) && int.TryParse(txtField2.Text, out int parsedBookId))
                    {
                        newBookId = parsedBookId;
                    }

                    int newYear = selectedGiverDisplay.PowerCount;
                    if (!string.IsNullOrWhiteSpace(txtField3.Text) && int.TryParse(txtField3.Text, out int parsedYear))
                    {
                        newYear = parsedYear;
                    }

                    _giverManager.UpdateGiver(selectedGiverDisplay.Id, txtField1.Text, newBookId, newYear, txtField4.Text);
                    LoadGivers();
                    ClearFields();
                    MessageBox.Show("Даритель обновлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void DeleteGiver()
        {
            
            var selectedGiverDisplay = (GiverDisplay)dataGridView1.SelectedRows[0].DataBoundItem;
            if (_giverManager.DeleteGiver(selectedGiverDisplay.Id)) 
            {
                LoadGivers();
                MessageBox.Show("Даритель удален!");
            }
            else
            {
                MessageBox.Show("Ошибка при удалении дарителя!");
            }
        }

        private void btnBusiness1_Click(object sender, EventArgs e)
        {
            if (cmbEntityType.SelectedItem.ToString() == "Книги")
            {
                var groups = _bookManager.GroupBooksByAuthor();
                string result = "Книги по авторам:\n\n";
                foreach (var group in groups)
                {
                    result += $"Автор: {group.Key}:\n";
                    foreach (var book in group.Value)
                    {
                        result += $"   - {book.Title} ({book.Year}г., {book.AbilitiesOfTheBook})\n";
                    }
                    result += "\n";
                }
                MessageBox.Show(result);
            }
            else
            {
                var groups = _giverManager.GroupGiversByTeams();
                string result = "Дарители по командам:\n\n";
                foreach (var group in groups)
                {
                    result += $"Команда: {group.Key}:\n";
                    foreach (var giver in group.Value)
                    {
                        var book = _bookManager.GetBookById(giver.BookId);
                        string bookTitle = book?.Title ?? "Неизвестная книга";
                        result += $"   - {giver.Name} (книга: {bookTitle})\n";
                    }
                    result += "\n";
                }
                MessageBox.Show(result);
            }
        }

        private void btnBusiness2_Click(object sender, EventArgs e)
        {
            if (cmbEntityType.SelectedItem.ToString() == "Книги")
            {
                string year = Microsoft.VisualBasic.Interaction.InputBox("Введите год:", "Поиск книг", "2000");
                if (int.TryParse(year, out int yearValue))
                {
                    var books = _bookManager.GetBooksPublishedAfterYear(yearValue);
                    string result = $"Книги после {yearValue} года:\n\n";
                    foreach (var book in books)
                    {
                        result += $"{book.Title} - {book.Author} ({book.Year}г., {book.AbilitiesOfTheBook})\n";
                    }
                    MessageBox.Show(result);
                }
            }
            else
            {
                string year = Microsoft.VisualBasic.Interaction.InputBox("Введите очки силы:", "Поиск Книг", "2000");
                if (int.TryParse(year, out int yearValue))
                {
                    var givers = _giverManager.GetGiversWithBooksAfterYear(yearValue); 
                    string result = $"Книги  с очками силы больше {yearValue} :\n\n";
                    foreach (var giver in givers)
                    {
                        var book = _bookManager.GetBookById(giver.BookId);
                        string bookTitle = book?.Title ?? "Неизвестная книга";
                        result += $"{giver.Name} - {bookTitle} ({giver.YearOfCreation}г.)\n";
                    }
                    MessageBox.Show(result);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (cmbEntityType.SelectedItem.ToString() == "Книги")
                {
                    var book = (Book)dataGridView1.SelectedRows[0].DataBoundItem;
                    txtField1.Text = book.Title;
                    txtField2.Text = book.Author;
                    txtField3.Text = book.AbilitiesOfTheBook;
                    txtField4.Text = book.Year.ToString();
                }
                else
                {
                    
                    var giverDisplay = (GiverDisplay)dataGridView1.SelectedRows[0].DataBoundItem;
                    txtField1.Text = giverDisplay.Name;
                    txtField2.Text = giverDisplay.BookId.ToString(); 
                    txtField3.Text = giverDisplay.PowerCount.ToString();
                    txtField4.Text = giverDisplay.Team;
                }
            }
        }

        private void ClearFields()
        {
            txtField1.Clear();
            txtField2.Clear();
            txtField3.Clear();
            txtField4.Clear();
            txtField5.Clear();
        }

        private void lblField5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}