using BookManagerApp.Logic;
using BookManagerApp.Model;
using BookManagerApp.Presenter;
using BookManagerApp.Shared.models;
using BookManagerApp.Shared;
using Ninject;

namespace BookManagerApp.WinFormsUI
{
    /// <summary>
    /// Главная форма приложения для управления книгами и дарителями
    /// Реализует интерфейс ILibraryView для архитектуры MVP
    /// </summary>
    public partial class MainForm : Form, ILibraryView
    {
        private LibraryPresenter _presenter;

        public event EventHandler LoadBooks;
        public event EventHandler AddBook;
        public event EventHandler UpdateBook;
        public event EventHandler DeleteBook;
        public event EventHandler GroupBooksByAuthor;
        public event EventHandler BooksAfterYear;

        public event EventHandler LoadGivers;
        public event EventHandler AddGiver;
        public event EventHandler UpdateGiver;
        public event EventHandler DeleteGiver;
        public event EventHandler GroupGiversByTeam;
        public event EventHandler GiversWithPower;

        /// <summary>
        /// Инициализирует новую форму и создает зависимости через Ninject
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());

            // 2. Получаем ILibraryFacade через Ninject
            var library = ninjectKernel.Get<ILibraryFacade>();

            // 3. Создаем Presenter ВРУЧНУЮ (не через Ninject!)
            _presenter = new LibraryPresenter(this, library);

