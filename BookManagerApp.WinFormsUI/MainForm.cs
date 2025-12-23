using BookManagerApp.Model;
using BookManagerApp.Shared;
using BookManagerApp.Shared.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BookManagerApp.WinFormsUI
{
    public partial class MainForm : Form, ILibraryView
    {
        // === События интерфейса ILibraryView ===
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
        /// Конструктор формы (теперь без фасада и без внутреннего Presenter).
        /// Presenter создаётся снаружи через PresenterFactory и подписывается на события этой формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeForm();

            lblField5.Visible = false;
            txtField5.Visible = false;

            // Важно: грузим данные после того, как Presenter уже будет создан и подпишется на события.
            // Поэтому делаем это в Shown (сработает уже после Application.Run, когда подписки точно есть).
            this.Shown += MainForm_Shown;
        }
        /// <summary>
        /// Загружаем стартовые данные в зависимости от выбранной сущности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            
            if (cmbEntityType.SelectedItem?.ToString() == "Книги")
                LoadBooks?.Invoke(this, EventArgs.Empty);
            else
                LoadGivers?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///  Методы интерфейса ILibraryView 
        /// </summary>
        /// <returns></returns>

        public string GetBookTitle() => txtField1.Text;
        public string GetBookAuthor() => txtField2.Text;
        public string GetBookAbility() => txtField3.Text;
        public string GetBookYear() => txtField4.Text;

        public string GetGiverName() => txtField1.Text;
        public string GetGiverBookId() => txtField2.Text;
        public string GetGiverPower() => txtField3.Text;
        public string GetGiverTeam() => txtField4.Text;

        public int GetSelectedBookId()
        {
            if (cmbEntityType.SelectedItem?.ToString() == "Книги" &&
                dataGridView1.SelectedRows.Count > 0 &&
                dataGridView1.SelectedRows[0].DataBoundItem is Book book)
            {
                return book.Id;
            }
            return 0;
        }

        public int GetSelectedGiverId()
        {
            if (cmbEntityType.SelectedItem?.ToString() == "Дарители" &&
                dataGridView1.SelectedRows.Count > 0 &&
                dataGridView1.SelectedRows[0].DataBoundItem is GiverDisplay giverDisplay)
            {
                return giverDisplay.Id;
            }
            return 0;
        }

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

        public void ShowGivers(List<GiverDto> giverDtos)
        {
            var giversDisplay = new List<GiverDisplay>();

            foreach (var dto in giverDtos)
            {
                // Получаем информацию о книге через Shared-сервис (инициализируется в PresenterFactory)
                var bookDto = BookInfoService.GetBookInfo(dto.BookId);

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

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ClearForm()
        {
            txtField1.Clear();
            txtField2.Clear();
            txtField3.Clear();
            txtField4.Clear();
            txtField5.Clear();
        }

        // === Инициализация и обработчики формы ===

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

            UpdateBookControls();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbEntityType.SelectedItem?.ToString() == "Книги")
                AddBook?.Invoke(this, EventArgs.Empty);
            else
                AddGiver?.Invoke(this, EventArgs.Empty);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите элемент для обновления!");
                return;
            }

            if (cmbEntityType.SelectedItem?.ToString() == "Книги")
                UpdateBook?.Invoke(this, EventArgs.Empty);
            else
                UpdateGiver?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите элемент для удаления!");
                return;
            }

            if (cmbEntityType.SelectedItem?.ToString() == "Книги")
                DeleteBook?.Invoke(this, EventArgs.Empty);
            else
                DeleteGiver?.Invoke(this, EventArgs.Empty);
        }

        private void btnBusiness1_Click(object sender, EventArgs e)
        {
            if (cmbEntityType.SelectedItem?.ToString() == "Книги")
                GroupBooksByAuthor?.Invoke(this, EventArgs.Empty);
            else
                GroupGiversByTeam?.Invoke(this, EventArgs.Empty);
        }

        private void btnBusiness2_Click(object sender, EventArgs e)
        {
            if (cmbEntityType.SelectedItem?.ToString() == "Книги")
                BooksAfterYear?.Invoke(this, EventArgs.Empty);
            else
                GiversWithPower?.Invoke(this, EventArgs.Empty);
        }

        private void cmbEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearForm();

            if (cmbEntityType.SelectedItem?.ToString() == "Книги")
            {
                UpdateBookControls();
                LoadBooks?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                UpdateGiverControls();
                LoadGivers?.Invoke(this, EventArgs.Empty);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            if (cmbEntityType.SelectedItem?.ToString() == "Книги" &&
                dataGridView1.SelectedRows[0].DataBoundItem is Book book)
            {
                txtField1.Text = book.Title;
                txtField2.Text = book.Author;
                txtField3.Text = book.AbilitiesOfTheBook;
                txtField4.Text = book.Year.ToString();
            }
            else if (cmbEntityType.SelectedItem?.ToString() == "Дарители" &&
                     dataGridView1.SelectedRows[0].DataBoundItem is GiverDisplay giverDisplay)
            {
                txtField1.Text = giverDisplay.Name;
                txtField2.Text = giverDisplay.BookId.ToString();
                txtField3.Text = giverDisplay.PowerCount.ToString();
                txtField4.Text = giverDisplay.Team;
            }
        }

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

        // Заглушки (если дизайнер их создавал)
        private void lblField5_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