            InitializeForm();
            lblField5.Visible = false;
            txtField5.Visible = false;
        }

        /// <summary>
        /// Возвращает название книги из текстового поля 1
        /// </summary>
        public string GetBookTitle() => txtField1.Text;

        /// <summary>
        /// Возвращает автора книги из текстового поля 2
        /// </summary>
        public string GetBookAuthor() => txtField2.Text;

        /// <summary>
        /// Возвращает способность книги из текстового поля 3
        /// </summary>
        public string GetBookAbility() => txtField3.Text;

        /// <summary>
        /// Возвращает год издания книги из текстового поля 4
        /// </summary>
        public string GetBookYear() => txtField4.Text;

        /// <summary>
        /// Возвращает имя дарителя из текстового поля 1
        /// </summary>
        public string GetGiverName() => txtField1.Text;

        /// <summary>
        /// Возвращает ID книги дарителя из текстового поля 2
        /// </summary>
        public string GetGiverBookId() => txtField2.Text;

        /// <summary>
        /// Возвращает очки силы дарителя из текстового поля 3
        /// </summary>
        public string GetGiverPower() => txtField3.Text;

        /// <summary>
        /// Возвращает команду дарителя из текстового поля 4
        /// </summary>
        public string GetGiverTeam() => txtField4.Text;

        /// <summary>
        /// Получает ID выбранной книги из DataGridView
        /// </summary>
        public int GetSelectedBookId()
        {
            if (cmbEntityType.SelectedItem?.ToString() == "Книги" &&
                dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].DataBoundItem is Book book)
                {
                    return book.Id;
                }
            }
            return 0;
        }

        /// <summary>
        /// Получает ID выбранного дарителя из DataGridView
        /// </summary>
        public int GetSelectedGiverId()
        {
            if (cmbEntityType.SelectedItem?.ToString() == "Дарители" &&
                dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].DataBoundItem is GiverDisplay giverDisplay)
                {
                    return giverDisplay.Id;
                }
            }
            return 0;
        }

        /// <summary>
        /// Отображает список книг в DataGridView
        /// </summary>
        public void ShowBooks(List<BookDto> bookDtos)
        {
            var books = bookDtos.Select(dto => new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                Author = dto.Author,
                AbilitiesOfTheBook = dto.AbilitiesOfTheBook,
                Year = dto.Year
            }).ToList();

            dataGridView1.DataSource = books;
        }

        /// <summary>
        /// Отображает список дарителей в DataGridView
        /// </summary>
        public void ShowGivers(List<GiverDto> giverDtos)
        {
            var giversDisplay = new List<GiverDisplay>();

            foreach (var dto in giverDtos)
            {
                // Запрашиваем информацию о книге через Presenter
                var bookDto = _presenter.GetBookInfo(dto.BookId);

                giversDisplay.Add(new GiverDisplay
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    BookId = dto.BookId,
                    BookTitle = bookDto?.Title ?? "Не найдена",
                    BookAbility = bookDto?.AbilitiesOfTheBook ?? "Нет способности",
                    PowerCount = dto.YearOfCreation,
                    Team = dto.Team
                });
            }

            dataGridView1.DataSource = giversDisplay;
        }

        /// <summary>
        /// Показывает сообщение пользователю
        /// </summary>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Очищает все текстовые поля формы
        /// </summary>
        public void ClearForm()
        {
            txtField1.Clear();
            txtField2.Clear();
            txtField3.Clear();
            txtField4.Clear();
            txtField5.Clear();
        }

        /// <summary>
        /// Инициализирует элементы управления формы
        /// </summary>
        private void InitializeForm()
        {
            cmbEntityType.Items.Add("Книги");
            cmbEntityType.Items.Add("Дарители");
            cmbEntityType.SelectedIndex = 0;
            cmbEntityType.SelectedIndexChanged += cmbEntityType_SelectedIndexChanged;

            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnBusiness1.Click += btnBusiness1_Click;
            btnBusiness2.Click += btnBusiness2_Click;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            LoadBooks?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbEntityType.SelectedItem.ToString() == "Книги")
            {
                AddBook?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                AddGiver?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Обновить"
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (cmbEntityType.SelectedItem.ToString() == "Книги")
                {
                    UpdateBook?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    UpdateGiver?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для обновления!");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (cmbEntityType.SelectedItem.ToString() == "Книги")
                {
                    DeleteBook?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    DeleteGiver?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления!");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Бизнес-функция 1"
        /// </summary>
        private void btnBusiness1_Click(object sender, EventArgs e)
        {
            if (cmbEntityType.SelectedItem.ToString() == "Книги")
            {
                GroupBooksByAuthor?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                GroupGiversByTeam?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Бизнес-функция 2"
        /// </summary>
        private void btnBusiness2_Click(object sender, EventArgs e)
        {
            if (cmbEntityType.SelectedItem.ToString() == "Книги")
            {
                BooksAfterYear?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                GiversWithPower?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Обработчик изменения выбранного типа сущности
        /// </summary>
        private void cmbEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearForm();
            if (cmbEntityType.SelectedItem.ToString() == "Книги")
            {
                LoadBooks?.Invoke(this, EventArgs.Empty);
                UpdateBookControls();
            }
            else
            {
                LoadGivers?.Invoke(this, EventArgs.Empty);
                UpdateGiverControls();
            }
        }

        /// <summary>
        /// Настраивает элементы управления для работы с книгами
        /// </summary>
        private void UpdateBookControls()
        {
            lblField1.Text = "Название:";
            lblField2.Text = "Автор:";
            lblField3.Text = "Способность:";
            lblField4.Text = "Год:";

            lblField1.Visible = true; txtField1.Visible = true;
            lblField2.Visible = true; txtField2.Visible = true;
            lblField3.Visible = true; txtField3.Visible = true;
            lblField4.Visible = true; txtField4.Visible = true;
            lblField5.Visible = false; txtField5.Visible = false;
        }

        /// <summary>
        /// Настраивает элементы управления для работы с дарителями
        /// </summary>
        private void UpdateGiverControls()
        {
            lblField1.Text = "Имя:";
            lblField2.Text = "ID книги:";
            lblField3.Text = "Очки Силы:";
            lblField4.Text = "Команда:";

            lblField1.Visible = true; txtField1.Visible = true;
            lblField2.Visible = true; txtField2.Visible = true;
            lblField3.Visible = true; txtField3.Visible = true;
            lblField4.Visible = true; txtField4.Visible = true;
            lblField5.Visible = false; txtField5.Visible = false;
        }

        /// <summary>
        /// Обработчик изменения выбранной строки в DataGridView
        /// </summary>
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

        private void lblField5_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}